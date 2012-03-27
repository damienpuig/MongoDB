using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Mongo.DB
{
    internal class Logger
    {
        internal static void TraceInformation(string message)
        {
            safeExecute(() => Trace.TraceInformation(message));
        }

        internal static void TraceWarning(string message)
        {
            safeExecute(() => Trace.TraceWarning(message));
        }

        internal static void TraceError(string message)
        {
            safeExecute(() => Trace.TraceError(message));
        }

        private static void safeExecute(Action action)
        {
            try
            {
                action();
            }
            catch (Exception)
            {
                throw new Exception("Cannot trace");
            }
        }
    }
}
