using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalNeolaser.Helpers
{ 
    public class LogHelper
    {
        public static string LogFile; //nombre del fichero de log, existirá un log por dia.

        public LogHelper()
        {
            SetLogFile();
        }

        private void SetLogFile()
        {
            LogFile = HttpContext.Current.Server.MapPath(String.Format("~/Content/Logs/{0}{1}{2}", DateTime.Today.Year, DateTime.Today.Month.ToString().PadLeft(2, '0'),
                DateTime.Today.Day.ToString().PadLeft(2, '0'))
                );
                
        }

        public void WriteLog(string message)
        {
            using (StreamWriter log = new StreamWriter(LogFile, true))
            {
                log.WriteLine(message);
            }
        }

    }   

}