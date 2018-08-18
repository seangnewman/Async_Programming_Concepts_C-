using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Networking
{
    [TestClass]
    public class Test_Download
    {
        string url = "http://www.deelay.me/10000/http://www.delsink.com";

        [TestMethod]
        public void Test_Download_delsinkidotcom_Synchronous()
        {
            var httpRequestInfo = HttpWebRequest.CreateHttp(url);
            var httpResponseInfo = httpRequestInfo.GetResponse();

            var responseStream = httpResponseInfo.GetResponseStream();  // downloading page content
            using (var sr = new StreamReader(responseStream))
            {
                var webPage = sr.ReadToEnd();
            }
        }
        
        [TestMethod]
        public async Task Test_Download_deslinkdotcom_AsyncAwait()
        {
            var httpRequestInfo = HttpWebRequest.CreateHttp(url);
            var httpResponseInfo =  await httpRequestInfo.GetResponseAsync();

            var responseStream = httpResponseInfo.GetResponseStream();  // downloading page content
            using (var sr = new StreamReader(responseStream))
            {
                var webPage = sr.ReadToEnd();
            }
        }




        [TestMethod]
        public void Test_Download_deslinkdotcom_BeginEnd()
        {
            var httpRequestInfo = HttpWebRequest.CreateHttp(url);
            var callback = new AsyncCallback(HttpResponseAvailable);

            var ar = httpRequestInfo.BeginGetResponse(callback, httpRequestInfo) ;   //initial round trip to server

            ar.AsyncWaitHandle.WaitOne();
        }

        private static void HttpResponseAvailable(IAsyncResult ar)
        {
            // Running asyncronously on background thread
            var httpRequestInfo = ar.AsyncState as HttpWebRequest;
            var httpResponseInfo = httpRequestInfo.EndGetResponse(ar) as HttpWebResponse;

            var responseStream = httpResponseInfo.GetResponseStream();  // downloading page content

            using (var sr = new StreamReader(responseStream))
            {
                var webPage = sr.ReadToEnd();
            }


        }


        [TestMethod]
        public void Test_Download_deslinkdotcom_AsyncTask()
        {
            var httpRequestInfo = HttpWebRequest.CreateHttp(url);
           
            Task<WebResponse> taskWebResponse = httpRequestInfo.GetResponseAsync();
            Task taskContinuation = taskWebResponse.ContinueWith(HttpResponseContinuation, TaskContinuationOptions.OnlyOnRanToCompletion);
          
            Task.WaitAll(taskWebResponse, taskContinuation);
        }

        private static void HttpResponseContinuation(Task<WebResponse> taskResponse)
        {
            var httpResponseInfo = taskResponse.Result as HttpWebResponse;


            var responseStream = httpResponseInfo.GetResponseStream();  // downloading page content

            using (var sr = new StreamReader(responseStream))
            {
                var webPage = sr.ReadToEnd();
            }


        }
    }
}
