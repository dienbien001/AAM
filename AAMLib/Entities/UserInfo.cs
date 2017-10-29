using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAMLib.Entities
{
    public class UserInfo
    {
        public string EnrollNumber { get; set; } //Mã chấm công
        public string Name { get; set; }
        public string Password { get; set; }
        public int Privilege { get; set; } //Quyền trên máy chấm công: 0 - User, 1 - Admin
        public bool Enable { get; set; } //Còn hoạt động hay không
        public int FingerIndex { get; set; } //Thứ tự ngón tay
        public string TmpData { get; set; } //Template vân tay
        public int Flag { get; set; }
    }
}
