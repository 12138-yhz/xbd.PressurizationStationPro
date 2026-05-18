namespace xbd.PressurizationStationPro
{
    partial class FrmUserManager
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUserManager));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.lbl_Exit = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.dgv_User = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.用户ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_DeleteUser = new System.Windows.Forms.Button();
            this.btn_UpdateUser = new System.Windows.Forms.Button();
            this.btn_AddUser = new System.Windows.Forms.Button();
            this.cmb_RoleName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_UserPwd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_UserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_User)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_Title);
            this.panel1.Controls.Add(this.lbl_Exit);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(625, 46);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseMove);
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Title.ForeColor = System.Drawing.Color.White;
            this.lbl_Title.Location = new System.Drawing.Point(45, 9);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(88, 26);
            this.lbl_Title.TabIndex = 3;
            this.lbl_Title.Text = "用户管理";
            // 
            // lbl_Exit
            // 
            this.lbl_Exit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_Exit.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lbl_Exit.ForeColor = System.Drawing.Color.White;
            this.lbl_Exit.Location = new System.Drawing.Point(576, 5);
            this.lbl_Exit.Name = "lbl_Exit";
            this.lbl_Exit.Size = new System.Drawing.Size(46, 37);
            this.lbl_Exit.TabIndex = 2;
            this.lbl_Exit.Text = "X";
            this.lbl_Exit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Exit.Click += new System.EventHandler(this.lbl_Exit_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::xbd.PressurizationStationPro.Properties.Resources.User_png;
            this.pictureBox1.Location = new System.Drawing.Point(5, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 32);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(625, 1);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.mainPanel.Controls.Add(this.dgv_User);
            this.mainPanel.Controls.Add(this.btn_DeleteUser);
            this.mainPanel.Controls.Add(this.btn_UpdateUser);
            this.mainPanel.Controls.Add(this.btn_AddUser);
            this.mainPanel.Controls.Add(this.cmb_RoleName);
            this.mainPanel.Controls.Add(this.label4);
            this.mainPanel.Controls.Add(this.txt_UserPwd);
            this.mainPanel.Controls.Add(this.label3);
            this.mainPanel.Controls.Add(this.txt_UserName);
            this.mainPanel.Controls.Add(this.label2);
            this.mainPanel.Controls.Add(this.panel1);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mainPanel.Location = new System.Drawing.Point(1, 1);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(1);
            this.mainPanel.Size = new System.Drawing.Size(627, 495);
            this.mainPanel.TabIndex = 1;
            // 
            // dgv_User
            // 
            this.dgv_User.AllowUserToAddRows = false;
            this.dgv_User.AllowUserToDeleteRows = false;
            this.dgv_User.AllowUserToResizeColumns = false;
            this.dgv_User.AllowUserToResizeRows = false;
            this.dgv_User.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.dgv_User.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_User.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_User.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_User.ColumnHeadersHeight = 35;
            this.dgv_User.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_User.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column11,
            this.Column10,
            this.用户ID});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_User.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_User.EnableHeadersVisualStyles = false;
            this.dgv_User.Location = new System.Drawing.Point(35, 235);
            this.dgv_User.MultiSelect = false;
            this.dgv_User.Name = "dgv_User";
            this.dgv_User.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_User.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_User.RowHeadersWidth = 55;
            this.dgv_User.RowTemplate.Height = 35;
            this.dgv_User.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_User.Size = new System.Drawing.Size(561, 244);
            this.dgv_User.TabIndex = 10;
            this.dgv_User.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_User_CellClick);
            this.dgv_User.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_User_CellFormatting);
            this.dgv_User.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgv_User_RowPostPaint);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "LoginName";
            this.Column1.HeaderText = "用户名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 150;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "LoginPwd";
            this.Column11.HeaderText = "用户密码";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column11.Width = 180;
            // 
            // Column10
            // 
            this.Column10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column10.DataPropertyName = "RoleName";
            this.Column10.HeaderText = "用户角色";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 用户ID
            // 
            this.用户ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.用户ID.DataPropertyName = "LoginId";
            this.用户ID.HeaderText = "用户Id";
            this.用户ID.Name = "用户ID";
            this.用户ID.ReadOnly = true;
            this.用户ID.Visible = false;
            // 
            // btn_DeleteUser
            // 
            this.btn_DeleteUser.BackColor = System.Drawing.Color.Transparent;
            this.btn_DeleteUser.BackgroundImage = global::xbd.PressurizationStationPro.Properties.Resources.Yellow_png;
            this.btn_DeleteUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_DeleteUser.FlatAppearance.BorderSize = 0;
            this.btn_DeleteUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DeleteUser.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_DeleteUser.Location = new System.Drawing.Point(379, 189);
            this.btn_DeleteUser.Margin = new System.Windows.Forms.Padding(0);
            this.btn_DeleteUser.Name = "btn_DeleteUser";
            this.btn_DeleteUser.Size = new System.Drawing.Size(90, 30);
            this.btn_DeleteUser.TabIndex = 9;
            this.btn_DeleteUser.Text = "删除用户";
            this.btn_DeleteUser.UseVisualStyleBackColor = false;
            this.btn_DeleteUser.Click += new System.EventHandler(this.btn_DeleteUser_Click);
            // 
            // btn_UpdateUser
            // 
            this.btn_UpdateUser.BackColor = System.Drawing.Color.Transparent;
            this.btn_UpdateUser.BackgroundImage = global::xbd.PressurizationStationPro.Properties.Resources.Pink_png;
            this.btn_UpdateUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_UpdateUser.FlatAppearance.BorderSize = 0;
            this.btn_UpdateUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_UpdateUser.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_UpdateUser.Location = new System.Drawing.Point(379, 132);
            this.btn_UpdateUser.Margin = new System.Windows.Forms.Padding(0);
            this.btn_UpdateUser.Name = "btn_UpdateUser";
            this.btn_UpdateUser.Size = new System.Drawing.Size(90, 30);
            this.btn_UpdateUser.TabIndex = 9;
            this.btn_UpdateUser.Text = "修改用户";
            this.btn_UpdateUser.UseVisualStyleBackColor = false;
            this.btn_UpdateUser.Click += new System.EventHandler(this.btn_UpdateUser_Click);
            // 
            // btn_AddUser
            // 
            this.btn_AddUser.BackColor = System.Drawing.Color.Transparent;
            this.btn_AddUser.BackgroundImage = global::xbd.PressurizationStationPro.Properties.Resources.Green_png;
            this.btn_AddUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_AddUser.FlatAppearance.BorderSize = 0;
            this.btn_AddUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddUser.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_AddUser.Location = new System.Drawing.Point(379, 75);
            this.btn_AddUser.Margin = new System.Windows.Forms.Padding(0);
            this.btn_AddUser.Name = "btn_AddUser";
            this.btn_AddUser.Size = new System.Drawing.Size(90, 30);
            this.btn_AddUser.TabIndex = 9;
            this.btn_AddUser.Text = "添加用户";
            this.btn_AddUser.UseVisualStyleBackColor = false;
            this.btn_AddUser.Click += new System.EventHandler(this.btn_AddUser_Click);
            // 
            // cmb_RoleName
            // 
            this.cmb_RoleName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.cmb_RoleName.FormattingEnabled = true;
            this.cmb_RoleName.Location = new System.Drawing.Point(210, 189);
            this.cmb_RoleName.Name = "cmb_RoleName";
            this.cmb_RoleName.Size = new System.Drawing.Size(140, 29);
            this.cmb_RoleName.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(114, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 21);
            this.label4.TabIndex = 7;
            this.label4.Text = "用户权限：";
            // 
            // txt_UserPwd
            // 
            this.txt_UserPwd.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txt_UserPwd.Location = new System.Drawing.Point(210, 132);
            this.txt_UserPwd.Name = "txt_UserPwd";
            this.txt_UserPwd.PasswordChar = '*';
            this.txt_UserPwd.Size = new System.Drawing.Size(140, 29);
            this.txt_UserPwd.TabIndex = 6;
            this.txt_UserPwd.UseSystemPasswordChar = true;
            this.txt_UserPwd.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(114, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "用户密码：";
            // 
            // txt_UserName
            // 
            this.txt_UserName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txt_UserName.Location = new System.Drawing.Point(210, 73);
            this.txt_UserName.Name = "txt_UserName";
            this.txt_UserName.Size = new System.Drawing.Size(140, 29);
            this.txt_UserName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(114, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "用户名称：";
            // 
            // FrmUserManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(629, 497);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmUserManager";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户管理";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_User)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_Exit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_UserPwd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_UserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_RoleName;
        private System.Windows.Forms.Button btn_AddUser;
        private System.Windows.Forms.Button btn_DeleteUser;
        private System.Windows.Forms.Button btn_UpdateUser;
        private System.Windows.Forms.DataGridView dgv_User;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn 用户ID;
    }
}