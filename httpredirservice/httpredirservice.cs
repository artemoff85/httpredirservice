using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Net;
using System.Threading;

namespace httpredirservice
{
    public partial class httpredirservice : ServiceBase
    {
        static bool IsRun;
        static string[] prefixes = { "http://*/", "https://*:443/" };
        public httpredirservice()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            IsRun = ThreadPool.QueueUserWorkItem(LaunchLoop);
        }

        protected override void OnStop()
        {
            IsRun = false;
        }

        static void LaunchLoop(object o)
        {
            HttpListener listener = new HttpListener();
            foreach(string prefix in prefixes)
            {
                listener.Prefixes.Add(prefix);
            }

            listener.Start();
            while (IsRun)
            {
                ThreadPool.QueueUserWorkItem(ThreadProc, listener.GetContext());
            }
            listener.Stop();
        }

        static void ThreadProc(object o)
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

    }
}