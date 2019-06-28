using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Net;
using System.Threading;
using HTTPRedirectorClass;

namespace httpredirserviceapp
{
    class Program
    {
        static string[] prefixes = { "http://*/", "https://*:443/" };

        static void Main(string[] args)
        {
            HTTPRedirector.Start(prefixes, false);
        }
    }
}
