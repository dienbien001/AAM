using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAMLib.Entities;

namespace AAMLib
{
    public class UserAttLib
    {
        public zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();

        public string sDevIPAddress = ""; //Device's ip address for connect
        public string sDevName = ""; //Device's name
        public Int16 iDevPort = 4370; //Device's port for connect
        public Int16 iDeviceID = 1; //DeviceID in database
        public DateTime dateToGet = DateTime.Now; //Get current date
        private bool bIsConnected = false; //the boolean value identifies whether the device is connected
        private int iMachineNumber = 1; //the serial number of the device.After connecting the device ,this value will be changed.

        public UserAttLib(string DevName, string DevIPAddress, Int16 DevPort, Int16 DeviceID, DateTime DateToGet)
        {
            sDevName = DevName;
            sDevIPAddress = DevIPAddress;
            iDevPort = DevPort;
            iDeviceID = DeviceID;
            dateToGet = DateToGet;
        }
        #region GetAttLog
        private List<AttendanceInfo> GetAttInfos()
        {
            List<AttendanceInfo> lAttInfo = new List<AttendanceInfo>(); //Store result
            string logPath = AppDomain.CurrentDomain.BaseDirectory; //Path to root directory of the app
            string data = "";
            LogUtilities logUtil = new LogUtilities();

            logUtil.WriteLog(""); //Print break line

            bIsConnected = axCZKEM1.Connect_Net(sDevIPAddress, iDevPort);
            if (bIsConnected)
            {
                data = string.Format("Connected to device <{2} - {3}> at address <{0}:{1}>. Last update time: <{4}>", sDevIPAddress, iDevPort, iDeviceID, sDevName, dateToGet.ToString());
                logUtil.WriteLog(data);
                iMachineNumber = 1;//In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
                axCZKEM1.RegEvent(iMachineNumber, 65535);//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
            }
            else
            {
                int errorCode = 0;
                axCZKEM1.GetLastError(errorCode);
                data = string.Format("Failed to connect to device <{3} - {4}> at address <{0}:{1}>. Error code: {2}", sDevIPAddress, iDevPort, errorCode.ToString(), iDeviceID, sDevName);
                logUtil.WriteLog(data);
                return null;
            }

            string sdwEnrollNumber = "";
            int idwTMachineNumber = 0;
            int idwEMachineNumber = 0;
            int idwVerifyMode = 0;
            int idwInOutMode = 0;
            int idwYear = 0;
            int idwMonth = 0;
            int idwDay = 0;
            int idwHour = 0;
            int idwMinute = 0;
            int idwSecond = 0;
            int idwWorkcode = 0;

            int idwErrorCode = 0;
            int iGLCount = 0;
            int iIndex = 0;

            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;

            axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device
            if (axCZKEM1.ReadGeneralLogData(iMachineNumber))//read all the attendance records to the memory
            {
                while (axCZKEM1.SSR_GetGeneralLogData(iMachineNumber, out sdwEnrollNumber, out idwVerifyMode,
                           out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                {
                    AttendanceInfo attInfo = new AttendanceInfo
                    {
                        DeviceID = iDeviceID,
                        EnrollNumber = sdwEnrollNumber,
                        VerifyMode = idwVerifyMode,
                        InOutMode = idwInOutMode,
                        Year = idwYear,
                        Month = idwMonth,
                        Day = idwDay,
                        Hour = idwHour,
                        Minute = idwMinute,
                        Second = idwSecond,
                        WorkCode = idwWorkcode,
                        EnrollDateTime = DateTime.ParseExact(idwDay.ToString() + "/" + idwMonth.ToString() + "/" + idwYear.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString(), "d/M/yyyy H:m:s", provider),
                };
                    lAttInfo.Add(attInfo);
                }
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);

                if (idwErrorCode != 0)
                {
                    logUtil.WriteLog("Reading data from terminal failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    logUtil.WriteLog("No data from terminal returns!");
                }
            }

            axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device

            axCZKEM1.EnableDevice(iMachineNumber, true);
            axCZKEM1.Disconnect();
            logUtil.WriteLog("Attendance logs count: " + lAttInfo.Count());
            return lAttInfo;
        }
        public List<AttendanceInfo> GetAttInfoByDate()
        {
            List<AttendanceInfo> lAtt = GetAttInfos();

            if (lAtt == null) //Không kết nối được đến thiết bị hoặc không có bản ghi
            {
                return null;
            }

            if (lAtt.Count > 0)
            {
                //var result = from att in lAtt
                //             where att.Year == dateToGet.Year && att.Month == dateToGet.Month && att.Day == dateToGet.Day
                //             select att;
                var result = from att in lAtt
                             where att.EnrollDateTime >= dateToGet
                             select att;
                return (List<AttendanceInfo>)result.ToList();

            }
            return null;
        }
        #endregion
    }
}
