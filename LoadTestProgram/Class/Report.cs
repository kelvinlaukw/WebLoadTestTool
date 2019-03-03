using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadTestProgram.Class
{
    class Report
    {
        public List<ReportTask> reportTaskList = new List<ReportTask>();
        private List<Job> jobList = new List<Job>();

        public void InitialJobList(List<Job> jobList)
        {
            this.jobList = jobList;
        }

        public void GenerateReport()
        {
            int tolalRecordCount, totalFailureRecordCount = 0;
            TimeSpan totalAverageTime, totalMaxTime, totalMinTime;
            
            tolalRecordCount = reportTaskList.Count();
            totalFailureRecordCount = reportTaskList.Select(x => x.failureRecordCount).Sum() ;
            totalAverageTime = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(reportTaskList.Average(x => x.totalProcessTime.TotalMilliseconds)));
            totalMaxTime = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(reportTaskList.Max(x => x.maxTime.TotalMilliseconds)));
            totalMinTime = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(reportTaskList.Min(x => x.minTime.TotalMilliseconds)));

            using (System.IO.StreamWriter file =
                      new System.IO.StreamWriter(@".\LoadTestsReportSummary_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt", true))
            {
                file.WriteLine("Summary");
                file.WriteLine("=================================================================");
                file.WriteLine("Total no.of sample:" + tolalRecordCount.ToString());
                file.WriteLine("Failure %:" + (double)(totalFailureRecordCount / tolalRecordCount * 100));
                file.WriteLine("Total Average Time(MSec):" + totalAverageTime.TotalMilliseconds.ToString());
                file.WriteLine("Total Max. Time(MSec):" + totalMaxTime.TotalMilliseconds.ToString());
                file.WriteLine("Total Min. Time(MSec):" + totalMinTime.TotalMilliseconds.ToString());
                
                file.WriteLine();
                file.WriteLine("Pages");
                file.WriteLine("=================================================================");

                foreach (Job j in jobList)
                {
                    var selectedReportTaskList = reportTaskList.Where(x=> x.taskName == j.jobType);

                    tolalRecordCount = selectedReportTaskList.Count();
                    
                    if (tolalRecordCount > 0 )
                    {
                        totalFailureRecordCount = selectedReportTaskList.Select(x => x.failureRecordCount).Sum();
                        totalAverageTime = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(selectedReportTaskList.Average(x => x.totalProcessTime.TotalMilliseconds)));
                        totalMaxTime = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(selectedReportTaskList.Max(x => x.maxTime.TotalMilliseconds)));
                        totalMinTime = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(selectedReportTaskList.Min(x => x.minTime.TotalMilliseconds)));

                        file.WriteLine("Task Name:" + j.jobType);
                        file.WriteLine("Total no.of sample:" + tolalRecordCount.ToString());
                        file.WriteLine("Failure %:" + (double)(totalFailureRecordCount / tolalRecordCount * 100));
                        file.WriteLine("Average Time(MSec):" + totalAverageTime.TotalMilliseconds.ToString());
                        file.WriteLine("Max. Time(MSec):" + totalMaxTime.TotalMilliseconds.ToString());
                        file.WriteLine("Min. Time(MSec):" + totalMinTime.TotalMilliseconds.ToString());

                        file.WriteLine();
                    }
                }
            }
        }
    }
}
