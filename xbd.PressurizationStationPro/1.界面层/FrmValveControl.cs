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
    public partial class FrmValveControl : Form
    {

        private string valveName;
        private bool state;
        private PlcDataService plcDataService;

        public FrmValveControl(string valveName,bool state,PlcDataService plcDataService)
        {
            InitializeComponent();

            this.valveName = valveName;
            this.state = state;
            this.plcDataService = plcDataService;


            this.TopMost = true;
            this.lbl_Message.Text = "是否确定要"+(this.state ? "关闭" : "打开") +"?";
        }

 
        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if(plcDataService.IsConnected)
            {
                bool result = true;
                switch (valveName)
                {
                    case "进水阀":
                        result = plcDataService.ValveInControl(!state);
                        break;
                    case "出水阀":
                        result = plcDataService.ValveOutControl(!state);
                    break;
                    default:
                        new FrmMsgNoAck("未知阀门名称！", "阀门控制").ShowDialog();
                        break;
                }
                if (result)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else 
                {
                    new FrmMsgNoAck("阀门控制失败，请检查！", "阀门控制").ShowDialog();
                }
            }
            else
            {
                new FrmMsgNoAck("请检查PLC连接是否正常！","阀门控制").ShowDialog();
            }
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

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
