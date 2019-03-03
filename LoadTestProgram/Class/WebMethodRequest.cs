using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;   

namespace LoadTestProgram.Class
{
    class WebMethodRequest
    {
        public static async Task<WebRequestMethodsResult> PostMethod(string jobUrl, JobData jobDataProcessing)
        {
            string postData = string.Empty;
            Random rnd = new Random();
            WebRequestMethodsResult result = new WebRequestMethodsResult();
      
            try
            {
                if (jobDataProcessing.paramList.Count > 0)
                {
                    foreach (DictionaryEntry param in jobDataProcessing.paramList[rnd.Next(0, jobDataProcessing.paramList.Count - 1)])
                    {
                        if (postData != string.Empty && param.Value.ToString() != string.Empty)
                        {
                            postData += "&";
                        }

                        if (param.Value.ToString() != string.Empty)
                        {
                            postData += param.Key + "=" + param.Value;
                        }
                    }
                }

                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
                var request = (HttpWebRequest)WebRequest.Create(jobUrl);
                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var startTime = DateTime.Now;
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                var endTime = DateTime.Now;
                int elapsedMs = Convert.ToInt32((endTime - startTime).TotalMilliseconds);

                result.processTime = new TimeSpan(0, 0, 0, 0, elapsedMs);
                result.httpResponse = response;
                result.succeed = true;
                result.responseContent = new StreamReader(response.GetResponseStream()).ReadToEnd();
                result.remark = result.httpResponse.StatusCode + " MS:" + result.processTime.TotalMilliseconds.ToString() + " " + jobUrl + postData + " " + result.responseContent;

                response.Close();
            }
            catch (WebException we)
            {
                result.processTime = new TimeSpan(0, 0, 0, 0, 0);
                result.succeed = false;
                result.httpResponse = null;
                result.responseContent = null;
                result.remark = "POST Error " + jobUrl + postData + " " + we.Message;
            }

            return result;
        }

        public static async Task<WebRequestMethodsResult> GetMethod(string jobUrl, JobData jobDataProcessing)
        {
            string queryString = string.Empty;
            Random rnd = new Random();
            WebRequestMethodsResult result = new WebRequestMethodsResult();

            try
            {
                if (jobDataProcessing.paramList.Count > 0)
                {
                    foreach (DictionaryEntry param in jobDataProcessing.paramList[rnd.Next(0, jobDataProcessing.paramList.Count - 1)])
                    {
                        if (param.Value.ToString() != string.Empty)
                        {
                            queryString += "/" + param.Value ;
                        }
                    }
                }

                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

                var startTime = DateTime.Now;
                var request = (HttpWebRequest)WebRequest.Create(jobUrl + queryString);
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                var endTime = DateTime.Now;
                int elapsedMs = Convert.ToInt32((endTime - startTime).TotalMilliseconds);

                result.processTime = new TimeSpan(0, 0, 0, 0, elapsedMs);
                result.httpResponse = response;
                result.succeed = true;
                result.responseContent = new StreamReader(response.GetResponseStream()).ReadToEnd();
                result.remark = result.httpResponse.StatusCode + " MS:" + result.processTime.TotalMilliseconds.ToString() + " " + jobUrl + queryString + " " + result.responseContent;

                response.Close();
            }
            catch(WebException we)
            {
                result.processTime = new TimeSpan(0, 0, 0, 0, 0);
                result.succeed = false;
                result.httpResponse = null;
                result.responseContent = null;
                result.remark = "GET Error " + jobUrl + queryString + " " + we.Message;
            }
            return result;
        }

        //For HTTPS URL
        public static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
