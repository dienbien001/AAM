using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AAMLib;
using AAMLib.Abtracts;
using AAMLib.Entities;
using System.Configuration;

namespace AMM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        //Create Standalone SDK class dynamicly.
        private AAMLib.ConnectLib connLib = new ConnectLib();
        private Boolean bIsConnected = false;
        string ConnStr = "";//"Data Source=172.16.20.235\\SQL_GHR;Initial Catalog=GHR_GreenCity;Persist Security Info=True;User ID=test;Password=12345678aA";
        string ConnStrCC = "";//"Data Source=172.16.20.235\\SQL_GHR;Initial Catalog=GHR_GreenCity;Persist Security Info=True;User ID=test;Password=12345678aA";

        private void button1_Click(object sender, EventArgs e)
        {
            bIsConnected = connLib.ConnectDev(txtIP.Text, Convert.ToInt16(txtPort.Text));
            Console.WriteLine("Connected status: " + bIsConnected.ToString());
            connLib.DisconnectDev();
            Console.WriteLine("Connected status: Disconnected");

            UserInfoLib userInfoLib = new UserInfoLib(txtIP.Text, Convert.ToInt16(txtPort.Text));
            List<UserInfo> lUserInfo = userInfoLib.GetUserInfos();
            if (lUserInfo.Count() > 0)
            {
                for (int i = 0; i < lUserInfo.Count; i++)
                {
                    ListViewItem list = new ListViewItem();
                    list.Text = lUserInfo[i].EnrollNumber;
                    list.SubItems.Add(lUserInfo[i].Name);
                    list.SubItems.Add(lUserInfo[i].FingerIndex.ToString());
                    list.SubItems.Add(lUserInfo[i].TmpData);
                    list.SubItems.Add(lUserInfo[i].Privilege.ToString());
                    list.SubItems.Add(lUserInfo[i].Password);
                    list.SubItems.Add(lUserInfo[i].Enable.ToString());
                    list.SubItems.Add(lUserInfo[i].Flag.ToString());

                    listView1.Items.Add(list);
                }
            }
            Console.WriteLine("User count: " + lUserInfo.Count());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtIP.Text = "172.16.68.174";
            txtPort.Text = "4370";
            txtDeviceID.Text = "1";
        }

        private void btnGetAttLog_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;

            bIsConnected = connLib.ConnectDev(txtIP.Text, Convert.ToInt16(txtPort.Text));
            Console.WriteLine("Connected status: " + bIsConnected.ToString());
            connLib.DisconnectDev();
            Console.WriteLine("Connected status: Disconnected");

            UserAttLib attendanceLib = new UserAttLib("DeviceName",txtIP.Text, Convert.ToInt16(txtPort.Text), Convert.ToInt16(txtDeviceID.Text), date);

            List<AttendanceInfo> lAttInfo = attendanceLib.GetAttInfoByDate();

            if (lAttInfo.Count() > 0)
            {
                Console.WriteLine("Total att logs count: " + lAttInfo.Count());
                DBLib dbLib = new DBLib("");
                dbLib.InsertAttLog(lAttInfo);
                MessageBox.Show("Insert completed");
            }
        }

        private void btnGetDeviceList_Click(object sender, EventArgs e)
        {
            listDevice.Items.Clear();
            
            Int32 interval = 24 * 60 * 60; //Check interval (milliseconds)
            //Read configuration
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                ConnStr = appSettings["ConnectionString"] ?? "";
                ConnStrCC = appSettings["ConnectionStringCC"] ?? "";
                interval = Convert.ToInt32(appSettings["Interval"]);

                if (ConnStr.Length > 0 && ConnStrCC.Length > 0)
                {
                    Console.WriteLine("Read configuration successful");
                }
                else
                {
                    Console.WriteLine("Configuration error");
                }
            }
            catch
            {
                Console.WriteLine("Read configuration failed");
            }

            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                con.Open();
                try
                {
                    string sql = "SELECT MayChamCongID,Ten,IP,Port,IsDownLoading FROM NS_TL_CC_MayChamCong";

                    using (SqlCommand command = new SqlCommand(sql, con))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                ListViewItem list = new ListViewItem();
                                list.Text = dr["MayChamCongID"].ToString();
                                list.SubItems.Add(dr["Ten"].ToString());
                                list.SubItems.Add(dr["IP"].ToString());
                                list.SubItems.Add(dr["Port"].ToString());
                                list.SubItems.Add(dr["IsDownLoading"].ToString());

                                listDevice.Items.Add(list);
                            }

                        }
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void btnGetAttLogFromDevice_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now.AddDays(-2);

            if (listDevice.Items.Count > 0)
            {
                foreach (ListViewItem item in listDevice.Items)
                {
                    UserAttLib attendanceLib = new UserAttLib(item.SubItems[1].Text, item.SubItems[2].Text, Convert.ToInt16(item.SubItems[3].Text), Convert.ToInt16(item.SubItems[0].Text), date);

                    List<AttendanceInfo> lAttInfo = attendanceLib.GetAttInfoByDate();

                    if (lAttInfo == null) { continue; }

                    if (lAttInfo.Count() > 0)
                    {
                        Console.WriteLine("Total att logs count: " + lAttInfo.Count());
                        DBLib dbLib = new DBLib("");
                        //dbLib.InsertAttLog(lAttInfo);
                    }
                }

                using (SqlConnection con = new SqlConnection(ConnStrCC))
                {
                    con.Open();
                    try
                    {
                        string sql = "DECLARE @RC int EXECUTE @RC = [dbo].[UpdateNhanVienID] ";
                        
                        using (SqlCommand command = new SqlCommand(sql, con))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message.ToString());
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }
    }
}
