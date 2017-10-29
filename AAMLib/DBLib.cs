using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAMLib.Entities;
using System.Data.SqlClient;

namespace AAMLib
{
    public class DBLib
    {
        string connstr = "";
        //string connstr = "Data Source=THANHIT\\SQLEXPRESS;Initial Catalog=GHR_GreenCity_CC;Persist Security Info=True;User ID=sa;Password=12345678aA";
        LogUtilities logUtil = new LogUtilities();

        public DBLib(string ConnectionString)
        {
            connstr = ConnectionString;
        }
        public void InsertAttLog(List<AttendanceInfo> listAttLog)
        {
            Int64 LogCount = 0;
            logUtil.WriteLog("Begin insert data");

            foreach (AttendanceInfo att in listAttLog)
            {
                LogCount += AddAttLog(att.DeviceID, att.EnrollNumber, att.VerifyMode, att.InOutMode, att.Year, att.Month, att.Day, att.Hour, att.Minute, att.Second, att.WorkCode);
            }

            //logUtil.WriteLog("Insert completed. Number of records: " + LogCount.ToString());
            logUtil.WriteLog("Insert completed.");
        }
        private int AddAttLog(int DeviceID, string EnrollNumber, int VerifyMode, int InOutMode, int Year, int Month, int Day, int Hour, int Minute, int Second, int WorkCode)
        {
            int insertCount = 0; //Count inserted row

            using (SqlConnection con = new SqlConnection(connstr))
            {
                con.Open();
                try
                {
                    string sql = "SET NOCOUNT OFF; IF NOT EXISTS (SELECT * FROM NS_TL_CC_DuLieuChamCong " +
                                   "WHERE MayChamCongID = @MayChamCongID " +
                                   "AND ThoiGian = @ThoiGian " +
                                   "AND VaoRa = @VaoRa " +
                                   "AND MaChamCong = @MaChamCong) " +
                                   "BEGIN INSERT INTO NS_TL_CC_DuLieuChamCong(MayChamCongID, ThoiGian, VaoRa, NhapTay, MaChamCong, DuLieuKhongTongHop) " +
                                   "VALUES(@MayChamCongID, @ThoiGian, @VaoRa, @NhapTay, @MaChamCong, @DuLieuKhongTongHop) END";
                    //"INSERT INTO NS_TL_CC_DuLieuChamCong (MayChamCongID, ThoiGian, VaoRa, NhapTay, MaChamCong)"
                    //+ " VALUES(@MayChamCongID, @ThoiGian, @VaoRa, @NhapTay, @MaChamCong)"
                    //+ " WHERE not exists(Select null from NS_TL_CC_DuLieuChamCong "
                    //+ " where MayChamCongID = @MayChamCongID and ThoiGian = @ThoiGian and VaoRa = @VaoRa and MaChamCong = @MaChamCong)"

                    using (SqlCommand command = new SqlCommand(sql, con))
                    {
                        string ThoiGian = Year.ToString() + "-" + Month.ToString() + "-" + Day.ToString() + " " + Hour.ToString() + ":" + Minute.ToString() + ":" + Second.ToString();
                        command.Parameters.Add(new SqlParameter("MayChamCongID", DeviceID));
                        command.Parameters.Add(new SqlParameter("ThoiGian", ThoiGian));
                        command.Parameters.Add(new SqlParameter("VaoRa", InOutMode));
                        command.Parameters.Add(new SqlParameter("NhapTay", "0")); //Khả năng là trường hợp thêm record thủ công thông qua phần mềm OOS
                        command.Parameters.Add(new SqlParameter("DuLieuKhongTongHop", "0")); //Chưa biết để làm gì
                        command.Parameters.Add(new SqlParameter("MaChamCong", EnrollNumber));
                        insertCount += command.ExecuteNonQuery();
                    }
                }
                catch (SqlException Ex)
                {
                    logUtil.WriteLog("Error: " + Ex.Message.ToString());
                    return 0;
                }
                catch (Exception Ex)
                {
                    logUtil.WriteLog("Error: " + Ex.Message.ToString());
                    return 0;
                }
            }
            return insertCount;
        }
        public void ExecStoreUpdateNhanVienID()
        {
            using (SqlConnection con = new SqlConnection(connstr))
            {
                con.Open();
                try
                {
                    string sql = "DECLARE @RC int EXECUTE @RC = [dbo].[UpdateNhanVienID] ";

                    using (SqlCommand command = new SqlCommand(sql, con))
                    {
                        command.ExecuteNonQuery();
                    }
                    logUtil.WriteLog("Successful update NhanVienID");
                }
                catch (Exception Ex)
                {
                    logUtil.WriteLog("Error update NhanVienID: " + Ex.Message.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
        
    }
}
