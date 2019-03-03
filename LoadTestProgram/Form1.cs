using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoadTestProgram.Class;
using System.Configuration;

namespace LoadTestProgram
{
    public partial class Form1 : Form
    {
        private Thread mainThread;
        private ParallelOptions parallelOption;
        private List<Job> jobList;
        private List<JobData> jobDataList;
        private int maxNoOfVirtualUser, iterationUser, deferStartTime, threadSleepTime = 0;
        private Boolean repeatTask;
        private int[] userTaskRatio = null;
        private Report rpt;
        private TimeSpan threadSleepTimeInterval;
        private List<Task> tasks = new List<Task>();
        private Boolean CancelFlag = false;
        static readonly object locker = new object();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                tbNoOfVirtualUser.Enabled = false;
                tbUserPerIteration.Enabled = false;
                tbDeferStartTime.Enabled = false;
                cbRepeatTask.Enabled = false;
                tbThreadSleepTime.Enabled = false;
                tbUserTaskRatio.Enabled = false;
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                mainThread = new Thread(new ThreadStart(this.StartRun));
                mainThread.Start();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            //cancelTokenSource.Cancel();
            CancelFlag = true;
            //Thread.Sleep(10000);
            Task.WaitAll(tasks.ToArray(), 30000);
            mainThread.Abort();
            rpt.GenerateReport();

            CancelFlag = false;
            tbNoOfVirtualUser.Enabled = true;
            tbUserPerIteration.Enabled = true;
            tbDeferStartTime.Enabled = true;
            cbRepeatTask.Enabled = true;
            tbThreadSleepTime.Enabled = true;
            tbUserTaskRatio.Enabled = true;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        public void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
            tbLiveMessageBoard.AppendText(value + "\r\n");
        }

        private Boolean Validation()
        {
            Boolean result = true;

            //Get the job list from configuration files
            jobList = Job.GetJobList(ConfigurationManager.AppSettings["WebUrlFilePath"].ToString());
            jobDataList = Job.GetJobDataList(ConfigurationManager.AppSettings["DataFilePath"].ToString());

            if (!string.IsNullOrEmpty(tbUserTaskRatio.Text.ToString()))
            {
                string[] ratioValues = tbUserTaskRatio.Text.Split('/');
                userTaskRatio = Array.ConvertAll<string, int>(ratioValues, int.Parse);
            }

            //Check record count with the job and user task ratio
            if (jobList.Count != userTaskRatio.Length)
            {
                MessageBox.Show("Number of Job and User Task Ratio is mismatched!");
                result = false;
            }

            if (!Int32.TryParse(tbUserPerIteration.Text, out iterationUser)) 
            { 
                iterationUser = 0;
                MessageBox.Show("Number of user per iteration is not numeric data!");
                result = false;
            }

            if (cbRepeatTask.Checked && !Int32.TryParse(tbThreadSleepTime.Text, out threadSleepTime))
            {
                threadSleepTime = 1;
                MessageBox.Show("Thread sleep time is not numeric data!");
                result = false;
            }

            if (!Int32.TryParse(tbDeferStartTime.Text, out deferStartTime)) 
            { 
                deferStartTime = 0;
            }

            repeatTask = cbRepeatTask.Checked;

            return result;
        }

        private async Task ProcessAsyncTask(VirtualUser vu)
        {
            
            if (!vu.repeatTask)
            {
                await vu.ProcessAction(jobDataList, rpt);
            }
            else
            {
                while (vu.repeatTask)
                {
                    await vu.ProcessAction(jobDataList, rpt);

                    lock(locker)
                    {
                        if (CancelFlag)
                        {
                            break;
                        }
                        else
                        {
                            AppendTextBox(vu.ReportStatus());
                            System.Threading.Thread.Sleep(threadSleepTimeInterval);
                        }
                    }
                }

                if (CancelFlag)
                {
                    await Task.Delay(1);
                }
            }
        }

        private void MergeAndProcessVirtualUserList(List<VirtualUserPool> virtualUserPoolList)
        {
            tasks = new List<Task>();

            List<VirtualUser> mergedVirtualUserList = new List<VirtualUser>();
            foreach(VirtualUserPool vp in virtualUserPoolList)
            {
                mergedVirtualUserList.InsertRange(0, vp.VirtualUserList);
            }

            Parallel.ForEach(mergedVirtualUserList, parallelOption, (singleVirtualUser) =>
            {
                AppendTextBox(singleVirtualUser.ReportStatus());

                TimeSpan deferStartTimeInterval;
                deferStartTimeInterval = new TimeSpan(0, 0, singleVirtualUser.deferStartTime);
                System.Threading.Thread.Sleep(deferStartTimeInterval);

                tasks.Add(ProcessAsyncTask(singleVirtualUser));
            });
        }

        private void StartRun()
        {
            parallelOption = new ParallelOptions();
            
            rpt = new Report();
            rpt.InitialJobList(jobList);

            List<UserGroup> virtualUserGroups = new List<UserGroup>();
            List<VirtualUserPool> virtualUserPoolList = new List<VirtualUserPool>();
            threadSleepTimeInterval = new TimeSpan(0, 0, threadSleepTime);
        
            if (Int32.TryParse(tbNoOfVirtualUser.Text, out maxNoOfVirtualUser))
            {
                virtualUserGroups = VirtualUser.DeclareVirtualUserGroup(maxNoOfVirtualUser, iterationUser, deferStartTime, repeatTask);

                parallelOption.MaxDegreeOfParallelism = 20;
                
                foreach (UserGroup group in virtualUserGroups)
                {
                    virtualUserPoolList.Add(VirtualUser.SetupVirtualUserPool(jobList, group.noOfUser, group.groupId, group.deferStartTime, group.repeatTask));
                }

                foreach (VirtualUserPool vp in virtualUserPoolList)
                {
                    foreach (VirtualUser vu in vp.VirtualUserList)
                    {
                        //Assign Job to virtual user
                        vu.AssignJob(vp.VirtualUserList.Length, vu.GetID(), userTaskRatio, jobList);
                    }
                }
                MergeAndProcessVirtualUserList(virtualUserPoolList);
            }
            else
            {
                //Prompt Error Message
                MessageBox.Show("Error! No. of Virtual User is not a numeric data!");
            }
        }

        private void cbRepeatTask_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRepeatTask.Checked)
            {
                tbThreadSleepTime.Enabled = true;
            }
            else
            {
                tbThreadSleepTime.Enabled = false;
                tbThreadSleepTime.Clear();
            }
        }
    }
}
