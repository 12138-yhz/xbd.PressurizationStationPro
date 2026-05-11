using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xbd.DataConvertLib;
using xbd.s7netplus;
using DataType = xbd.s7netplus.DataType;

namespace xbd.PressurizationStationPro
{
    internal class S7NetLib
    {
        private Plc siemens;

        private static object lockObj = new object();

        public CpuType CpuType { get; set; }
        public string IPAddress { get; set; }
        public short Rack { get; set; }
        public short Slot { get; set; }

        public S7NetLib()
        {
        }

        public S7NetLib(CpuType cpuType, string ipAddress, short rack, short slot)
        {
            this.CpuType = cpuType;
            this.IPAddress = ipAddress;
            this.Rack = rack;
            this.Slot = slot;
        }


        /// <summary>
        /// 建立连接
        /// </summary>
        /// <returns></returns>
        public OperateResult Connect()
        {
            try
            {
                if (this.siemens != null && this.siemens.IsConnected)
                {
                    this.siemens.Close();
                }

                siemens = new Plc(CpuType, IPAddress, Rack, Slot);
                siemens.Open();
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult(ex.Message);
            }

            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Disconnect()
        {
            try
            {
                if (this.siemens != null && this.siemens.IsConnected)
                {
                    this.siemens.Close();
                }
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult(ex.Message);
            }
            return OperateResult.CreateSuccessResult();
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="db"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public OperateResult<byte[]> ReadByteArray(DataType dataType, int db, int start, int count)
        {
            try
            {
                lock (lockObj)
                {
                    var result = siemens.ReadBytes(dataType, db, start, count);
                    if (result != null)
                    {
                        return OperateResult.CreateSuccessResult(result);
                    }
                    else
                    {
                        return OperateResult.CreateFailResult<byte[]>("读取PLC数据失败");
                    }
                }


            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult<byte[]>(ex.Message);
            }
        }

        /// <summary>
        /// 读取单个变量
        /// </summary>
        /// <param name="varAddress"></param>
        /// <returns></returns>
        public OperateResult<object> ReadVariable(string varAddress)
        {
            try
            {
                lock (lockObj)
                {
                    var result = siemens.Read(varAddress);
                    if (result != null)
                    {
                        return OperateResult.CreateSuccessResult<object>(result);
                    }
                    else
                    {
                        return OperateResult.CreateFailResult<object>("读取PLC数据失败");
                    }
                }


            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult<object>(ex.Message);
            }
        }

        /// <summary>
        /// 读取单个类
        /// </summary>
        /// <param name="db"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public OperateResult<T> ReadClass<T>(int db, int start) where T : class
        {
            try
            {
                lock (lockObj)
                {
                    var result = siemens.ReadClass<T>(db, start);
                    if (result != null)
                    {
                        return OperateResult.CreateSuccessResult<T>(result);
                    }
                    else
                    {
                        return OperateResult.CreateFailResult<T>("读取PLC数据失败");
                    }
                }


            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult<T>(ex.Message);
            }
        }


        /// <summary>
        /// 单个变量写入PLC
        /// </summary>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public OperateResult WriteVariable(string address, object value)
        {
            try
            {
                lock (lockObj)
                {
                    siemens.Write(address, value);
                   
                    return OperateResult.CreateSuccessResult();
                }


            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult(ex.Message);
            }
        }
    }
}
