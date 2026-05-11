using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xbd.s7netplus;

namespace xbd.PressurizationStationPro
{
    public class SysInfoService
    {

        /// <summary>
        /// 读取配置文件返回 SysInfo 对象
        /// </summary>
        /// <param name="path">INI 配置文件路径</param>
        /// <returns>填充好的 SysInfo 对象，读取失败返回 null</returns>
        public SysInfo GetSysInfoFromPath(string path)
        {
            try
            {
                // 实例化对象
                SysInfo sysInfo = new SysInfo();

                // 读取【通信参数】节点
                sysInfo.IPAddress = IniConfigHelper.ReadIniData("通信参数", "IP地址", "127.0.0.1", path);
                sysInfo.CpuType = (CpuType)Enum.Parse(typeof(CpuType),
                    IniConfigHelper.ReadIniData("通信参数", "CPU类型", "S7200Smart", path), true);
                sysInfo.Rack = Convert.ToInt16(IniConfigHelper.ReadIniData("通信参数", "机架号", "0", path));
                sysInfo.Slot = Convert.ToInt16(IniConfigHelper.ReadIniData("通信参数", "插槽号", "0", path));

                // 读取【系统参数】节点
                sysInfo.AutoStart = IniConfigHelper.ReadIniData("系统参数", "开机启动", "1", path) == "1";
                sysInfo.ScreenTime = Convert.ToInt32(IniConfigHelper.ReadIniData("系统参数", "熄屏时间", "0", path));
                sysInfo.LogoffTime = Convert.ToInt32(IniConfigHelper.ReadIniData("系统参数", "注销时间", "0", path));
                sysInfo.CameraIndex = Convert.ToInt32(IniConfigHelper.ReadIniData("系统参数", "摄像头序号", "0", path));

                return sysInfo;
            }
            catch (Exception)
            {
                // 发生任何异常（如文件不存在、格式错误、转换失败），返回 null
                return null;
            }
        }

        /// <summary>
        /// 将 SysInfo 对象写入到配置文件中
        /// </summary>
        /// <param name="sysInfo">要写入的 SysInfo 对象</param>
        /// <param name="path">INI 配置文件路径</param>
        /// <returns>是否全部写入成功</returns>
        public bool SetSysInfoToPath(SysInfo sysInfo, string path)
        {
            bool result = true;

            // 写入【通信参数】节点
            result &= IniConfigHelper.WriteIniData("通信参数", "IP地址", sysInfo.IPAddress, path);
            result &= IniConfigHelper.WriteIniData("通信参数", "CPU类型", sysInfo.CpuType.ToString(), path);
            result &= IniConfigHelper.WriteIniData("通信参数", "机架号", sysInfo.Rack.ToString(), path);
            result &= IniConfigHelper.WriteIniData("通信参数", "插槽号", sysInfo.Slot.ToString(), path);

            // 写入【系统参数】节点
            result &= IniConfigHelper.WriteIniData("系统参数", "开机启动", sysInfo.AutoStart ? "1" : "0", path);
            result &= IniConfigHelper.WriteIniData("系统参数", "熄屏时间", sysInfo.ScreenTime.ToString(), path);
            result &= IniConfigHelper.WriteIniData("系统参数", "注销时间", sysInfo.LogoffTime.ToString(), path);
            result &= IniConfigHelper.WriteIniData("系统参数", "摄像头序号", sysInfo.CameraIndex.ToString(), path);

            return result;
        }
    }
}
