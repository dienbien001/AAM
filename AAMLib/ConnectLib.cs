using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAMLib
{
    public class ConnectLib
    {
        //Create Standalone SDK class dynamicly.
        public zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();

        public bool ConnectDev(string DevipAddress, Int16 devPort)
        {
            return axCZKEM1.Connect_Net(DevipAddress, devPort);
        }
        public void DisconnectDev()
        {
            axCZKEM1.Disconnect();
        }
    }
}
