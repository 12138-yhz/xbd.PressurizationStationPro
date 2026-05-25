using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xbd.DataConvertLib;

namespace xbd.ModbusLib
{
    public class ModbusRTU : SerialBase, IModbusRW
    {
        public OperateResult<bool[]> ReadCoils(ushort start, ushort length, byte slaveId = 1)
        {
            //发送报文格式
            //从站地址 + 功能码 + 起始线圈地址 + 线圈数量 + CRC校验

            //第一步：构造请求报文
            List<byte> sendCommand = new List<byte>();
            sendCommand.Add(slaveId);
            sendCommand.Add(0x01); //功能码：读线圈
            sendCommand.Add((byte)(start / 256)); //起始地址高字节
            sendCommand.Add((byte)(start % 256)); //起始地址低字节
            sendCommand.Add((byte)(length / 256)); //线圈数量高字节
            sendCommand.Add((byte)(length % 256)); //线圈数量低字节
            sendCommand.AddRange(CRC16(sendCommand.ToArray(), sendCommand.Count)); //CRC校验低字节

            //第二步：发送报文
            //第三步：接收报文
            OperateResult<byte[]> result = SendAndReceive(sendCommand.ToArray());

            int byteLength = length %8==0 ? length / 8 : length / 8 + 1;

            if (result.IsSuccess)
            {
                //第四步：验证报文
                if(result.Content.Length == 5 + byteLength&&CheckCRC(result.Content))
                {
                    //第五步：解析报文
                    byte[] data = result.Content.Skip(3).Take(byteLength).ToArray();

                    return OperateResult.CreateSuccessResult(BitLib.GetBitArrayFromByteArray(data).Take(length).ToArray());
                }
                else
                {
                    return OperateResult.CreateFailResult<bool[]>("返回报文错误"+BitConverter.ToString(result.Content));
                }
            }
            else
            {
                return OperateResult.CreateFailResult<bool[]>(result);
            }
        }

        public OperateResult<bool[]> ReadInputs(ushort start, ushort length, byte slaveId = 1)
        {
            //发送报文格式
            //从站地址 + 功能码 + 起始线圈地址 + 线圈数量 + CRC校验

            //第一步：构造请求报文
            List<byte> sendCommand = new List<byte>();
            sendCommand.Add(slaveId);
            sendCommand.Add(0x02); //功能码：读输入
            sendCommand.Add((byte)(start / 256)); //起始地址高字节
            sendCommand.Add((byte)(start % 256)); //起始地址低字节
            sendCommand.Add((byte)(length / 256)); //线圈数量高字节
            sendCommand.Add((byte)(length % 256)); //线圈数量低字节
            sendCommand.AddRange(CRC16(sendCommand.ToArray(), sendCommand.Count)); //CRC校验低字节

            //第二步：发送报文
            //第三步：接收报文
            OperateResult<byte[]> result = SendAndReceive(sendCommand.ToArray());

            int byteLength = (length + 7) / 8;

            if (result.IsSuccess)
            {
                //第四步：验证报文
                if (result.Content.Length == 5 + byteLength && CheckCRC(result.Content))
                {
                    //第五步：解析报文
                    byte[] data = result.Content.Skip(3).Take(byteLength).ToArray();

                    return OperateResult.CreateSuccessResult(BitLib.GetBitArrayFromByteArray(data).Take(length).ToArray());
                }
                else
                {
                    return OperateResult.CreateFailResult<bool[]>("返回报文错误" + BitConverter.ToString(result.Content));
                }
            }
            else
            {
                return OperateResult.CreateFailResult<bool[]>(result);
            }
        }

