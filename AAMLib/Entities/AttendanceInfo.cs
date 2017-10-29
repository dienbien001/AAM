using System;
using System.Globalization;

namespace AAMLib.Entities
{
    public class AttendanceInfo
    {
        public int DeviceID { get; set; } //ID máy chấm công
        public string EnrollNumber { get; set; } //Mã chấm công
        public int VerifyMode { get; set; } // 0 - Vân tay, 10 - Password
        public int InOutMode { get; set; } //Check in/Check out
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public int WorkCode { get; set; }
        public DateTime EnrollDateTime { get; set; } //Ngày giờ check
        
    }
}
