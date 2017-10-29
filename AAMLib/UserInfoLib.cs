using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAMLib.Abtracts;
using AAMLib.Entities;

namespace AAMLib
{
    public class UserInfoLib
    {
        //Create Standalone SDK class dynamicly.
        public zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();

        public string sDevIPAddress = ""; //Device's ip address for connect
        public Int16 iDevPort = 4370; //Device's port for connect
        private bool bIsConnected = false; //the boolean value identifies whether the device is connected
        private int iMachineNumber = 1; //the serial number of the device.After connecting the device ,this value will be changed.

        public UserInfoLib (string DevIPAddress, Int16 DevPort)
        {
            sDevIPAddress = DevIPAddress;
            iDevPort = DevPort;
        }
        public List<UserInfo> GetUserInfos()
        {
            List<UserInfo> lUserInfo = new List<UserInfo>(); //Store result
            //string logPath = AppDomain.CurrentDomain.BaseDirectory; //Path to root directory of the app
            string data = "";
            LogUtilities logUtil = new LogUtilities();
            
            bIsConnected = axCZKEM1.Connect_Net(sDevIPAddress, iDevPort);
            if (bIsConnected)
            {
                data = string.Format("Connected to device <{0}:{1}>", sDevIPAddress, iDevPort);
                logUtil.WriteLog(data);
                iMachineNumber = 1;//In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
                axCZKEM1.RegEvent(iMachineNumber, 65535);//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
            }
            else
            {
                data = string.Format("Failed to connect to device <{0}:{1}>", sDevIPAddress, iDevPort);
                logUtil.WriteLog(data);
                return null;
            }

            string sdwEnrollNumber = "";
            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            bool bEnabled = false;

            int idwFingerIndex;
            string sTmpData = "";
            int iTmpLength = 0;
            int iFlag = 0;

            lUserInfo.Clear();
            axCZKEM1.EnableDevice(iMachineNumber, false);

            axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory
            axCZKEM1.ReadAllTemplate(iMachineNumber);//read all the users' fingerprint templates to the memory
            while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))//get all the users' information from the memory
            {
                for (idwFingerIndex = 0; idwFingerIndex < 10; idwFingerIndex++)
                {
                    if (axCZKEM1.GetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, out iFlag, out sTmpData, out iTmpLength))//get the corresponding templates string and length from the memory
                    {
                        UserInfo userInfo = new UserInfo { EnrollNumber = sdwEnrollNumber, Name = sName, FingerIndex = idwFingerIndex,
                            TmpData = sTmpData, Privilege = iPrivilege, Password = sPassword, Enable = bEnabled == true ? true : false,
                            Flag = iFlag };
                        lUserInfo.Add(userInfo);
                    }
                }
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);
            axCZKEM1.Disconnect();
            logUtil.WriteLog("User count: " + lUserInfo.Count());
            return lUserInfo;
        }
    }
}
