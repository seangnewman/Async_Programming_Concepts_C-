using System;
using System.IO;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Networking
{
    [TestClass]
    public class Test_Download
    {
        string url = "http://www.deelay.me/10000/http://www.delsink.com";

        [TestMethod]
        public void Test_Download_DelsinkCOM()
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
    }
}
