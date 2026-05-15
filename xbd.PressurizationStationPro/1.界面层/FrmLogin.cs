using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xbd.PressurizationStationPro
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();

            this.Load += FrmLogin_OnLoad;
        }

        private SysAdminService sysAdminService = new SysAdminService();
        private void FrmLogin_OnLoad(object sender, EventArgs args)
        {
            var sysAdmins = sysAdminService.QuerySysAdmins();
            if (sysAdmins.Count > 0)
            {
                foreach (var sysAdmin in sysAdmins)
                {
                    this.cmb_User.Items.Add(sysAdmin.LoginName);
                }
                this.cmb_User.SelectedIndex = 0;
            }
            else
            {
                new FrmMsgNoAck("没有管理员账号，请先创建管理员账号！","登录提示").ShowDialog();
            }
        }

        private void lbl_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (this.cmb_User.Text.Trim().Length == 0)
            {
                new FrmMsgNoAck("请输入用户名！", "登录提示").ShowDialog();
                return;
            }

            if (this.tex_Pwd.Text.Trim().Length == 0)
            {
                new FrmMsgNoAck("请输入密码！","登录提示").ShowDialog();
                return;
            }

            SysAdmin admin = new SysAdmin();
            admin.LoginName = this.cmb_User.Text.Trim();
            admin.Password = this.tex_Pwd.Text.Trim();

            admin = sysAdminService.AdminLogin(admin);

            if (admin == null) 
            { 
                new FrmMsgNoAck("用户名或密码错误！", "登录提示").ShowDialog();
            }
            else
            {
                //存储用户数据
                Program.CurrentUser = admin;

                this.DialogResult = DialogResult.OK;
            }
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
    }
}
