namespace xbd.PressurizationStationPro
{
    partial class MeterShow
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_ParmName = new System.Windows.Forms.Label();
            this.lbl_ParmValue = new System.Windows.Forms.Label();
            this.meter_Parm = new xbd.ControlLib.xbdAnalogMeter();
            this.SuspendLayout();
            // 
            // lbl_ParmName
            // 
            this.lbl_ParmName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_ParmName.ForeColor = System.Drawing.Color.White;
            this.lbl_ParmName.Location = new System.Drawing.Point(0, 144);
            this.lbl_ParmName.Name = "lbl_ParmName";
            this.lbl_ParmName.Size = new System.Drawing.Size(147, 24);
            this.lbl_ParmName.TabIndex = 0;
            this.lbl_ParmName.Text = "出水温度";
            this.lbl_ParmName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ParmValue
            // 
            this.lbl_ParmValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lbl_ParmValue.Location = new System.Drawing.Point(36, 118);
            this.lbl_ParmValue.Name = "lbl_ParmValue";
            this.lbl_ParmValue.Size = new System.Drawing.Size(79, 26);
            this.lbl_ParmValue.TabIndex = 2;
            this.lbl_ParmValue.Text = "0.00 C";
            this.lbl_ParmValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // meter_Parm
            // 
            this.meter_Parm.BodyColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.meter_Parm.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.meter_Parm.Location = new System.Drawing.Point(2, 0);
            this.meter_Parm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.meter_Parm.MaxValue = 50D;
            this.meter_Parm.MinValue = 0D;
            this.meter_Parm.Name = "meter_Parm";
            this.meter_Parm.NeedleColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.meter_Parm.Renderer = null;
            this.meter_Parm.ScaleColor = System.Drawing.Color.White;
            this.meter_Parm.ScaleDivisions = 11;
            this.meter_Parm.ScaleSubDivisions = 4;
            this.meter_Parm.Size = new System.Drawing.Size(145, 143);
            this.meter_Parm.TabIndex = 1;
            this.meter_Parm.Value = 0D;
            this.meter_Parm.ViewGlass = false;
            // 
            // MeterShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.Controls.Add(this.lbl_ParmValue);
            this.Controls.Add(this.meter_Parm);
            this.Controls.Add(this.lbl_ParmName);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MeterShow";
            this.Size = new System.Drawing.Size(147, 168);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_ParmName;
        private ControlLib.xbdAnalogMeter meter_Parm;
        private System.Windows.Forms.Label lbl_ParmValue;
    }
}
