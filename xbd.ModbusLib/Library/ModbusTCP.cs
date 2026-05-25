using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xbd.DataConvertLib;
using xbd.ModbusLib.Base;

namespace xbd.ModbusLib.Library
{

    /// <summary>
    /// 枚举
    /// </summary>
    public enum FunctionCode
    {
        ReadCoils = 0x01,
        ReadInputs = 0x02,
        ReadHoldingRegisters = 0x03,
        ReadInputRegisters = 0x04,
        WriteSingleCoil = 0x05,
        WriteSingleRegister = 0x06,
        WriteMultipleCoils = 0x0F,
        WriteMultipleRegisters = 0x10
    }

    public class ModbusTCP : TCPBase, IModbusRW
    {

        private static readonly object lockobject = new object();

        private ushort transactionId = 0;

        public ushort TransactionId
        {
            get
            {
                lock (lockobject)
                {
                    if (transactionId == ushort.MaxValue)
                    {
                        transactionId = 1;
                    }
                    return transactionId++;
                }
            }
        }


        /// <summary>
        ///  读取输出线圈
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        public OperateResult<bool[]> ReadCoils(ushort start, ushort length, byte slaveId = 1)
        {
            //第一步：拼接报文
            byte[] sendCommand = BuildReadMessageFrame(start, length, slaveId, FunctionCode.ReadCoils);
            //第二步：发送报文
            //第三步：接收报文
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //第四步：验证报文
                var checkResult = CheckResponse(result.Content, slaveId, true, UShortLib.GetByteLengthFromBoolLength(length));
                if (checkResult.IsSuccess)
                {
                    //第五步：解析数据
                    return OperateResult.CreateSuccessResult(AnalysisResponseMessage(result.Content,true).Content.Select(e=>e==0x01).Take(length).ToArray());
                }
                else
                {
                    return OperateResult.CreateFailResult<bool[]>(checkResult.Message);
                }
            }
            else
            {
                return OperateResult.CreateFailResult<bool[]>(result.Message);
            }
        }

