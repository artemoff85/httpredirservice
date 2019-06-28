using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using HTTPRedirectorClass;

namespace httpredirservice
{
    public partial class httpredirservice : ServiceBase
    {
        static string[] prefixes = { "http://*/", "https://*:443/" };
        public httpredirservice()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            HTTPRedirector.Start(prefixes, true);
        }

        protected override void OnStop()
        {
            
        }

    }
}