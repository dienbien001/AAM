using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAMLib.Entities;
using System.Data.SqlClient;
using System.Data;

namespace AAMLib
{
    public class DevideInfoLib
    {
        private string ConnStr = "";
        LogUtilities logUtil = new LogUtilities();

        public DevideInfoLib (string ConnectionString)
        {
            ConnStr = ConnectionString;
        }
        public List<DeviceInfo> GetListDevice()
        {
            List<DeviceInfo> listDevice = new List<DeviceInfo>();

            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                con.Open();
                try
                {
                    string sql = "SELECT MayChamCongID,Ten,IP,Port,IsDownLoading,DuLieuDocLanCuoi FROM NS_TL_CC_MayChamCong";

                    using (SqlCommand command = new SqlCommand(sql, con))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                DeviceInfo device = new DeviceInfo();
                                device.DeviceID = Convert.ToInt16(dr["MayChamCongID"]);
                                device.DeviceName = (dr["Ten"].ToString());
                                device.DeviceIP = (dr["IP"].ToString());
                                device.DevicePort = Convert.ToInt16(dr["Port"]);
                                device.IsDownloading = Convert.ToBoolean(dr["IsDownLoading"]);
                                device.LastAccessDateTime = DateTime.Parse(dr["DuLieuDocLanCuoi"].ToString());

                                listDevice.Add(device);
                            }
                        }
                    }
                    logUtil.WriteLog("Successful get list of attendance device: " + listDevice.Count + " device(s)");
                }
                catch (Exception Ex)
                {
                    logUtil.WriteLog("Error getting device list: " + Ex.Message.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
            return listDevice;
        }
        public void Update_DuLieuDocLanCuoi(int DeviceID, DateTime LastAccess)
        {
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                con.Open();
                try
                {
                    string sql = "Update NS_TL_CC_MayChamCong Set DuLieuDocLanCuoi = " + "'" + LastAccess.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where MayChamCongID = " + DeviceID.ToString();

                    using (SqlCommand command = new SqlCommand(sql, con))
                    {
                        command.ExecuteNonQuery();
                    }
                    logUtil.WriteLog("Successful update DuLieuDocLanCuoi");
                }
                catch (Exception Ex)
                {
                    logUtil.WriteLog("Error update DuLieuDocLanCuoi: " + Ex.Message.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}
