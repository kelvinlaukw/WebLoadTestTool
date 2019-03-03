using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadTestProgram.Class
{
    class JobData
    {
        public string jobType;
        public List<ListDictionary> paramList;
       
        public JobData()
        {
            this.jobType = string.Empty;
            this.paramList = new List<ListDictionary>();
        }
    }
}