        /// <summary>
        /// 读取保持寄存器
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        public OperateResult<byte[]> ReadHoldingRegisters(ushort start, ushort length, byte slaveId = 1)
        {
            //第一步：拼接报文
            byte[] sendCommand = BuildReadMessageFrame(start, length, slaveId, FunctionCode.ReadHoldingRegisters);
            //第二步：发送报文
            //第三步：接收报文
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //第四步：验证报文
                var checkResult = CheckResponse(result.Content, slaveId, true, (ushort)(length * 2));
                if (checkResult.IsSuccess)
                {
                    //第五步：解析数据
                    return OperateResult.CreateSuccessResult(AnalysisResponseMessage(result.Content, false).Content);
                }
                else
                {
                    return OperateResult.CreateFailResult<byte[]>(checkResult.Message);
                }
            }
            else
            {
                return OperateResult.CreateFailResult<byte[]>(result.Message);
            }
        }

        /// <summary>
        /// 读取输入寄存器
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        public OperateResult<byte[]> ReadInputRegisters(ushort start, ushort length, byte slaveId = 1)
        {
            //第一步：拼接报文
            byte[] sendCommand = BuildReadMessageFrame(start, length, slaveId, FunctionCode.ReadInputRegisters);
            //第二步：发送报文
            //第三步：接收报文
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //第四步：验证报文
                var checkResult = CheckResponse(result.Content, slaveId, true, (ushort)(length * 2));
                if (checkResult.IsSuccess)
                {
                    //第五步：解析数据
                    return OperateResult.CreateSuccessResult(AnalysisResponseMessage(result.Content, false).Content);
                }
                else
                {
                    return OperateResult.CreateFailResult<byte[]>(checkResult.Message);
                }
            }
            else
            {
                return OperateResult.CreateFailResult<byte[]>(result.Message);
            }
        }

        /// <summary>
        /// 读取输入线圈
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        public OperateResult<bool[]> ReadInputs(ushort start, ushort length, byte slaveId = 1)
        {
            //第一步：拼接报文
            byte[] sendCommand = BuildReadMessageFrame(start, length, slaveId, FunctionCode.ReadInputs);
            //第二步：发送报文
            //第三步：接收报文
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //第四步：验证报文
                var checkResult = CheckResponse(result.Content, slaveId, true, UShortLib.GetByteLengthFromBoolLength(length));
                if (checkResult.IsSuccess)
                {
                    //第五步：解析数据
                    return OperateResult.CreateSuccessResult(AnalysisResponseMessage(result.Content, true).Content.Select(e => e == 0x01).Take(length).ToArray());
                }
                else
                {
                    return OperateResult.CreateFailResult<bool[]>(checkResult.Message);
                }
            }
            else
            {
                return OperateResult.CreateFailResult<bool[]>(result.Message);
            }
        }

        public OperateResult WriteMultipleCoils(ushort start, bool[] values, byte slaveId = 1)
        {
            //第一步：拼接报文
            byte[] sendCommand = BuildWriteMessageFrame(start, ByteArrayLib.GetByteArrayFromBoolArray(values), slaveId, FunctionCode.WriteMultipleCoils,(ushort)values.Length);
            //第二步：发送报文
            //第三步：接收报文
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //第四步：验证报文
                var checkResult = CheckResponse(result.Content, slaveId, false);
                if (checkResult.IsSuccess)
                {
                    //第五步：解析数据
                    byte[] reqdata = sendCommand.Take(12).ToArray();
                    reqdata[4] = 0x00;
                    reqdata[5] = 0x06;

                    bool compare = ByteArrayLib.GetByteArrayEquals(reqdata, result.Content);


                    return compare ? OperateResult.CreateSuccessResult() : OperateResult.CreateFailResult("返回报文不正确:"+ BitConverter.ToString(result.Content));
                }
                else
                {
                    return OperateResult.CreateFailResult(checkResult.Message);
                }
            }
            else
            {
                return OperateResult.CreateFailResult(result.Message);
            } 
        }

        public OperateResult WriteMultipleRegisters(ushort start, byte[] values, byte slaveId = 1)
        {
            if(values == null || values.Length == 0 || values.Length % 2 != 0)
            {
                return OperateResult.CreateFailResult("写入字节长度必须为偶数。 ");
            }

            //第一步：拼接报文
            byte[] sendCommand = BuildWriteMessageFrame(start, values, slaveId, FunctionCode.WriteMultipleRegisters);
            //第二步：发送报文
            //第三步：接收报文
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //第四步：验证报文
                var checkResult = CheckResponse(result.Content, slaveId, false);
                if (checkResult.IsSuccess)
                {
                    //第五步：解析数据
                    byte[] reqdata = sendCommand.Take(12).ToArray();
                    reqdata[4] = 0x00;
                    reqdata[5] = 0x06;

                    bool compare = ByteArrayLib.GetByteArrayEquals(reqdata, result.Content);


                    return compare ? OperateResult.CreateSuccessResult() : OperateResult.CreateFailResult("返回报文不正确:" + BitConverter.ToString(result.Content));
                }
                else
                {
                    return OperateResult.CreateFailResult(checkResult.Message);
                }
            }
            else
            {
                return OperateResult.CreateFailResult(result.Message);
            }
        }

        public OperateResult WriteSingleCoil(ushort start, bool value, byte slaveId = 1)
        {
            //第一步：拼接报文
            byte[] sendCommand = BuildWriteMessageFrame (start, value?new byte[]{0xFF,0x00}:new byte[]{0x00,0x00}, slaveId, FunctionCode.WriteSingleCoil);
            //第二步：发送报文
            //第三步：接收报文
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //第四步：验证报文
                var checkResult = CheckResponse(result.Content, slaveId, false);
                if (checkResult.IsSuccess)
                {
                    //第五步：解析数据
                    bool compare = ByteArrayLib.GetByteArrayEquals(result.Content, sendCommand);


                    return compare ? OperateResult.CreateSuccessResult() : OperateResult.CreateFailResult("发送和返回报文不一致");
                }
                else
                {
                    return OperateResult.CreateFailResult(checkResult.Message);
                }
            }
            else
            {
                return OperateResult.CreateFailResult(result.Message);
            }
        }

        public OperateResult WriteSingleRegister(ushort start, byte[] value, byte slaveId = 1)
        {
            if(value == null || value.Length != 2)
            {
                return OperateResult.CreateFailResult("写入字节长度必须为2。 ");
            }

            //第一步：拼接报文
            byte[] sendCommand = BuildWriteMessageFrame(start, value, slaveId, FunctionCode.WriteSingleRegister);
            //第二步：发送报文
            //第三步：接收报文
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //第四步：验证报文
                var checkResult = CheckResponse(result.Content, slaveId, false);
                if (checkResult.IsSuccess)
                {
                    //第五步：解析数据
                    bool compare = ByteArrayLib.GetByteArrayEquals(result.Content, sendCommand);


                    return compare ? OperateResult.CreateSuccessResult() : OperateResult.CreateFailResult("发送和返回报文不一致");
                }
                else
                {
                    return OperateResult.CreateFailResult(checkResult.Message);
                }
            }
            else
            {
                return OperateResult.CreateFailResult(result.Message);
            }
        }

        public OperateResult WriteRegisterBit(string address, bool value, bool isLittleEndian = true, byte slaveId = 1)
        {
            //address 的格式必须为0.10，其中0表示寄存器地址，10表示寄存器内的位地址
            if (address.Contains(".") && address.Split('.').Length == 2)
            {
                string[] info = address.Split('.');

                if (ushort.TryParse(info[0], out ushort start) && ushort.TryParse(info[1], out ushort index))
                {
                    if (index >= 0 && index <= 15)
                    {
                        //先读取寄存器的值
                        OperateResult<byte[]> readResult = ReadHoldingRegisters(start, 1, slaveId);

                        if (readResult.IsSuccess)
                        {
                            //转换
                            byte[] wData = readResult.Content;
                            //修改寄存器内的位值
                            if (isLittleEndian)
                            {
                                int byteIndex = index < 8 ? 1 : 0;
                                wData[byteIndex] = ByteLib.SetbitValue(wData[byteIndex], index % 8, value);
                            }
                            else
                            {
                                int byteIndex = index < 8 ? 0 : 1;
                                wData[byteIndex] = ByteLib.SetbitValue(wData[byteIndex], index % 8, value);
                            }

                            //写回寄存器
                            return WriteSingleRegister(start, wData, slaveId);
                        }
                        return readResult;

                    }
                    else
                    {
                        return OperateResult.CreateFailResult("位偏移索引必须在0-15之间");

                    }
                }
                else
                {
                    return OperateResult.CreateFailResult("地址格式X.Y必须为有效整数");

                }
            }
            else
            {
                return OperateResult.CreateFailResult("地址格式必须为X.Y");
            }
        }



        private byte[] BuildReadMessageFrame(ushort start, ushort count, byte salveId, FunctionCode functionCode)
        {
            //创建一个 ByteArray 对象来构建消息帧
            ByteArray sendCommand = new ByteArray();

            //事物处理标识符
            sendCommand.Add(TransactionId);

            //协议标识符，Modbus TCP 协议中固定为 0
            sendCommand.Add((ushort)0x00);

            //长度字段，表示后续数据的字节数，这里是 6 字节（1 字节的从站地址 + 1 字节的功能码 + 2 字节的起始地址 + 2 字节的寄存器数量）
            sendCommand.Add((ushort)0x06);

            //从站地址
            sendCommand.Add(salveId);

            //功能码
            sendCommand.Add((byte)functionCode);

            //起始地址
            sendCommand.Add(start);

            //数量
            sendCommand.Add(count);

            return sendCommand.array;

        }


        private byte[] BuildWriteMessageFrame(ushort start,byte[] values, byte salveId, FunctionCode functionCode,ushort coilLength = 0)
        {


            //创建一个 ByteArray 对象来构建消息帧
            ByteArray sendCommand = new ByteArray();

            if(functionCode == FunctionCode.WriteSingleCoil||functionCode == FunctionCode.WriteSingleRegister)
            {
                //事物处理标识符
                sendCommand.Add(TransactionId);

                //协议标识符，Modbus TCP 协议中固定为 0
                sendCommand.Add((ushort)0x00);

                //长度字段，表示后续数据的字节数，这里是 6 字节（1 字节的从站地址 + 1 字节的功能码 + 2 字节的起始地址 + 2 字节的寄存器数量）
                sendCommand.Add((ushort)0x06);

                //从站地址
                sendCommand.Add(salveId);

                //功能码
                sendCommand.Add((byte)functionCode);

                //起始地址
                sendCommand.Add(start);

                sendCommand.Add(values);
            }else if(functionCode == FunctionCode.WriteMultipleCoils || functionCode == FunctionCode.WriteMultipleRegisters)
            {

                //事物处理标识符
                sendCommand.Add(TransactionId);

                //协议标识符，Modbus TCP 协议中固定为 0
                sendCommand.Add((ushort)0x00);

                //长度字段，表示后续数据的字节数，这里是 6 字节（1 字节的从站地址 + 1 字节的功能码 + 2 字节的起始地址 + 2 字节的寄存器数量）
                sendCommand.Add((ushort)(7+values.Length));

                //从站地址
                sendCommand.Add(salveId);

                //功能码
                sendCommand.Add((byte)functionCode);

                //起始地址
                sendCommand.Add(start);

                //数量
                sendCommand.Add(coilLength == 0? (ushort)(values.Length/2) : coilLength);

                //字节数
                sendCommand.Add((byte)values.Length);

                //数据
                sendCommand.Add(values);
            }
            else
            {
                return null;
            }

           return sendCommand.array;
        }

        private OperateResult CheckResponse(byte[] response, byte slaveId, bool isRead, ushort byteLength = 0)
        {


            //验证长度
            int reqLength = isRead ? 9 + byteLength : 12;
            if (response.Length == reqLength)
            {
                //验证单元标识符
                if (response[6] != slaveId)
                {
                    return OperateResult.CreateFailResult($"返回报文单元标识符验证不通过：{BitConverter.ToString(response)}");
                }
                else
                {
                    return OperateResult.CreateSuccessResult();
                }
            }
            else
            {
                return OperateResult.CreateFailResult($"返回报文长度验证不通过：{BitConverter.ToString(response)}");
            }

            //读取的验证和写入不一样
        }

        private OperateResult<byte[]> AnalysisResponseMessage(byte[] reponse, bool isBit)
        {
            byte[] data = ByteArrayLib.GetByteArrayFromByteArray(reponse, 9, reponse.Length - 9);

            if (isBit)
            {
                bool[] values = BitLib.GetBitArrayFromByteArray(data);

                //位数据需要特殊处理
                return OperateResult.CreateSuccessResult(values.Select(c => c == true ? (byte)0x01 : (byte)0x00).ToArray());
            }
            else
            {
                //字节数据直接返回
                return OperateResult.CreateSuccessResult(data);
            }
        }
    }
}
