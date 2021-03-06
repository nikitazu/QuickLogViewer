﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace QuickLogViewer.Components
{
    public class ErrorLogSimulator : IDisposable
    {
        private Lazy<EventLog> _eventLog = new Lazy<EventLog>(() => new EventLog("Application", ".", "Application Error"));

        public void SimulateErrorLog()
        {
            try
            {
                var x = new { Foo = (string)null };
                Action doError = () => Console.WriteLine(x.Foo.ToUpper());
                MyMethodThatShouldFail("XXX", doError);
            } catch (Exception e)
            {
                _eventLog.Value.WriteEntry(e.ToString(), EventLogEntryType.Error);
                throw;
            }
        }

        private string MyMethodThatShouldFail(string arg1, Action doError)
        {
            doError();
            Console.WriteLine(arg1);
            return arg1;
        }

        public void Dispose()
        {
            if (_eventLog.IsValueCreated)
            {
                _eventLog.Value.Dispose();
                _eventLog = null;
            }
        }
    }
}
