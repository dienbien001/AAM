using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AAMLib
{
    public class LogUtilities
    {
        string sLogPath = AppDomain.CurrentDomain.BaseDirectory; //Directory's path for log file

        //public LogUtilities (string LogPath)
        //{
        //    sLogPath = LogPath;
        //}
        //public void WriteLog(string LogPath, string sData)
        public void WriteLog(string sData)
        {
            StreamWriter sw = null;
            try
            {
                //sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + LogPath + "\\Full.log", true);
                sw = new StreamWriter(sLogPath + "\\Full.log", true);
                sw.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " -> " + sData);
                sw.Flush();
                sw.Close();
            }
            catch
            {
                // ignored
            }
        }
    }
    
    
}
