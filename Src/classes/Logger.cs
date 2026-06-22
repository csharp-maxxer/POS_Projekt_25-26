using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_Jahre_Hoelle.classes
{
    public static class Logger
    {
        public static Serilog.Core.Logger logger { get; private set; }
        public static bool Initialized { get; private set; } = false;
        public static void init(string logfilename)
        {

            logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.Console()
               .WriteTo.File(logfilename,
                   rollingInterval: RollingInterval.Day,
                   retainedFileCountLimit: 7)
               .CreateLogger();


            Log.Information("Logger initialisiert.");
            Initialized = true;
        }

    }
}
