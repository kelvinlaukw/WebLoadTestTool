using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Script.Serialization;

namespace LoadTestProgram.Class
{
    class Job
    {
        public string jobType;
        public string jobUrl;
        public string jobMethod;
        public string[] paramList;

        public Job(string jobType, string jobUrl, string jobMethod)
        {
            this.jobType = jobType;
            this.jobUrl = jobUrl;
            this.jobMethod = jobMethod;
        }

        public static List<JobData> GetJobDataList(string FilePath)
        {
            List<JobData> dataList = new List<JobData>();

            if (Directory.Exists(FilePath))
            {
                FileInfo[] fileList = new DirectoryInfo(FilePath).GetFiles("*.txt", SearchOption.AllDirectories);
                
                foreach(FileInfo info in fileList)
                {
                    dataList.Add(GetJobData(FilePath, Path.GetFileNameWithoutExtension(info.Name)));
                }
            }

            return dataList;
        }

        public static JobData GetJobData(string FilePath, string jobType)
        {
            string fileName = FilePath + "\\" + jobType + ".txt";
            JobData data = new JobData();
            string line;
            ListDictionary param;

            data.jobType = jobType;
            using (StreamReader sr = new StreamReader(fileName))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    var JSONparams = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(line);
                    param = new ListDictionary();
                    foreach (var JSONparam in JSONparams)
                    {
                        param.Add(JSONparam.Key, JSONparam.Value);
                    }
                    data.paramList.Add(param);
                }

                sr.Close();
                sr.Dispose();
            }
            return data;
        }

        public static List<Job> GetJobList(string FilePath)
        {
            string line;
            List<Job>jobList = new List<Job>();
            Job jobNode;
            string[] webUrlNode;
            
            using (StreamReader sr = new StreamReader(FilePath))
            {
                while((line=sr.ReadLine()) != null)
                {
                    webUrlNode = line.Split(',');
                    jobNode =new Job(webUrlNode[0], webUrlNode[1], webUrlNode[2]);
                    jobList.Add(jobNode);
                }

                sr.Close();
                sr.Dispose();
            }

            return jobList;
        }
    }
}
