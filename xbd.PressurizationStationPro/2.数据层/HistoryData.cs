using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.PressurizationStationPro
{
    public class HistoryData
    {
        public DateTime InsertTime { get; set; }
        public string PressureIn { get; set; } 
        public string PressureOut { get; set; } 
        public string TempIn1 { get; set; } 
        public string TempIn2 { get; set; } 
        public string TempOut { get; set; } 
        public string PressureTank1 { get; set; }
        public string PressureTank2 { get; set; }
        public string LevelTank1 { get; set; }
        public string LevelTank2 { get; set; }
        public string PressureTankOut { get; set; }
    }
}
