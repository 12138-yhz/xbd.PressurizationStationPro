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
    public partial class FrmUserManager : Form
    {
        private SysAdminService sysAdminService = new SysAdminService();
        private List<SysAdmin> sysAdmins;
        public FrmUserManager()
        {
            InitializeComponent();

            this.dgv_User.AutoGenerateColumns = false;

            this.Load += FrmUserManager_Load;
        }

        private void FrmUserManager_Load(object sender, EventArgs e)
        {
            this.cmb_RoleName.Items.Clear();
            this.cmb_RoleName.Items.AddRange(Enum.GetNames(typeof(RoleName)));

            RefreshUserList();

            if(sysAdmins.Count > 0)
            {
                UpdateUserInfo(sysAdmins.First());
            }
        }


        /// <summary>
        /// 刷新数据
        /// </summary>
        private void RefreshUserList()
        {
            sysAdmins = sysAdminService.QuerySysAdmins();
            if (sysAdmins.Count > 0)
            {
                this.dgv_User.DataSource = null;
                this.dgv_User.DataSource = sysAdmins;
            }
            else
            {
                this.dgv_User.DataSource = null;

            }
        }

        private void UpdateUserInfo(SysAdmin sysAdmin)
        {
            this.txt_UserName.Text = sysAdmin.LoginName;
            this.txt_UserPwd.Text = StringSecurityHelper.DESDecrypt(sysAdmin.LoginPwd);
            this.cmb_RoleName.SelectedItem = sysAdmin.RoleName.ToString();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void lbl_Exit_Click(object sender, EventArgs e)
        {
            Close();
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


        private void dgv_User_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewHelper.DgvRowPostPaint(this.dgv_User, e);
        }

        private void btn_AddUser_Click(object sender, EventArgs e)
        {
            //非空判断
            if (string.IsNullOrWhiteSpace(this.txt_UserName.Text))
            {
                new FrmMsgNoAck("用户名不能为空！", "增加用户").ShowDialog();
                return;
            }
            if(string.IsNullOrWhiteSpace(this.txt_UserPwd.Text))
            {
                new FrmMsgNoAck("用户密码不能为空！", "增加用户").ShowDialog();
                return;
            }
            if(string.IsNullOrWhiteSpace(this.cmb_RoleName.Text))
            {
                new FrmMsgNoAck("用户角色不能为空！", "增加用户").ShowDialog();
                return;
            }

            //是否存在同名用户

            if(sysAdmins.Any(s => s.LoginName == this.txt_UserName.Text.Trim()))
            {
                    new FrmMsgNoAck("已存在同名用户！", "增加用户").ShowDialog();
                    return;
            }

            //封装对象
            SysAdmin sysAdmin = new SysAdmin()
            {
                LoginName = this.txt_UserName.Text.Trim(),
                LoginPwd =StringSecurityHelper.DESEncrypt(this.txt_UserPwd.Text.Trim()),
                RoleName = (RoleName)Enum.Parse(typeof(RoleName), this.cmb_RoleName.Text.Trim())
            };

            //4.添加用户
            if(sysAdminService.AddSysAdmin(sysAdmin))
            {
               // new FrmMsgNoAck("添加用户成功！", "增加用户").ShowDialog();
                RefreshUserList();
            }
            else
            {
                new FrmMsgNoAck("添加用户失败！", "增加用户").ShowDialog();
            }
        }

        private void btn_UpdateUser_Click(object sender, EventArgs e)
        {
            //非空判断
            if (string.IsNullOrWhiteSpace(this.txt_UserName.Text))
            {
                new FrmMsgNoAck("用户名不能为空！", "修改用户").ShowDialog();
                return;
            }
            if (string.IsNullOrWhiteSpace(this.txt_UserPwd.Text))
            {
                new FrmMsgNoAck("用户密码不能为空！", "修改用户").ShowDialog();
                return;
            }
            if (string.IsNullOrWhiteSpace(this.cmb_RoleName.Text))
            {
                new FrmMsgNoAck("用户角色不能为空！", "修改用户").ShowDialog();
                return;
            }

            //封装对象
            SysAdmin sysAdmin = new SysAdmin()
            {
                LoginId = sysAdmins[this.dgv_User.SelectedRows[0].Index].LoginId,
                LoginName = this.txt_UserName.Text.Trim(),
                LoginPwd = this.txt_UserPwd.Text.Trim(),
                RoleName = (RoleName)Enum.Parse(typeof(RoleName), this.cmb_RoleName.Text.Trim())
            };

            //4.修改用户
            if (sysAdminService.ModifySysAdmin(sysAdmin))
            {
                // new FrmMsgNoAck("修改用户成功！", "修改用户").ShowDialog();
                RefreshUserList();
            }
            else
            {
                new FrmMsgNoAck("修改用户失败！", "修改用户").ShowDialog();
            }
        }

        private void btn_DeleteUser_Click(object sender, EventArgs e)
        {
            if(this.dgv_User.SelectedRows.Count > 0)
            {
              int loginId = sysAdmins[this.dgv_User.SelectedRows[0].Index].LoginId;
                if(sysAdminService.DeleteSysAdmin(loginId))
                {
                    //new FrmMsgNoAck("删除用户成功！", "删除用户").ShowDialog();
                    RefreshUserList();
                }
                else
                {
                    new FrmMsgNoAck("删除用户失败！", "删除用户").ShowDialog();
                }
            }
        }

        private void dgv_User_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {

                UpdateUserInfo(sysAdmins[e.RowIndex]);
                
            }
        }

        private void dgv_User_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if(e.ColumnIndex == 1 && e.Value != null)
                {
                    e.Value = StringSecurityHelper.DESDecrypt(e.Value.ToString());
                }
            }
        }
    }
}
