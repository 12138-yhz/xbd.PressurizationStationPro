using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using xbd.DataConvertLib;

namespace xbd.ModbusLib
{

    //串口通信基类，提供串口通信的基本功能和接口
    public class SerialBase
    {
        private SerialPort serialPort;
        public int ReadTimeout { get; set; } = 1000; //读取超时时间，默认1000毫秒
        public int WriteTimeout { get; set; } = 1000; //写入超时时间，默认1000毫秒

        public int SleepTime { get; set; } = 20; //发送数据后等待响应的时间，默认20毫秒
        public int ReceiveTimeOut { get; set; } = 1000; //接收数据超时时间，默认1000毫秒

        private SimpleHybirdLock lockObj = new SimpleHybirdLock(); //线程同步锁，确保串口通信的线程安全

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="portName">串口名称</param>
        /// <param name="baudRate">波特率</param>
        /// <param name="parity">校验位</param>
        /// <param name="dataBits">数据位</param>
        /// <param name="stopBits">停止位</param>
        /// <returns></returns>
        public OperateResult Open(string portName, int baudRate = 9600, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            //串口是否打开
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }

            try
            {
                serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);

                serialPort.ReadTimeout = ReadTimeout;
                serialPort.WriteTimeout = WriteTimeout;

                serialPort.Open();
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult(ex.Message);
            }
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }

        /// <summary>
        /// 发送并接收数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResult<byte[]> SendAndReceive(byte[] request)
        {
            //获取锁，确保线程安全
            lockObj.Enter();

            if (serialPort == null || !serialPort.IsOpen)
            {
                return OperateResult.CreateFailResult<byte[]>("串口未打开");
            }

            //内存流
            MemoryStream memoryStream = new MemoryStream();

            try
            {
                //发送报文
                this.serialPort.Write(request, 0, request.Length);

                //发送时间
                DateTime start = DateTime.Now;

                byte[] buffer = new byte[1024];
                while (true)
                {
                    Thread.Sleep(SleepTime);

                    if (this.serialPort.BytesToRead > 0)
                    {
                        int bytesRead = this.serialPort.Read(buffer, 0, this.serialPort.BytesToRead);
                        memoryStream.Write(buffer, 0, bytesRead);
                    }
                    else
                    {
                        //是否超时
                        if ((DateTime.Now - start).TotalMilliseconds > ReceiveTimeOut)
                        {
                            memoryStream.Dispose();
                            return OperateResult.CreateFailResult<byte[]>("接收数据超时");
                        }
                        else if (memoryStream.Length > 0)
                        {
                            break;
                        }
                        else
                        {
                            //没有接收到数据，继续等待
                            continue;
                        }
                    }
                }

                byte[] response = memoryStream.ToArray();

                memoryStream.Dispose();

                return OperateResult.CreateSuccessResult(response);

            }
            catch (Exception ex)
            {

                return OperateResult.CreateFailResult<byte[]>(ex.Message);
            }
            finally
            {
                //释放锁
                lockObj.Leave();
            }
        }
    }
}
