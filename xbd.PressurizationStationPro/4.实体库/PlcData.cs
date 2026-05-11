using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.PressurizationStationPro
{
    public class PlcData
    {

        public bool InPump1State { get; set; }
        public bool InPump2State { get; set; }
        public bool CirclePump1State { get; set; }
        public bool CirclePump2State { get; set; }
        public bool ValveInState { get; set; }
        public bool ValveOutState { get; set; }
        public bool SysRunState { get; set; }
        public bool SysAlarmState { get; set; }
        public float PressureIn { get; set; }
        public float PressureOut { get; set; }
        public float TempIn1 { get; set; }
        public float TempIn2 { get; set; }
        public float TempOut { get; set; }
        public float PressureTank1 { get; set; }
        public float PressureTank2 { get; set; }
        public float LevelTank1 { get; set; }
        public float LevelTank2 { get; set; }
        public float PressureTankOut { get; set; }
 
    }
}
