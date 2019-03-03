using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadTestProgram.Class
{
    class VirtualUser
    {
        private int id, groupId;
        private string status;
        private DateTime actionTime;
        private List<Job> jobList;
        private Job jobProcessing;
        private ReportTask reportTask = new ReportTask();
        public int deferStartTime = 0;
        public Boolean repeatTask = false;

        public VirtualUser(int id, int groupId)
        {
            this.id = id;
            this.groupId = groupId;
            this.status = "Ready"; 
        }

        public int GetID()
        {
            return this.id;
        }

        public string GetJobProcessing()
        {
            return "Proceeding Job: " + this.jobProcessing.jobType + " URL: " + this.jobProcessing.jobUrl;
        }

        public string ReportStatus()
        {
            return "Iteration:" + this.groupId + " User:" + this.id.ToString() + " is " + this.status;
        }

        public void AssignJob(int noOfUserPerIteration, int indexOfUser, int[] userTaskRatio, List<Job> jobList)
        {
            int maxIndexOfUser = 0;
            int jobIndex = 0;
            Job selectedJob = null;

            foreach (int ratio in userTaskRatio)
            {
                maxIndexOfUser += ratio ;
                if (indexOfUser <= maxIndexOfUser)
                {
                    selectedJob = jobList[jobIndex];
                    break;
                }
                jobIndex ++;
            }

            if (selectedJob != null)
            {
                this.GetJob(false, selectedJob.jobType);
            }
            else 
            {
                this.GetJob(true, string.Empty);
            }
        }

        public void GetJob(Boolean isRandomSelection, string jobType)
        {
            this.jobProcessing = null;

            Job selectedJob = null;
            if (isRandomSelection)
            {
                Random rd = new Random();
                selectedJob = this.jobList[rd.Next(0, this.jobList.Count - 1)]; 
            }
            else 
            {
                selectedJob = this.jobList.Find(x => x.jobType == jobType); 
            }

            this.jobProcessing = selectedJob;

            //Assign the report task 
            this.reportTask.taskName = selectedJob.jobType;
            this.reportTask.taskURL = selectedJob.jobUrl;
        }

        public async Task ProcessAction(List<JobData> jobDataList, Report rpt)
        {
            this.status = "Processing";
            this.actionTime = DateTime.Now;
            WebRequestMethodsResult requestResult = new WebRequestMethodsResult();
            TimeSpan processTime = new TimeSpan(0, 0, 0);
            DateTime processStartTime, processEndTime;

            JobData jobDataProcessing = new JobData();
            jobDataProcessing = jobDataList.Find(x => x.jobType == this.jobProcessing.jobType);

            this.reportTask.totalRecordCount++;
            
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@".\Iteration_" + this.groupId.ToString() + "_User_" + this.id.ToString() + ".txt", true))
            {
                processStartTime = DateTime.Now;
                file.WriteLine(processStartTime.ToString("MM/dd/yyyy hh:mm:ss.fff") + " Job " + this.jobProcessing.jobUrl);
            }

            switch (this.jobProcessing.jobMethod.ToString().ToUpper())
            {
                case "GET": requestResult = await WebMethodRequest.GetMethod(this.jobProcessing.jobUrl, jobDataProcessing);
                    break;
                case "POST": requestResult = await WebMethodRequest.PostMethod(this.jobProcessing.jobUrl, jobDataProcessing);
                    break;
            }

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@".\Iteration_" + this.groupId.ToString() + "_User_" + this.id.ToString() + ".txt",true))
            {
                processEndTime = DateTime.Now;
                file.WriteLine(processEndTime.ToString("MM/dd/yyyy hh:mm:ss.fff") + " " + requestResult.remark);
            }

            if (!requestResult.succeed)
            {
                this.reportTask.failureRecordCount = 1;
            }

            this.reportTask.totalProcessTime = requestResult.processTime;

            if (requestResult.processTime > this.reportTask.maxTime) 
            {
                this.reportTask.maxTime = requestResult.processTime; 
            }

            if (requestResult.processTime < this.reportTask.minTime && requestResult.processTime.TotalMilliseconds > 0) 
            {
                this.reportTask.minTime = requestResult.processTime; 
            }
            else if (this.reportTask.minTime.Ticks == 0) 
            {
                this.reportTask.minTime = requestResult.processTime;
            }

            ReportTask rtCopy = new ReportTask();
            rtCopy = this.reportTask.Clone();

            rpt.reportTaskList.Add(rtCopy);
        }

        public static List<UserGroup> DeclareVirtualUserGroup(int maxNoOfVirtualUser, int iterationUser, int DeferStartTime, Boolean repeatTask)
        {
            int groupId = 1;
            List<UserGroup> virtualUserGroupList = new List<UserGroup>();

            for (int i = 0; i < (maxNoOfVirtualUser / iterationUser); i++)
            {
                virtualUserGroupList.Add(new UserGroup(groupId, iterationUser, DeferStartTime * (i+1), repeatTask));
                groupId++;
            }

            if (maxNoOfVirtualUser % iterationUser > 0)
            {
                virtualUserGroupList.Add(new UserGroup(groupId, (maxNoOfVirtualUser % iterationUser), DeferStartTime, repeatTask));
            }
            
            return virtualUserGroupList;
        }

        public static VirtualUserPool SetupVirtualUserPool(List<Job> jobList, int maxNoOfVirtualUser, int groupId, int deferStartTime, Boolean repeatTask)
        {
            VirtualUser[] virtualUserList = new VirtualUser[maxNoOfVirtualUser];
            
            for (int i = 0; i < maxNoOfVirtualUser; i++)
            {
                virtualUserList[i] = new VirtualUser(i+1, groupId);
                virtualUserList[i].jobList = jobList;
                virtualUserList[i].deferStartTime = deferStartTime;
                virtualUserList[i].repeatTask = repeatTask;
            }

            VirtualUserPool virtualUserPool = new VirtualUserPool(virtualUserList, deferStartTime, repeatTask);

            return virtualUserPool;
        }
    }
}