using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXC_Net_homework_student
{
    public class Log
    {
        //得到实例类
        public static  ILog log=LogManager.GetLogger("myLog");

        public static void info(string message)
        {
            log.Info(message);
        }

        public static void error(string message)
        {
            log.Error(message);
        }

        public static void debug(string message)
        {
            log.Debug(message);
        }

        public static void warn(string message)
        {
            log.Warn(message);
        }

        public static void fatal(string message)
        {
            log.Fatal(message);
        }

    }
}
