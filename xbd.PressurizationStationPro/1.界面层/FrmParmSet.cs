using AForge.Video.DirectShow;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xbd.s7netplus;

namespace xbd.PressurizationStationPro
{
    public partial class FrmParmSet : Form
    {
        private SysInfo sysInfo;
        private SysInfoService infoService;
        private string sysInfoPath;


        public FrmParmSet()
        {
            InitializeComponent();
        }

        public FrmParmSet(SysInfo sysInfo, SysInfoService sysInfoService, string sysInfoPath)
        {
            InitializeComponent();
            this.sysInfo = sysInfo;
            this.infoService = sysInfoService;
            this.sysInfoPath = sysInfoPath;

            // 初始化界面控件的值
            this.cmb_CPUType.DataSource = Enum.GetNames(typeof(CpuType));

            FilterInfoCollection cameras = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach (FilterInfo item in cameras)
            {
                this.cmb_Camera.Items.Add(item.Name);
            }

            if (this.sysInfo != null)
            {
                this.txt_IPAddress.Text = this.sysInfo.IPAddress;
                this.cmb_CPUType.SelectedItem = this.sysInfo.CpuType.ToString();
                this.txt_Rack.Text = this.sysInfo.Rack.ToString();
                this.txt_Slot.Text = this.sysInfo.Slot.ToString();

                this.toggle_AutoStart.Checked = this.sysInfo.AutoStart;
                this.txt_ScreenTime.Text = this.sysInfo.ScreenTime.ToString();
                this.txt_LoginOutTime.Text = this.sysInfo.LogoffTime.ToString();

                if(cameras.Count> this.sysInfo.CameraIndex)
                {
                    this.cmb_Camera.SelectedIndex = this.sysInfo.CameraIndex;
                }

               
            }

            //自动启动开关事件绑定
            this.toggle_AutoStart.CheckedChanged += new System.EventHandler(this.toggle_AutoStart_CheckedChanged);
        }



        #region 无边框拖动 

        private Point mPoint;
        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint = new Point(e.X, e.Y);
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - mPoint.X, this.Location.Y + e.Y - mPoint.Y);
            }
        }
        #endregion

        private void btn_PLCSet_Click(object sender, EventArgs e)
        {
            if(this.sysInfo == null)
            {
                this.sysInfo = new SysInfo();
            }
            this.sysInfo.IPAddress = this.txt_IPAddress.Text.Trim();
            this.sysInfo.CpuType = (CpuType)Enum.Parse(typeof(CpuType), this.cmb_CPUType.Text.Trim());
            this.sysInfo.Rack = Convert.ToInt16(this.txt_Rack.Text.Trim());
            this.sysInfo.Slot = Convert.ToInt16(this.txt_Slot.Text.Trim());

            bool result = this.infoService.SetSysInfoToPath(this.sysInfo, this.sysInfoPath);

            if (result)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                new FrmMsgNoAck("通信参数写入失败！","通信参数").ShowDialog();
            }

        }

        private void btn_PLCCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btn_SysSet_Click(object sender, EventArgs e)
        {
            if (this.sysInfo == null)
            {
                this.sysInfo = new SysInfo();
            }
            this.sysInfo.AutoStart = this.toggle_AutoStart.Checked;
      
            this.sysInfo.ScreenTime = Convert.ToInt32(this.txt_ScreenTime.Text.Trim());
            this.sysInfo.LogoffTime = Convert.ToInt32(this.txt_LoginOutTime.Text.Trim());
            this.sysInfo.CameraIndex = this.cmb_Camera.SelectedIndex;

            bool result = this.infoService.SetSysInfoToPath(this.sysInfo, this.sysInfoPath);

            if (result)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                new FrmMsgNoAck("通信参数写入失败！", "通信参数").ShowDialog();
            }
        }

        private void btn_SysCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void lbl_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 开机启动
        /// <summary>  
        /// 修改程序在注册表中的键值  
        /// </summary>  
        /// <param name="isAuto">true:开机启动,false:不开机自启</param> 
        private void AutoStart(bool isAuto = true)
        {
            if (isAuto == true)
            {
                RegistryKey R_local = Registry.CurrentUser;
                RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                R_run.SetValue("PressurizationStationPro", System.Windows.Forms.Application.ExecutablePath);
                R_run.Close();
                R_local.Close();
            }
            else
            {
                RegistryKey R_local = Registry.CurrentUser;
                RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                R_run.DeleteValue("PressurizationStationPro", false);
                R_run.Close();
                R_local.Close();
            }
        }
        #endregion

        private void toggle_AutoStart_CheckedChanged(object sender, EventArgs e)
        {
            AutoStart(this.toggle_AutoStart.Checked);
        }
    }
}
