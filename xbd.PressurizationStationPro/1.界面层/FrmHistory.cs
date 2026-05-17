using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private List<HistoryData> historyDataList ;
        public FrmHistory()
        {
            InitializeComponent();

            this.dgv_HistoryData.AutoGenerateColumns = false;
            this.dgv_HistoryData.Columns[0].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";

            this.dtp_Start.Value = DateTime.Now.AddHours(-2);
            this.dtp_End.Value = DateTime.Now;

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
                if (res.Content.Count > 0)
                {
                    historyDataList = res.Content;
                    dgv_HistoryData.DataSource = null;
                    dgv_HistoryData.DataSource = res.Content;
                }
                else
                {
                    new FrmMsgNoAck("没有查询到历史数据！", "查询提示").ShowDialog(); 
                }
                   
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

        private void dgv_HistoryData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewHelper.DgvRowPostPaint(this.dgv_HistoryData, e);
        }

        private void btn_Export(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            //标题
            saveFileDialog.Title = "请选择文件";
            saveFileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx|CSV文件(*.csv)|*.csv";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = "数据记录_" + this.dtp_Start.Value.ToString("yyyyMMddHHmmSS") +"_"+this.dtp_End.Value.ToString("yyyyMMddHHmmSS ");


            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    MiniExcel.SaveAs(saveFileDialog.FileName, historyDataList);
                    Process.Start(saveFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    new FrmMsgNoAck("导出文件失败！" + ex.Message, "导出数据").ShowDialog();
                }
            }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            DataGridViewHelper.Print_DataGridView(this.dgv_HistoryData);
        }
    }
}
