using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using xbd.s7netplus;

namespace xbd.PressurizationStationPro
{
    public partial class FrmMain : Form
    {
        private S7NetLib siemens;
        public FrmMain()
        {
            InitializeComponent();

            siemens = new S7NetLib(CpuType.S71500, "192.168.10.200", 0, 0);

            var result = siemens.Connect();

            if (result.IsSuccess)
            {

                siemens.WriteVariable("DB1.DBX100.0", true);
                Thread.Sleep(100);
                siemens.WriteVariable("DB1.DBX100.0", false);


                var res = siemens.ReadClass<PlcData>(1, 0);
                if (res.IsSuccess)
                {
                    this.label36.Text = res.Content.TempIn1.ToString("f2");
                    this.xbdPump1.IsRun = res.Content.InPump1State;
                }
                else
                {
                    MessageBox.Show($"读取失败，错误信息：{res.Message}");
                }
            }
            else
            {
                MessageBox.Show("连接失败");
            }

        }
    }

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
        public byte[] SpareState { get; set; } = new byte[2];
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
        public byte[] SpareVariable { get; set; } = new byte[56];
        public bool InPump1Start { get; set; }
        public bool InPump1Stop { get; set; }
        public bool InPump2Start { get; set; }
        public bool InPump2Stop { get; set; }
        public bool CirclePump1Start { get; set; }
        public bool CirclePump1Stop { get; set; }
        public bool CirclePump2Start { get; set; }
        public bool CirclePump2Stop { get; set; }
        public bool ValveInOpen { get; set; }
        public bool ValveInClose { get; set; }
        public bool ValveOutOpen { get; set; }
        public bool ValveOutClose { get; set; }
    }
}
