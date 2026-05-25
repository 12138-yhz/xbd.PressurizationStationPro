using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using xbd.DataConvertLib;

namespace xbd.ModbusLib.Base
{
    public class TCPBase
    {
        /// <summary>
        /// 创建一个TCP客户端对象
        /// </summary>
        private TcpClient tcpClient;

        /// <summary>
        /// 发送超时时间
        /// </summary>
        private int SendTimeOut { get; set; } = 1000;


        /// <summary>
        /// 接收超时时间
        /// </summary>
        private int ReceiveTimeOut { get; set; } = 1000;

        /// <summary>
        /// 连接超时时间
        /// </summary>
        private int ConnectTimeOut { get; set; } = 2000;


        /// <summary>
        /// 最大等待时间
        /// </summary>
        private int MaxWaitTime { get; set; } = 2000;

        //锁对象
        private SimpleHybirdLock simpleHybirdLock = new SimpleHybirdLock();

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public OperateResult Connect(string ip,int port)
        {
            //实例化
            this.tcpClient = new TcpClient();

            try
            {
                this.tcpClient.ConnectAsync(ip, port).Wait(ConnectTimeOut);

                if (this.tcpClient.Connected)
                {
                    this.tcpClient.SendTimeout = SendTimeOut;
                    this.tcpClient.ReceiveTimeout = ReceiveTimeOut;
                    return OperateResult.CreateSuccessResult();
                }
                else
                {
                    return OperateResult.CreateFailResult("连接失败");
                } 


            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult(ex.Message);
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void DisConnect()
        {
            if(this.tcpClient != null && this.tcpClient.Connected)
            {
                this.tcpClient.Close();
            }
        }

        /// <summary>
        /// 发送并接收
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResult<byte[]> SendAndReceive(byte[] request)
        {
            simpleHybirdLock.Enter();

            MemoryStream memoryStream = new MemoryStream();

            try
            {
                this.tcpClient.GetStream().Write(request, 0, request.Length);

                DateTime start = DateTime.Now;

                byte[] buffer = new byte[1024];

                while (true)
                {

                    if (this.tcpClient.Available > 0)
                    {
                        int cout = this.tcpClient.GetStream().Read(buffer, 0, buffer.Length);

                        memoryStream.Write(buffer, 0, cout);
                    }
                    else
                    {
                        if ((DateTime.Now - start).TotalMilliseconds > MaxWaitTime)
                        {
                            memoryStream.Dispose();
                            return OperateResult.CreateFailResult<byte[]>("请求超时");
                        }
                        else if (memoryStream.Length > 0)
                        {
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                byte[] result = memoryStream.ToArray();
                memoryStream.Dispose();

                return OperateResult.CreateSuccessResult(result);
            }
            catch (Exception ex)
            {

                return OperateResult.CreateFailResult<byte[]>("请求超时："+ex.Message);
            }
            finally
            {
                simpleHybirdLock.Leave();
            }
        }
    }
}
