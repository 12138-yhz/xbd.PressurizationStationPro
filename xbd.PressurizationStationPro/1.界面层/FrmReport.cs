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
using xbd.DataConvertLib;

namespace xbd.PressurizationStationPro
{
    public partial class FrmReport : Form
    {
        public FrmReport()
        {
            InitializeComponent();

            this.cmb_ReportType.Items.Add("小时报表");

            this.cmb_ReportType.Items.Add("日报表");
            this.cmb_ReportType.SelectedIndex = 0;

            InitialColumnList();
        }

        private List<string> maxCondition = new List<string>();
        private List<string> minCondition = new List<string>();
        private List<string> avgCondition = new List<string>();
        private HistoryDataService historyDataService = new HistoryDataService();

        private void InitialColumnList()
        {
            List<string> columnList = new List<string>();
            columnList.Add("PressureIn");
            columnList.Add("PressureOut");
            columnList.Add("TempIn1");
            columnList.Add("TempIn2");
            columnList.Add("TempOut");
            columnList.Add("PressureTank1");
            columnList.Add("PressureTank2");
            columnList.Add("LevelTank1");
            columnList.Add("LevelTank2");
            columnList.Add("PressureTankOut");

            foreach (var item in columnList)
            {
                maxCondition.Add($"Max({item})");
                minCondition.Add($"Min({item})");
                avgCondition.Add($"Avg({item})");
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

        private void btn_QueryHistory_Click(object sender, EventArgs e)
        {
            //时间段
            List<string> startList = new List<string>();
            List<string> endList = new List<string>();

            DateTime dateTime = Convert.ToDateTime(this.dtp_ReportTime.Text);

            switch (this.cmb_ReportType.SelectedIndex)
            {
                case 0:
                    for (int i = 0; i < 60; i++)
                    {
                        startList.Add(dateTime.AddMinutes(i).ToString("yyyy-MM-dd HH:mm:ss"));
                        endList.Add(dateTime.AddMinutes(i + 1).ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    break;
                case 1:
                    for (int i = 0; i < 24; i++)
                    {
                        startList.Add(dateTime.AddHours(i).ToString("yyyy-MM-dd HH:mm:ss"));
                        endList.Add(dateTime.AddHours(i + 1).ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    break;
            }

            List<string> conditionList = this.rdb_Max.Checked ? maxCondition : this.rdb_Min.Checked ? minCondition : avgCondition;
            Task.Run(() =>
            {
                Task<OperateResult<DataTable>>[] taskList = new Task<OperateResult<DataTable>>[startList.Count];

                for (int i = 0; i < startList.Count; i++)
                {
                    taskList[i] = Task.Factory.StartNew((index) =>
                    {

                        return historyDataService.GetReportDataByCondition(startList[(int)index], endList[(int)index], conditionList, index.ToString());
                    }, i);
                }

                Task<OperateResult<DataTable>[]> task = Task.WhenAll(taskList);

                if (task.Result.Length > 0 && task.Result.First().IsSuccess)
                {
                    DataTable dataTable = GetAllDataTable(task.Result);

                    //显示
                    if (dataTable != null)
                    {
                        UpdateDataTable(dataTable, startList);
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            new FrmMsgNoAck("数据查询表为空！", "数据查询").ShowDialog();
                        }));

                    }
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        new FrmMsgNoAck("此时间段未查询到数据！", "数据查询").ShowDialog();
                    }));

                }
            });
        }
        private void cmb_ReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.cmb_ReportType.SelectedIndex)
            {
                case 0:
                    this.dtp_ReportTime.CustomFormat = "yyyy-MM-dd HH:00:00";
                    break;
                case 1:
                    this.dtp_ReportTime.CustomFormat = "yyyy-MM-dd 00:00:00";
                    break;
            }
        }

        private DataTable GetAllDataTable(OperateResult<DataTable>[] dataResult)
        {
            List<DataTable> dataTables = new List<DataTable>();

            foreach (var item in dataResult)
            {
                if (item.IsSuccess)
                {
                    dataTables.Add(item.Content);
                }
            }


            if (dataTables.Count > 0)
            {
                dataTables = dataTables.OrderBy(t => Convert.ToInt32(t.TableName)).ToList();

                // 合并所有DataTable
                DataTable resultTable = dataTables.First().Clone();

                object[] rowData = new object[resultTable.Columns.Count];

                for (int i = 0; i < dataTables.Count; i++)
                {
                    for (int j = 0; j < dataTables[i].Rows.Count; j++)
                    {
                        dataTables[i].Rows[j].ItemArray.CopyTo(rowData, 0);
                        resultTable.Rows.Add(rowData);
                    }
                }
                return resultTable;
            }
            return null;
        }

        private void UpdateDataTable(DataTable dataTable, List<string> startList)
        {
            //加判断变为更通用的方法，

            if (this.dgv_HistoryData.InvokeRequired)
            {
                this.dgv_HistoryData.Invoke(new Action<DataTable,List<string>>(UpdateDataTable),dataTable,startList);
            }
            else
            {
                this.dgv_HistoryData.Rows.Clear();
             
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    int rowIndex = this.dgv_HistoryData.Rows.Add();

                    this.dgv_HistoryData.Rows[rowIndex].Cells[0].Value = startList[i];

                    for (int j = 0;j<dataTable.Columns.Count;j++)
                    {

                        if(dataTable.Rows[i][j] is DBNull)
                        {
                            this.dgv_HistoryData.Rows[rowIndex].Cells[j + 1].Value = "---";
                        }
                        else
                        {
                            this.dgv_HistoryData.Rows[rowIndex].Cells[j + 1].Value = dataTable.Rows[i][j].ToString();
                        }
                           
                    }   
                }
            }
        }

        private void dgv_HistoryData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewHelper.DgvRowPostPaint(this.dgv_HistoryData, e);
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            DataGridViewHelper.Print_DataGridView(this.dgv_HistoryData);
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            //标题
            saveFileDialog.Title = "请选择文件";
            saveFileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx|CSV文件(*.csv)|*.csv";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = "数据报录_"+this.cmb_ReportType.Text+"_" +Convert.ToDateTime(this.dtp_ReportTime.Text).ToString("yyyyMMddHHmmSS");


            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    MiniExcel.SaveAs(saveFileDialog.FileName, GetHistoryDataList());
                    Process.Start(saveFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    new FrmMsgNoAck("导出文件失败！" + ex.Message, "导出数据").ShowDialog();
                }
            }
        }

        public List<HistoryData> GetHistoryDataList() 
        { 
            if(this.dgv_HistoryData.Rows.Count > 0)
            {
                List<HistoryData> historyDataList = new List<HistoryData>();
                for (int i = 0; i < this.dgv_HistoryData.Rows.Count; i++)
                {
                    HistoryData historyData = new HistoryData();
                    historyData.InsertTime = Convert.ToDateTime(this.dgv_HistoryData.Rows[i].Cells[0].Value);
                    historyData.PressureIn = this.dgv_HistoryData.Rows[i].Cells[1].Value.ToString();
                    historyData.PressureOut = this.dgv_HistoryData.Rows[i].Cells[2].Value.ToString();
                    historyData.TempIn1 = this.dgv_HistoryData.Rows[i].Cells[3].Value.ToString();
                    historyData.TempIn2 = this.dgv_HistoryData.Rows[i].Cells[4].Value.ToString();
                    historyData.TempOut = this.dgv_HistoryData.Rows[i].Cells[5].Value.ToString();
                    historyData.PressureTank1 = this.dgv_HistoryData.Rows[i].Cells[6].Value.ToString();
                    historyData.PressureTank2 = this.dgv_HistoryData.Rows[i].Cells[7].Value.ToString();
                    historyData.LevelTank1 = this.dgv_HistoryData.Rows[i].Cells[8].Value.ToString();
                    historyData.LevelTank2 = this.dgv_HistoryData.Rows[i].Cells[9].Value.ToString();
                    historyData.PressureTankOut = this.dgv_HistoryData.Rows[i].Cells[10].Value.ToString();
                    historyDataList.Add(historyData);
                }
                return historyDataList;
            }
            return new List<HistoryData>();
        }
    }
}