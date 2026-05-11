using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xbd.DataConvertLib;

namespace xbd.PressurizationStationPro
{
    public class PlcDataService
    {
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

            var result = s7Net.ReadByteArray(0, 0, 0, byteCount);

            if(result.IsSuccess && result.Content != null && result.Content.Length == byteCount)
            {
                //数据解析
                PlcData plcData = new PlcData();

                //bool解析
                plcData.InPump1State = BitLib.GetBitFromByteArray(result.Content, 0, 0);
                plcData.InPump2State = BitLib.GetBitFromByteArray(result.Content, 0, 0);
                plcData.CirclePump1State = BitLib.GetBitFromByteArray(result.Content, 0, 0);
                plcData.CirclePump2State = BitLib.GetBitFromByteArray(result.Content, 0, 0);
                plcData.ValveInState = BitLib.GetBitFromByteArray(result.Content, 0, 0);
                plcData.ValveOutState = BitLib.GetBitFromByteArray(result.Content, 0, 0);
                plcData.SysRunState = BitLib.GetBitFromByteArray(result.Content, 0, 0);
                plcData.SysAlarmState = BitLib.GetBitFromByteArray(result.Content, 0, 0);

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

    }
}
