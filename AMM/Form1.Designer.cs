namespace AMM
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.c1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnGetAttLog = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDeviceID = new System.Windows.Forms.TextBox();
            this.listDevice = new System.Windows.Forms.ListView();
            this.MayChamCongID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Ten = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Port = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnGetDeviceList = new System.Windows.Forms.Button();
            this.IsDownloading = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnGetAttLogFromDevice = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(192, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Get User Info";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(86, 12);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(100, 20);
            this.txtIP.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Port";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(86, 38);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 20);
            this.txtPort.TabIndex = 3;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.c1,
            this.c2,
            this.c3,
            this.c4,
            this.c5,
            this.c6,
            this.c7,
            this.c8});
            this.listView1.Location = new System.Drawing.Point(289, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(483, 110);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // c1
            // 
            this.c1.Text = "UserID";
            // 
            // c2
            // 
            this.c2.Text = "Name";
            // 
            // c3
            // 
            this.c3.Text = "FingerIndex";
            this.c3.Width = 79;
            // 
            // c4
            // 
            this.c4.Text = "tmpData";
            // 
            // c5
            // 
            this.c5.Text = "Privilege";
            // 
            // c6
            // 
            this.c6.Text = "Password";
            // 
            // c7
            // 
            this.c7.Text = "Enabled";
            // 
            // c8
            // 
            this.c8.Text = "Flag";
            this.c8.Width = 36;
            // 
            // btnGetAttLog
            // 
            this.btnGetAttLog.Location = new System.Drawing.Point(192, 35);
            this.btnGetAttLog.Name = "btnGetAttLog";
            this.btnGetAttLog.Size = new System.Drawing.Size(91, 23);
            this.btnGetAttLog.TabIndex = 6;
            this.btnGetAttLog.Text = "Get Att Log";
            this.btnGetAttLog.UseVisualStyleBackColor = true;
            this.btnGetAttLog.Click += new System.EventHandler(this.btnGetAttLog_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "DeviceID";
            // 
            // txtDeviceID
            // 
            this.txtDeviceID.Location = new System.Drawing.Point(86, 64);
            this.txtDeviceID.Name = "txtDeviceID";
            this.txtDeviceID.Size = new System.Drawing.Size(100, 20);
            this.txtDeviceID.TabIndex = 7;
            // 
            // listDevice
            // 
            this.listDevice.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.MayChamCongID,
            this.Ten,
            this.IP,
            this.Port,
            this.IsDownloading});
            this.listDevice.Location = new System.Drawing.Point(277, 190);
            this.listDevice.Name = "listDevice";
            this.listDevice.Size = new System.Drawing.Size(483, 182);
            this.listDevice.TabIndex = 9;
            this.listDevice.UseCompatibleStateImageBehavior = false;
            this.listDevice.View = System.Windows.Forms.View.Details;
            // 
            // MayChamCongID
            // 
            this.MayChamCongID.Text = "MayChamCongID";
            // 
            // Ten
            // 
            this.Ten.Text = "Ten";
            // 
            // IP
            // 
            this.IP.Text = "IP";
            this.IP.Width = 79;
            // 
            // Port
            // 
            this.Port.Text = "Port";
            // 
            // btnGetDeviceList
            // 
            this.btnGetDeviceList.Location = new System.Drawing.Point(171, 190);
            this.btnGetDeviceList.Name = "btnGetDeviceList";
            this.btnGetDeviceList.Size = new System.Drawing.Size(91, 23);
            this.btnGetDeviceList.TabIndex = 10;
            this.btnGetDeviceList.Text = "GetDeviceList";
            this.btnGetDeviceList.UseVisualStyleBackColor = true;
            this.btnGetDeviceList.Click += new System.EventHandler(this.btnGetDeviceList_Click);
            // 
            // IsDownloading
            // 
            this.IsDownloading.Text = "IsDownloading";
            this.IsDownloading.Width = 91;
            // 
            // btnGetAttLogFromDevice
            // 
            this.btnGetAttLogFromDevice.Location = new System.Drawing.Point(171, 238);
            this.btnGetAttLogFromDevice.Name = "btnGetAttLogFromDevice";
            this.btnGetAttLogFromDevice.Size = new System.Drawing.Size(91, 43);
            this.btnGetAttLogFromDevice.TabIndex = 11;
            this.btnGetAttLogFromDevice.Text = "Get Att logs from device";
            this.btnGetAttLogFromDevice.UseVisualStyleBackColor = true;
            this.btnGetAttLogFromDevice.Click += new System.EventHandler(this.btnGetAttLogFromDevice_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 491);
            this.Controls.Add(this.btnGetAttLogFromDevice);
            this.Controls.Add(this.btnGetDeviceList);
            this.Controls.Add(this.listDevice);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDeviceID);
            this.Controls.Add(this.btnGetAttLog);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader c1;
        private System.Windows.Forms.ColumnHeader c2;
        private System.Windows.Forms.ColumnHeader c3;
        private System.Windows.Forms.ColumnHeader c4;
        private System.Windows.Forms.ColumnHeader c5;
        private System.Windows.Forms.ColumnHeader c6;
        private System.Windows.Forms.ColumnHeader c7;
        private System.Windows.Forms.ColumnHeader c8;
        private System.Windows.Forms.Button btnGetAttLog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDeviceID;
        private System.Windows.Forms.ListView listDevice;
        private System.Windows.Forms.ColumnHeader MayChamCongID;
        private System.Windows.Forms.ColumnHeader Ten;
        private System.Windows.Forms.ColumnHeader IP;
        private System.Windows.Forms.ColumnHeader Port;
        private System.Windows.Forms.ColumnHeader IsDownloading;
        private System.Windows.Forms.Button btnGetDeviceList;
        private System.Windows.Forms.Button btnGetAttLogFromDevice;
    }
}