        public OperateResult<byte[]> ReadHoldingRegisters(ushort start, ushort length, byte slaveId = 1)
        {
            //发送报文格式
            //从站地址 + 功能码 + 起始寄存器地址 + 寄存器数量 + CRC校验

            //第一步：构造请求报文
            List<byte> sendCommand = new List<byte>();
            sendCommand.Add(slaveId);
            sendCommand.Add(0x03); //功能码：读保持寄存器
            sendCommand.Add((byte)(start / 256)); //起始地址高字节
            sendCommand.Add((byte)(start % 256)); //起始地址低字节
            sendCommand.Add((byte)(length / 256)); //线圈数量高字节
            sendCommand.Add((byte)(length % 256)); //线圈数量低字节
            sendCommand.AddRange(CRC16(sendCommand.ToArray(), sendCommand.Count)); //CRC校验低字节

            //第二步：发送报文
            //第三步：接收报文
            OperateResult<byte[]> result = SendAndReceive(sendCommand.ToArray());

            int byteLength = length*2;

            if (result.IsSuccess)
            {
                //第四步：验证报文
                if (result.Content.Length == 5 + byteLength && CheckCRC(result.Content))
                {
                    //第五步：解析报文
                    byte[] data = result.Content.Skip(3).Take(byteLength).ToArray();

                    return OperateResult.CreateSuccessResult(data);
                }
                else
                {
                    return OperateResult.CreateFailResult<byte[]>("返回报文错误" + BitConverter.ToString(result.Content));
                }
            }
            else
            {
                return OperateResult.CreateFailResult<byte[]>(result);
            }
        }

        public OperateResult<byte[]> ReadInputRegisters(ushort start, ushort length, byte slaveId = 1)
        {
            //发送报文格式
            //从站地址 + 功能码 + 起始寄存器地址 + 寄存器数量 + CRC校验

            //第一步：构造请求报文
            List<byte> sendCommand = new List<byte>();
            sendCommand.Add(slaveId);
            sendCommand.Add(0x04); //功能码：读保持寄存器
            sendCommand.Add((byte)(start / 256)); //起始地址高字节
            sendCommand.Add((byte)(start % 256)); //起始地址低字节
            sendCommand.Add((byte)(length / 256)); //线圈数量高字节
            sendCommand.Add((byte)(length % 256)); //线圈数量低字节
            sendCommand.AddRange(CRC16(sendCommand.ToArray(), sendCommand.Count)); //CRC校验低字节

            //第二步：发送报文
            //第三步：接收报文
            OperateResult<byte[]> result = SendAndReceive(sendCommand.ToArray());

            int byteLength = length * 2;

            if (result.IsSuccess)
            {
                //第四步：验证报文
                if (result.Content.Length == 5 + byteLength && CheckCRC(result.Content))
                {
                    //第五步：解析报文
                    byte[] data = result.Content.Skip(3).Take(byteLength).ToArray();

                    return OperateResult.CreateSuccessResult(data);
                }
                else
                {
                    return OperateResult.CreateFailResult<byte[]>("返回报文错误" + BitConverter.ToString(result.Content));
                }
            }
            else
            {
                return OperateResult.CreateFailResult<byte[]>(result);
            }
        }

        public OperateResult WriteSingleCoil(ushort start, bool value, byte slaveId = 1)
        {
            //发送报文格式
            //从站地址 + 功能码 + 起始线圈地址 + 写入值 + CRC校验

            //第一步：构造请求报文
            List<byte> sendCommand = new List<byte>();
            sendCommand.Add(slaveId);
            sendCommand.Add(0x05); //功能码：写单个线圈
            sendCommand.Add((byte)(start / 256)); //起始地址高字节
            sendCommand.Add((byte)(start % 256)); //起始地址低字节
            sendCommand.Add(value ? (byte)0xFF : (byte)0x00); //写入值
            sendCommand.Add(0x00); //写入值
            sendCommand.AddRange(CRC16(sendCommand.ToArray(), sendCommand.Count)); //CRC校验低字节

            //第二步：发送报文
            //第三步：接收报文
            OperateResult<byte[]> result = SendAndReceive(sendCommand.ToArray());

            if (result.IsSuccess)
            {
                //第四步：验证报文
                if (result.Content.Length == 8 && CheckCRC(result.Content))
                {
                    //第五步：解析报文
                    if(ByteArrayLib.GetByteArrayEquals(result.Content, sendCommand.ToArray()))
                    {
                        return OperateResult.CreateSuccessResult();
                    }
                    else
                    {
                        return OperateResult.CreateFailResult("返回报文与接收报文不一致" + BitConverter.ToString(result.Content));
                    }

                        
                }
                else
                {
                    return OperateResult.CreateFailResult("返回报文错误" + BitConverter.ToString(result.Content));
                }
            }
            else
            {
                return OperateResult.CreateFailResult<byte[]>(result.Message);
            }
        }   
        public OperateResult WriteSingleRegister(ushort start, byte[] value, byte slaveId = 1)
        {
            if(value == null || value.Length != 2)
            {
                return OperateResult.CreateFailResult("写入寄存器的值必须是2字节");
            }

            //发送报文格式
            //从站地址 + 功能码 + 起始寄存器地址  + 写入值 + CRC校验

            //第一步：构造请求报文
            List<byte> sendCommand = new List<byte>();
            sendCommand.Add(slaveId);
            sendCommand.Add(0x06); //功能码：写单个线圈
            sendCommand.Add((byte)(start / 256)); //起始地址高字节
            sendCommand.Add((byte)(start % 256)); //起始地址低字节
            sendCommand.AddRange(value); //写入值
            sendCommand.AddRange(CRC16(sendCommand.ToArray(), sendCommand.Count)); //CRC校验低字节

            //第二步：发送报文
            //第三步：接收报文
            OperateResult<byte[]> result = SendAndReceive(sendCommand.ToArray());

            if (result.IsSuccess)
            {
                //第四步：验证报文
                if (result.Content.Length == 8 && CheckCRC(result.Content))
                {
                    //第五步：解析报文
                    if (ByteArrayLib.GetByteArrayEquals(result.Content, sendCommand.ToArray()))
                    {
                        return OperateResult.CreateSuccessResult();
                    }
                    else
                    {
                        return OperateResult.CreateFailResult("返回报文与接收报文不一致" + BitConverter.ToString(result.Content));
                    }


                }
                else
                {
                    return OperateResult.CreateFailResult("返回报文错误" + BitConverter.ToString(result.Content));
                }
            }
            else
            {
                return OperateResult.CreateFailResult<byte[]>(result.Message);
            }
        }


