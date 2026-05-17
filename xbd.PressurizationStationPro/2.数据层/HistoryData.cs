using MiniExcelLibs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.PressurizationStationPro
{
    public class HistoryData
    {
        [MiniExcelLibs.Attributes.ExcelColumnName("日期时间")]
        [ExcelFormat("yyyy-MM-dd HH:mm:ss")]
        [ExcelColumnWidth(50)]
        public DateTime InsertTime { get; set; }
        [MiniExcelLibs.Attributes.ExcelColumnName("进口压力")]
        [ExcelColumnWidth(20)]
        public string PressureIn { get; set; }
        [MiniExcelLibs.Attributes.ExcelColumnName("出口压力")]
        [ExcelColumnWidth(20)]
        public string PressureOut { get; set; }
        [MiniExcelLibs.Attributes.ExcelColumnName("进口温度1")]
        [ExcelColumnWidth(20)]
        public string TempIn1 { get; set; }
        [MiniExcelLibs.Attributes.ExcelColumnName("进口温度2")]
        [ExcelColumnWidth(20)]
        public string TempIn2 { get; set; }
        [MiniExcelLibs.Attributes.ExcelColumnName("出口温度")]
        [ExcelColumnWidth(20)]
        public string TempOut { get; set; }
        [MiniExcelLibs.Attributes.ExcelColumnName("水箱压力1")]
        [ExcelColumnWidth(20)]
        public string PressureTank1 { get; set; }
        [MiniExcelLibs.Attributes.ExcelColumnName("水箱压力2")]
        [ExcelColumnWidth(20)]
        public string PressureTank2 { get; set; }
        [MiniExcelLibs.Attributes.ExcelColumnName("水箱液位1")]
        [ExcelColumnWidth(20)]
        public string LevelTank1 { get; set; }
        [MiniExcelLibs.Attributes.ExcelColumnName("水箱液位2")]
        [ExcelColumnWidth(20)]
        public string LevelTank2 { get; set; }
        [MiniExcelLibs.Attributes.ExcelColumnName("水箱出口压力")]
        [ExcelColumnWidth(20)]
        public string PressureTankOut { get; set; }
    }
}
