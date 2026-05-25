using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xbd.DataConvertLib;

namespace xbd.ModbusLib
{
    /// <summary>
    /// modbus读写接口，定义了modbus协议中常用的读写功能，包括读线圈、读离散输入、读保持寄存器、读输入寄存器，以及写单个线圈、写单个寄存器、写多个线圈、写多个寄存器等方法。
    /// </summary>
    public interface IModbusRW
    {
        OperateResult<bool[]> ReadCoils(ushort start, ushort length,byte slaveId = 1);
        OperateResult<bool[]> ReadInputs(ushort start, ushort length,byte slaveId = 1);
        OperateResult<byte[]> ReadHoldingRegisters(ushort start, ushort length,byte slaveId = 1);
        OperateResult<byte[]> ReadInputRegisters(ushort start, ushort length,byte slaveId = 1);

        OperateResult WriteSingleCoil(ushort start, bool value, byte slaveId = 1);
        OperateResult WriteSingleRegister(ushort start, byte[] value, byte slaveId = 1);
        OperateResult WriteMultipleCoils(ushort start, bool[] values, byte slaveId = 1);
        OperateResult WriteMultipleRegisters(ushort start, byte[] values, byte slaveId = 1);
    }
}
