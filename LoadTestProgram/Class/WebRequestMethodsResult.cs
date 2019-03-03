using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace LoadTestProgram.Class
{
    class WebRequestMethodsResult
    {
        public Boolean succeed = true;
        public string remark;
        public TimeSpan processTime;
        public HttpWebResponse httpResponse;
        public string responseContent;
    }
}
