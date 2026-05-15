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
    public partial class FrmHistory : Form
    {

        private HistoryDataService historyDataService = new HistoryDataService();
        public FrmHistory()
        {
            InitializeComponent();
            this.TopMost = true;
        }

 
        private void btn_QueryHistory_Click(object sender, EventArgs e)
        {
            if(dtp_Start.Value > dtp_End.Value)
            {
                new FrmMsgNoAck("开始时间不能大于结束时间！", "查询提示").ShowDialog();
                return;
            }
            var res = historyDataService.GetHistoryDataByTime(this.dtp_Start.Value, this.dtp_End.Value);

            if(res.IsSuccess)
            {
                dgv_HistoryData.DataSource = res.Content;
            }
            else
            {
                new FrmMsgNoAck("查询历史数据失败！"+ res.Message, "查询提示").ShowDialog();
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
    }
}