        public OperateResult WriteMultipleCoils(ushort start, bool[] values, byte slaveId = 1)
        {
            //发送报文格式
            //从站地址 + 功能码 + 起始线圈地址 + 线圈数量 + 字节计数 + 写入值 + CRC校验

            //第一步：构造请求报文
            byte[] wByteArray = ByteArrayLib.GetByteArrayFromBoolArray(values);

            var sendCommand = new List<byte>();
            sendCommand.Add(slaveId);
            sendCommand.Add(0x0F); //功能码：写多个线圈
            sendCommand.Add((byte)(start / 256)); //起始地址高字节
            sendCommand.Add((byte)(start % 256)); //起始地址低字节
            sendCommand.Add((byte)(values.Length / 256)); //线圈数量高字节
            sendCommand.Add((byte)(values.Length % 256)); //线圈数量低字节
            sendCommand.Add((byte)wByteArray.Length); //字节计数
            sendCommand.AddRange(wByteArray); //写入值
            sendCommand.AddRange(CRC16(sendCommand.ToArray(), sendCommand.Count)); //CRC校验低字节


            //第二步：发送报文
            //第三步：接收报文
            OperateResult<byte[]> result = SendAndReceive(sendCommand.ToArray());

            if (result.IsSuccess)
            {
                //第四步：验证报文
                if (result.Content.Length == 8 && CheckCRC(result.Content))
                {
                    //第五步：解析报文
                    if (ByteArrayLib.GetByteArrayEquals(result.Content.Take(6).ToArray(), sendCommand.Take(6).ToArray()))
                    {
                        return OperateResult.CreateSuccessResult();
                    } 
                    else
                    {
                        return OperateResult.CreateFailResult("接收报文不正确" + BitConverter.ToString(result.Content));
                    }
                }
                else
                {
                    return OperateResult.CreateFailResult("返回报文错误" + BitConverter.ToString(result.Content));
                }
            }
            else
            {
                return OperateResult.CreateFailResult<byte[]>(result.Message);
            }
        }

