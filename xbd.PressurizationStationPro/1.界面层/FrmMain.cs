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
using xbd.ControlLib;
using xbd.s7netplus;

namespace xbd.PressurizationStationPro
{
    public partial class FrmMain : Form
    {
        private S7NetLib siemens;

        private SysInfoService infoService = new SysInfoService();

        /// <summary>
        /// 系统配置路径
        /// </summary>
        private string sysInfoPath = Application.StartupPath + "\\SysInfo.ini";

        /// <summary>
        /// 系统配置对象
        /// </summary>
        private SysInfo sysInfo = new SysInfo();

        private PlcDataService plcDataService = new PlcDataService();

        private CancellationTokenSource clts = new CancellationTokenSource();

        public FrmMain()
        {
            InitializeComponent();

            this.Load += FrmMain_Load;
            this.FormClosing += FrmMain_FormClosing;

        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            clts.Cancel();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.sysInfo = infoService.GetSysInfoFromPath(sysInfoPath);

            if (sysInfo == null)
            {
                new FrmMsgNoAck("系统配置加载失败！", "系统配置");
                return;
            }

            Task.Run(new Action(() => { PLCCommlication(); }));
        }

        private void PLCCommlication()
        {
            while (!clts.IsCancellationRequested)
            {
                if (plcDataService.IsConnected)
                {
                    var data = plcDataService.ReadPlcData();
                    //成功
                    if (data.IsSuccess)
                    {
                        ///容错次数清零
                        plcDataService.ErrorTimes = 0; 
                        this.UpdateUIData(data.Content);
                    }
                    //失败
                    else
                    {
                        //容错次数
                        plcDataService.ErrorTimes++;

                        if (plcDataService.ErrorTimes >= plcDataService.ErrorAllowTimes)
                        {
                            plcDataService.IsConnected = false;

                        }
                    }

                    Thread.Sleep(300);

                }
                else
                {

                    if (!plcDataService.IsFirstScan)
                    {
                        //不是第一次扫描，说明之前连接过PLC，但现在连接失败了，重连
                        Thread.Sleep(3000);
                        plcDataService.Disconnect();
                    }
                    else
                    {
                        //第一次扫描，尝试连接PLC
                        plcDataService.IsFirstScan = false;
                    }

                    var result = plcDataService.Connect(this.sysInfo);

                    plcDataService.IsConnected = result.IsSuccess;
                }
            }
        }

        private void btn_SetParm_Click(object sender, EventArgs e)
        {
            new FrmParmSet(this.sysInfo, this.infoService, this.sysInfoPath).ShowDialog();
        }

        private void UpdateUIData(PlcData plcData)
        {

            if (this.InvokeRequired) 
            {
                try
                {
                    this.Invoke(new Action<PlcData>(UpdateUIData), plcData);
                }
                catch (Exception)
                {
                    return;
                }
            }
            else
            {
                //左侧仪表
                this.lbl__PressureIn.Text = plcData.PressureIn.ToString("f2") + " bar";
                this.lbl__PressureOut.Text = plcData.PressureOut.ToString("f2") + " bar";
                this.meter_PressureIn.Value = plcData.PressureIn;
                this.meter_PressureOut.Value = plcData.PressureOut;

                //底测
                this.ms_TempIn1.ParmValue = plcData.TempIn1;
                this.ms_TempIn2.ParmValue = plcData.TempIn2;
                this.ms_TempOut.ParmValue = plcData.TempOut;
                this.ms_TankPressureIn1.ParmValue = plcData.PressureTank1;
                this.ms_TankPressureIn2.ParmValue = plcData.PressureTank2;
                this.ms_TankPressureOut.ParmValue = plcData.PressureTankOut;

                //系统状态
                this.led_SysRunState.State = plcData.SysRunState;
                this.led_SysAlarmState.State = plcData.SysAlarmState;

                //系统参数
                this.lbl_PressureTank1.Text = plcData.PressureTank1.ToString("f2");
                this.lbl_LevelTank1.Text = plcData.PressureTank1.ToString("f2");
                this.lbl_PressureTank2.Text = plcData.PressureTank2.ToString("f2");
                this.lbl_LevelTank2.Text = plcData.PressureTank2.ToString("f2");
                this.lbl_PressureTankOut.Text = plcData.PressureTankOut.ToString("f2");

                //流程图数据
                this.lbl_TempIn1.Text = plcData.TempIn1.ToString("f2");
                this.lbl_TempIn2.Text = plcData.TempIn2.ToString("f2");
                this.lbl_TempOut.Text = plcData.TempOut.ToString("f2");

                this.pump_In1.IsRun = plcData.InPump1State;
                this.pump_In2.IsRun = plcData.InPump2State;

                this.valve_In.State = plcData.ValveInState;
                this.valve_Out.State = plcData.ValveOutState;

                this.motor_Pump1.PumpState = plcData.CirclePump1State ? PumpState.运行 : PumpState.停止;
                this.motor_Pump2.PumpState = plcData.CirclePump2State ? PumpState.运行 : PumpState.停止;

                //量程 2m
                this.wave_Tank1.Value = Convert.ToInt32((plcData.LevelTank1 / 2.0f) * 100.0f);
                this.wave_Tank2.Value = Convert.ToInt32((plcData.LevelTank2 / 2.0f) * 100.0f);

                this.lbl_PreTankOut.Text = plcData.PressureTankOut.ToString("f2");

                this.btn_Pump1.Text = plcData.CirclePump1State ? "停止" : "启动";
                this.btn_Pump2.Text = plcData.CirclePump2State ? "停止" : "启动";
            }

            
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void btn_Pump1_Click(object sender, EventArgs e)
        {
            plcDataService.CirclePump1Control(this.btn_Pump1.Text == "启动");
        }

        private void btn_Pump2_Click(object sender, EventArgs e)
        {
            plcDataService.CirclePump2Control(this.btn_Pump2.Text == "启动");
        }

        private void toggle_Pump1_CheckedChanged(object sender, EventArgs e)
        {
            if(plcDataService.InPump1Control(this.toggle_Pump1.Checked)==false)
            {
               this.toggle_Pump1.CheckedChanged -= toggle_Pump1_CheckedChanged;
                this.toggle_Pump1.Checked = !this.toggle_Pump1.Checked;
                this.toggle_Pump1.CheckedChanged += toggle_Pump1_CheckedChanged;
            }
        }

        private void toggle_Pump2_CheckedChanged(object sender, EventArgs e)
        {
            if (plcDataService.InPump2Control(this.toggle_Pump2.Checked) == false)
            {
                this.toggle_Pump2.CheckedChanged -= toggle_Pump2_CheckedChanged;
                this.toggle_Pump2.Checked = !this.toggle_Pump2.Checked;
                this.toggle_Pump2.CheckedChanged += toggle_Pump2_CheckedChanged;
            }
        }

        private void btn_SysRest_Click(object sender, EventArgs e)
        {
            plcDataService.SysReset();
        }
    }
}
