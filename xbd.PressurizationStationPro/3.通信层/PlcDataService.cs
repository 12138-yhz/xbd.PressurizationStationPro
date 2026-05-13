using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using xbd.DataConvertLib;

namespace xbd.PressurizationStationPro
{
    public class PlcDataService
    {
        /// <summary>
        /// 第一次连接
        /// </summary>
        public bool IsFirstScan { get; set; } = true;
        /// <summary>
        /// 连接状态，连接成功为 true，连接失败为 false
        /// </summary>
        public bool IsConnected { get; set; } = false;

        /// <summary>
        /// 当前连接错误次数，连接成功后重置为 0，连接失败时加 1
        /// </summary>
        public int ErrorTimes { get; set; }
        /// <summary>
        /// 最大允许连接错误次数，超过该次数后 IsConnected 置为 false，默认为 3
        /// </summary>
        public int ErrorAllowTimes { get; set; } = 3;

        private S7NetLib s7Net;

        public OperateResult Connect(SysInfo sysInfo)
        {
            s7Net = new S7NetLib(sysInfo.CpuType,sysInfo.IPAddress, sysInfo.Rack, sysInfo.Slot);

            return s7Net.Connect();
        }

        public OperateResult Disconnect()
        {
            if(s7Net != null)
            {
                return OperateResult.CreateFailResult("未连接PLC");
            }
            return s7Net.Disconnect();
        }

        /// <summary>
        /// 数据读取
        /// </summary>
        /// <returns></returns>
        public OperateResult<PlcData> ReadPlcData()
        {
           int byteCount = 44;

            var result = s7Net.ReadByteArray(xbd.s7netplus.DataType.DataBlock, 1, 0, byteCount);

            if(result.IsSuccess && result.Content != null && result.Content.Length == byteCount)
            {
                //数据解析
                PlcData plcData = new PlcData();

                //bool解析
                plcData.InPump1State = BitLib.GetBitFromByteArray(result.Content, 0, 0);
                plcData.InPump2State = BitLib.GetBitFromByteArray(result.Content, 0, 1);
                plcData.CirclePump1State = BitLib.GetBitFromByteArray(result.Content, 0, 2);
                plcData.CirclePump2State = BitLib.GetBitFromByteArray(result.Content, 0, 3);
                plcData.ValveInState = BitLib.GetBitFromByteArray(result.Content, 0, 4);
                plcData.ValveOutState = BitLib.GetBitFromByteArray(result.Content, 0, 5);
                plcData.SysRunState = BitLib.GetBitFromByteArray(result.Content, 0, 6);
                plcData.SysAlarmState = BitLib.GetBitFromByteArray(result.Content, 0, 7);

                //浮点数解析
                plcData.PressureIn = FloatLib.GetFloatFromByteArray(result.Content, 4);
                plcData.PressureOut = FloatLib.GetFloatFromByteArray(result.Content, 8);
                plcData.TempIn1 = FloatLib.GetFloatFromByteArray(result.Content, 12);
                plcData.TempIn2  = FloatLib.GetFloatFromByteArray(result.Content, 16);
                plcData.TempOut = FloatLib.GetFloatFromByteArray(result.Content, 20);
                plcData.PressureTank1 = FloatLib.GetFloatFromByteArray(result.Content, 24);
                plcData.PressureTank2 = FloatLib.GetFloatFromByteArray(result.Content, 28);
                plcData.LevelTank1 = FloatLib.GetFloatFromByteArray(result.Content, 32);
                plcData.LevelTank2 = FloatLib.GetFloatFromByteArray(result.Content, 36);
                plcData.PressureTankOut = FloatLib.GetFloatFromByteArray(result.Content, 40);

                return OperateResult.CreateSuccessResult(plcData);
            }
            else
            {
                return OperateResult.CreateFailResult<PlcData>(result.Message);
            }
        }



        public bool InPump1Control(bool value)
        {
            string startAddress = "DB1.DBX100.0";
            string stopAddress = "DB1.DBX100.1";
            string controlAddress = value ? startAddress : stopAddress;
            bool result = s7Net.WriteVariable(controlAddress, true).IsSuccess;
            Thread.Sleep(50);
            result &= s7Net.WriteVariable(controlAddress, false).IsSuccess;
            return result;
        }

        /// <summary>
        /// 2号进水泵控制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool InPump2Control(bool value)
        {
            string startAddress = "DB1.DBX100.2";
            string stopAddress = "DB1.DBX100.3";
            string controlAddress = value ? startAddress : stopAddress;
            bool result = s7Net.WriteVariable(controlAddress, true).IsSuccess;
            Thread.Sleep(50);
            result &= s7Net.WriteVariable(controlAddress, false).IsSuccess;
            return result;
        }

        /// <summary>
        /// 1号循环泵控制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CirclePump1Control(bool value)
        {
            string startAddress = "DB1.DBX100.4"; // 注意：此处地址与InPump2Control相同，请核实是否为笔误
            string stopAddress = "DB1.DBX100.5";
            string controlAddress = value ? startAddress : stopAddress;
            bool result = s7Net.WriteVariable(controlAddress, true).IsSuccess;
            Thread.Sleep(50);
            result &= s7Net.WriteVariable(controlAddress, false).IsSuccess;
            return result;
        }

        /// <summary>
        /// 2号循环泵控制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CirclePump2Control(bool value)
        {
            string startAddress = "DB1.DBX100.6"; // 注意：此处地址与InPump2Control相同，请核实是否为笔误
            string stopAddress = "DB1.DBX100.7";
            string controlAddress = value ? startAddress : stopAddress;
            bool result = s7Net.WriteVariable(controlAddress, true).IsSuccess;
            Thread.Sleep(50);
            result &= s7Net.WriteVariable(controlAddress, false).IsSuccess;
            return result;
        }


        /// <summary>
        /// 系统复位
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SysReset()
        {
            string controlAddress = "DB1.DBX101.4";
     
            bool result = s7Net.WriteVariable(controlAddress, true).IsSuccess;
            Thread.Sleep(50);
            result &= s7Net.WriteVariable(controlAddress, false).IsSuccess;
            return result;
        }
    }
}
