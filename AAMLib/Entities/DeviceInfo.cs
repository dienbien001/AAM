using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAMLib.Entities
{
    public class DeviceInfo
    {
        public Int16 DeviceID { get; set; }
        public string DeviceName { get; set; }
        public string DeviceIP { get; set; }
        public Int16 DevicePort { get; set; }
        public bool IsDownloading { get; set; }
        public DateTime LastAccessDateTime { get; set; }
        
    }
}
