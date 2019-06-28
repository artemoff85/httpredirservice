using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;

namespace HTTPRedirectorClass
{
    public static class HTTPRedirector
    {
        private static bool IsRun = true;
        private static string[] aPrefixes = { };

        private static void RedirectProc(object o)
        {
            HttpListenerContext context = o as HttpListenerContext;
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;
            string responseString = "";
            string redirectString = request.Url.ToString().Replace("://", "://www.");
            response.Redirect(redirectString);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

        private static void LaunchRedirectLoop(object aObject)
        {
            HttpListener aListener = (HttpListener)aObject;
            aListener.Start();
            while (IsRun)
            {
                ThreadPool.QueueUserWorkItem(RedirectProc, aListener.GetContext());
            }
            aListener.Stop();
        }

        public static void Start(string[] aPrefixes, bool IsOnNewThread)
        {
            HttpListener listener = new HttpListener();
            foreach (string prefix in aPrefixes)
            {
                listener.Prefixes.Add(prefix);
            }
            
            if (IsOnNewThread)
            {
                ThreadPool.QueueUserWorkItem(LaunchRedirectLoop, listener);
            }
            else
            {
                LaunchRedirectLoop(listener);
            }
        }

    }
}
