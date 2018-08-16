using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AsyncDelegateDemo
{
    [TestClass]
    public class Test_Delegates
    {
        private void DoWork()
        {
            Debug.WriteLine("Hello World");
            Debug.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());
        }

        delegate void DoWorkDelegate();    //delegate is like a c++ function pointer


        [TestMethod]
        public void Demo1()
        {
            Debug.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());
            DoWorkDelegate m = new DoWorkDelegate(DoWork);
            AsyncCallback callback = new AsyncCallback(TheCallback);

            // The Begin - End Pattern for Asyncronous Processing
            IAsyncResult ar= m.BeginInvoke(callback, m);

            //Do Work
            ar.AsyncWaitHandle.WaitOne();

            

             
        }

        private static void TheCallback(IAsyncResult ar)
        {
            //Called wihen async method completes
            var m = ar.AsyncState as DoWorkDelegate;

            //this is where you use try catch
            m.EndInvoke(ar);   // Ask for result
        }
    }
}
