using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xbd.DataConvertLib;

namespace xbd.PressurizationStationPro
{
    public class HistoryDataService
    {
        /// <summary>
        /// 插入一条数据记录
        /// </summary>
        /// <param name="historyData">要插入的历史数据对象</param>
        /// <returns>返回操作是否成功</returns>
        public bool AddHistoryData(HistoryData historyData)
        {
            string sql = "INSERT INTO HistoryData (InsertTime, PressureIn, PressureOut, TempIn1, TempIn2, TempOut, PressureTank1, PressureTank2, LevelTank1, LevelTank2, PressureTankOut) VALUES (@InsertTime, @PressureIn, @PressureOut, @TempIn1, @TempIn2, @TempOut, @PressureTank1, @PressureTank2, @LevelTank1, @LevelTank2, @PressureTankOut)";
            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@InsertTime", historyData.InsertTime.ToString("yyyy-MM-dd HH:mm:ss")),
                new SQLiteParameter("@PressureIn", historyData.PressureIn),
                new SQLiteParameter("@PressureOut", historyData.PressureOut),
                new SQLiteParameter("@TempIn1", historyData.TempIn1),
                new SQLiteParameter("@TempIn2", historyData.TempIn2),
                new SQLiteParameter("@TempOut", historyData.TempOut),
                new SQLiteParameter("@PressureTank1", historyData.PressureTank1),
                new SQLiteParameter("@PressureTank2", historyData.PressureTank2),
                new SQLiteParameter("@LevelTank1", historyData.LevelTank1),
                new SQLiteParameter("@LevelTank2", historyData.LevelTank2),
                new SQLiteParameter("@PressureTankOut", historyData.PressureTankOut)
            };
            return SQLiteHelper.ExecuteNonQuery(sql, parameters) > 0;
        }


        /// <summary>
        /// 开始时间和结束时间查询历史数据记录，返回一个包含查询结果的OperateResult对象，OperateResult对象的Content属性包含一个HistoryData对象的列表，每个HistoryData对象表示一条历史数据记录。
        /// </summary>
        /// <param name="sart">查询的开始时间</param>
        /// <param name="end">查询的结束时间</param>
        /// <returns>返回一个OperateResult对象，包含查询结果或错误信息</returns>
        public OperateResult<List<HistoryData>> GetHistoryDataByTime(DateTime sart,DateTime end) 
        {
            string sql = "SELECT InsertTime, PressureIn, PressureOut, TempIn1, TempIn2, TempOut, PressureTank1, PressureTank2, LevelTank1, LevelTank2, PressureTankOut FROM HistoryData WHERE InsertTime >= @Start AND InsertTime <= @End ORDER BY InsertTime DESC";
            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@Start", sart.ToString("yyyy-MM-dd HH:mm:ss")),
                new SQLiteParameter("@End", end.ToString("yyyy-MM-dd HH:mm:ss"))
            };
            try
            {
                SQLiteDataReader dataReader = SQLiteHelper.ExecuteReader(sql, parameters);
                List<HistoryData> historyDataList = new List<HistoryData>();
                while (dataReader.Read())
                {
                    historyDataList.Add(new HistoryData
                    {
                        InsertTime = Convert.ToDateTime(dataReader["InsertTime"]),
                        PressureIn = dataReader["PressureIn"].ToString(),
                        PressureOut = dataReader["PressureOut"].ToString(),
                        TempIn1 = dataReader["TempIn1"].ToString(),
                        TempIn2 = dataReader["TempIn2"].ToString(),
                        TempOut = dataReader["TempOut"].ToString(),
                        PressureTank1 = dataReader["PressureTank1"].ToString(),
                        PressureTank2 = dataReader["PressureTank2"].ToString(),
                        LevelTank1 = dataReader["LevelTank1"].ToString(),
                        LevelTank2 = dataReader["LevelTank2"].ToString(),
                        PressureTankOut = dataReader["PressureTankOut"].ToString()
                    });
                }
                dataReader.Close();
                return OperateResult.CreateSuccessResult(historyDataList);
            }
            catch (Exception ex)
            {

                return OperateResult.CreateFailResult<List<HistoryData>>(ex.Message);
            }
        }

        /// <summary>
        /// 需求是根据开始时间和结束时间以及查询条件查询历史数据记录，并返回一个包含查询结果的OperateResult对象，OperateResult对象的Content属性包含一个DataTable对象，DataTable对象包含查询结果的数据。 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="condition"></param>
        /// <param name="dataableName">数据表名称</param>
        /// <returns></returns>
        public OperateResult<DataTable> GetReportDataByCondition(string start,string end,List<string> condition,string dataableName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Select ");
            stringBuilder.Append(string.Join(",", condition));
            stringBuilder.Append(" From HistoryData Where InsertTime between @Start and @End");

            SQLiteParameter[] parameters = new SQLiteParameter[] { 
            
                new SQLiteParameter("@Start", start),
                new SQLiteParameter("@End", end)
             };

            try
            {
                DataSet dataSet = SQLiteHelper.GetDataSet(stringBuilder.ToString(), parameters, dataableName);
                if(dataSet.Tables.Count > 0)
                {
                    return OperateResult.CreateSuccessResult(dataSet.Tables[0]);
                }
                else
                {
                    return OperateResult.CreateFailResult<DataTable>("未查询到数据");
                }
            }
            catch (Exception ex)
            {

                return OperateResult.CreateFailResult<DataTable>(ex.Message);
            }
        }

    }
}