        public OperateResult WriteMultipleRegisters(ushort start, byte[] values, byte slaveId = 1)
        {
            if (values == null || values.Length % 2 != 0)
            {
                return OperateResult.CreateFailResult("写入寄存器的值必须是偶数个字节");
            }

            //发送报文格式
            //从站地址 + 功能码 + 起始寄存器地址 + 寄存器数量 + 字节计数 + 写入值 + CRC校验

            //第一步：构造请求报文
   
            var sendCommand = new List<byte>();
            sendCommand.Add(slaveId);
            sendCommand.Add(0x10); //功能码：写多个线圈
            sendCommand.Add((byte)(start / 256)); //起始地址高字节
            sendCommand.Add((byte)(start % 256)); //起始地址低字节
            sendCommand.Add((byte)(values.Length/2 / 256)); //线圈数量高字节
            sendCommand.Add((byte)(values.Length/2 % 256)); //线圈数量低字节
            sendCommand.Add((byte)values.Length); //字节计数
            sendCommand.AddRange(values); //写入值
            sendCommand.AddRange(CRC16(sendCommand.ToArray(), sendCommand.Count)); //CRC校验低字节

            //第二步：发送报文
            //第三步：接收报文
            OperateResult<byte[]> result = SendAndReceive(sendCommand.ToArray());

            if (result.IsSuccess)
            {
                //第四步：验证报文
                if (result.Content.Length == 8 && CheckCRC(result.Content))
                {
                    //第五步：解析报文
                    if (ByteArrayLib.GetByteArrayEquals(result.Content.Take(6).ToArray(), sendCommand.Take(6).ToArray()))
                    {
                        return OperateResult.CreateSuccessResult();
                    }
                    else
                    {
                        return OperateResult.CreateFailResult("接收报文不正确" + BitConverter.ToString(result.Content));
                    }
                }
                else
                {
                    return OperateResult.CreateFailResult("返回报文错误" + BitConverter.ToString(result.Content));
                }
            }
            else
            {
                return OperateResult.CreateFailResult<byte[]>(result.Message);
            }
        }

        public OperateResult WriteRegisterBit(string address,bool value,bool isLittleEndian = true, byte slaveId = 1)
        {
            //address 的格式必须为0.10，其中0表示寄存器地址，10表示寄存器内的位地址
            if(address.Contains(".")&&address.Split('.').Length == 2)
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
                        return readResult ;
                        
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



        #region  CRC校验

        private readonly byte[] aucCRCHi = {
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40
         };
        private readonly byte[] aucCRCLo = {
             0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 0x07, 0xC7,
             0x05, 0xC5, 0xC4, 0x04, 0xCC, 0x0C, 0x0D, 0xCD, 0x0F, 0xCF, 0xCE, 0x0E,
             0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09, 0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9,
             0x1B, 0xDB, 0xDA, 0x1A, 0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 0x1D, 0x1C, 0xDC,
             0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
             0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32,
             0x36, 0xF6, 0xF7, 0x37, 0xF5, 0x35, 0x34, 0xF4, 0x3C, 0xFC, 0xFD, 0x3D,
             0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A, 0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38,
             0x28, 0xE8, 0xE9, 0x29, 0xEB, 0x2B, 0x2A, 0xEA, 0xEE, 0x2E, 0x2F, 0xEF,
             0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
             0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1,
             0x63, 0xA3, 0xA2, 0x62, 0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4,
             0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F, 0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB,
             0x69, 0xA9, 0xA8, 0x68, 0x78, 0xB8, 0xB9, 0x79, 0xBB, 0x7B, 0x7A, 0xBA,
             0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
             0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0,
             0x50, 0x90, 0x91, 0x51, 0x93, 0x53, 0x52, 0x92, 0x96, 0x56, 0x57, 0x97,
             0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C, 0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E,
             0x5A, 0x9A, 0x9B, 0x5B, 0x99, 0x59, 0x58, 0x98, 0x88, 0x48, 0x49, 0x89,
             0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
             0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83,
             0x41, 0x81, 0x80, 0x40
         };

        private byte[] CRC16(byte[] data, int length)
        {
            int i = 0;
            byte[] res = new byte[2] { 0xFF, 0xFF };
            ushort iIndex;
            while (length-- > 0)
            {
                iIndex = (ushort)(res[0] ^ data[i++]);
                res[0] = (byte)(res[1] ^ aucCRCHi[iIndex]);
                res[1] = aucCRCLo[iIndex];
            }
            return res;
        }

  
        private bool CheckCRC(byte[] value)
        {
            if (value == null || value.Length <= 2) return false;

            // 取出报文前 N-2 个字节计算 CRC
            byte[] calculatedCrc = CRC16(value, value.Length - 2);

            // 直接拿计算结果和报文末尾的两个字节对比 (0 内存分配)
            return calculatedCrc[0] == value[value.Length - 2] &&
                   calculatedCrc[1] == value[value.Length - 1];
        }

        #endregion
    }
}
