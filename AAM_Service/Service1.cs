using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using AAMLib.Entities;
using AAMLib;
using System.Configuration;

namespace AAM_Service
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer = null;
        string ConnStr = "";//"Data Source=172.16.20.235\\SQL_GHR;Initial Catalog=GHR_GreenCity;Persist Security Info=True;User ID=test;Password=12345678aA";
        string ConnStrCC = "";//"Data Source=172.16.20.235\\SQL_GHR;Initial Catalog=GHR_GreenCity;Persist Security Info=True;User ID=test;Password=12345678aA";

        private Int32 interval = 1000; //Init interval (milliseconds)
        private double dateDiff = -1;

        AAMLib.LogUtilities logUtil = new AAMLib.LogUtilities();

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer(interval);

            //Read configuration
            try
            {
                ConnStr = ConfigurationManager.AppSettings["ConnectionString"].ToString() ?? "";
                ConnStrCC = ConfigurationManager.AppSettings["ConnectionStringCC"].ToString() ?? "";
                interval = Convert.ToInt32(ConfigurationManager.AppSettings["Interval"]);
                dateDiff = Convert.ToDouble(ConfigurationManager.AppSettings["DateDiff"]);

                if (ConnStr.Length > 0 && ConnStrCC.Length > 0)
                {
                    logUtil.WriteLog("Read configuration successful. Interval: " + timer.Interval.ToString() + "̣(ms).");
                }
                else
                {
                    logUtil.WriteLog("Configuration error");
                }
            }
            catch (ConfigurationErrorsException ex)
            {
                logUtil.WriteLog("Read configuration failed: " + ex.Message.ToString());
            }

            logUtil.WriteLog("");
            logUtil.WriteLog("Service started");
            // Tạo 1 timer từ libary System.Timers, execute sau mỗi 1 đơn vị interval
            // Những gì xảy ra khi timer đó dc tick
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;

            // Enable timer
            timer.Enabled = true;
            
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (interval > 0)
            {
                timer.Stop();
                timer.Interval = interval;
                timer.Start();
            }

            DateTime date = DateTime.Now; //Get current date time

            //Write log start job:
            logUtil.WriteLog("");
            logUtil.WriteLog("Start job. Date to get Attendance logs: " + date.Date.ToString("dd-MM-yyyy"));
            
            AAMLib.DevideInfoLib devLib = new AAMLib.DevideInfoLib(ConnStr);
            DBLib dbLib = new DBLib(ConnStrCC);

            //Get list device
            List<DeviceInfo> listDevice = devLib.GetListDevice();
            if (listDevice.Count > 0)
            {
                logUtil.WriteLog("");

                for (int i = 0; i < listDevice.Count; i++)
                {
                    UserAttLib attendanceLib = new UserAttLib(listDevice[i].DeviceName, listDevice[i].DeviceIP, listDevice[i].DevicePort, listDevice[i].DeviceID, listDevice[i].LastAccessDateTime);

                    date = DateTime.Now;

                    List<AttendanceInfo> lAttInfo = attendanceLib.GetAttInfoByDate();

                    if (lAttInfo == null) { continue; }

                    if (lAttInfo.Count() > 0)
                    {
                        logUtil.WriteLog("New records found: " + lAttInfo.Count());
                        dbLib.InsertAttLog(lAttInfo); //Insert att log to database
                        devLib.Update_DuLieuDocLanCuoi(listDevice[i].DeviceID, date); //Update DuLieuDocLanCuoi
                    }
                    else
                    {
                        logUtil.WriteLog("No new record found");
                    }
                }
                dbLib.ExecStoreUpdateNhanVienID(); //update employeeid
            }
        }

        protected override void OnStop()
        {
            logUtil.WriteLog("Service stopped.");
        }
    }
}
