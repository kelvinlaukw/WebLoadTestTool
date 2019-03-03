using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadTestProgram.Class
{
    class ReportTask
    {
        public string taskName;
        public string taskURL;
        public int totalRecordCount;
        public int failureRecordCount;
        public TimeSpan totalProcessTime = new TimeSpan(0,0,0);
        public TimeSpan minTime = new TimeSpan(0, 0, 0);
        public TimeSpan maxTime = new TimeSpan(0, 0, 0);

        public ReportTask Clone()
        {
            return (ReportTask)this.MemberwiseClone();
        }
    }
}
