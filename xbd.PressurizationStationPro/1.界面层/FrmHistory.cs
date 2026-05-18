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
        private readonly HistoryDataService historyDataService = new HistoryDataService();
        // 工业规范：初始化为泛型空列表，彻底杜绝 Null 闪退风险
        private List<HistoryData> historyDataList = new List<HistoryData>();

        public FrmHistory()
        {
            InitializeComponent();

            this.dgv_HistoryData.AutoGenerateColumns = false;

            // 工业优化：如果列数较多，强烈建议在这里开启 DataGridView 的双缓冲（利用反射）
            typeof(DataGridView).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(dgv_HistoryData, true, null);

            if (this.dgv_HistoryData.Columns.Count > 0)
            {
                this.dgv_HistoryData.Columns[0].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            }

            this.dtp_Start.Value = DateTime.Now.AddHours(-2);
            this.dtp_End.Value = DateTime.Now;
        }

        // 1. 改为 async 异步事件，拒绝卡死
        private async void btn_QueryHistory_Click(object sender, EventArgs e)
        {
            if (dtp_Start.Value > dtp_End.Value)
            {
                new FrmMsgNoAck("开始时间不能大于结束时间！", "查询提示").ShowDialog();
                return;
            }

            // 工业防护：查询时禁用按钮，防止操作工疯狂乱点导致数据库雪崩
            btn_QueryHistory.Enabled = false;
            this.Cursor = Cursors.WaitCursor; // 变成沙漏状态提示用户正在加载

            try
            {
                // 2. 将耗时的数据库查询丢到后台线程异步执行（UI 保持丝滑自由拖动）
                var res = await Task.Run(() => historyDataService.GetHistoryDataByTime(this.dtp_Start.Value, this.dtp_End.Value));

                if (res.IsSuccess)
                {
                    if (res.Content != null && res.Content.Count > 0)
                    {
                        historyDataList = res.Content;

                        // 干净利落的数据绑定
                        dgv_HistoryData.DataSource = null;
                        dgv_HistoryData.DataSource = historyDataList;
                    }
                    else
                    {
                        historyDataList.Clear();
                        dgv_HistoryData.DataSource = null;
                        new FrmMsgNoAck("没有查询到历史数据！", "查询提示").ShowDialog();
                    }
                }
                else
                {
                    new FrmMsgNoAck("查询历史数据失败！" + res.Message, "查询提示").ShowDialog();
                }
            }
            catch (Exception ex)
            {
                new FrmMsgNoAck("查询发生未捕获异常：" + ex.Message, "错误").ShowDialog();
            }
            finally
            {
                // 恢复界面状态
                btn_QueryHistory.Enabled = true;
                this.Cursor = Cursors.Default;
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

        // 3. 导出事件同样改造为 async 异步
        private async void btn_Export(object sender, EventArgs e)
        {
            // 防御性编程：如果当前根本没数据，直接拦截，不弹保存框
            if (historyDataList == null || historyDataList.Count == 0)
            {
                new FrmMsgNoAck("当前没有可导出的数据，请先进行查询！", "导出数据").ShowDialog();
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "请选择文件",
                Filter = "Excel文件(*.xlsx)|*.xlsx|CSV文件(*.csv)|*.csv",
                FilterIndex = 1,
                RestoreDirectory = true,
                // 修复：大写的 SS 更改为小写 ss，去掉尾部空格
                FileName = $"数据记录_{this.dtp_Start.Value:yyyyMMddHHmmss}_{this.dtp_End.Value:yyyyMMddHHmmss}"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    string filePath = saveFileDialog.FileName;

                    // 4. 将 MiniExcel 写磁盘的高耗时动作异步化
                    await Task.Run(() => MiniExcel.SaveAs(filePath, historyDataList));

                    // 5. 兼容现代 .NET 环境的进程启动方式（防止低版本/高版本环境冲突闪退）
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    new FrmMsgNoAck("导出文件失败！" + ex.Message, "导出数据").ShowDialog();
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            DataGridViewHelper.Print_DataGridView(this.dgv_HistoryData);
        }
    }
}