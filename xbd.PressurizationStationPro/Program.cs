using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xbd.PressurizationStationPro
{
    internal static class Program
    {

        /// <summary>
        /// 锁屏时间滴答次数
        /// </summary>
        public static int TickCount { get;  set; }

        /// <summary>
        /// 当前登录的用户信息
        /// </summary>
        public static SysAdmin CurrentUser { get; set; }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            new SQLiteService().SetConnectStr($"Data Source={Application.StartupPath}\\DataBase\\PressurizationStationDataBase;Pooling=true;FillIfMissing=false;");

            Application.Run(new FrmMain());


        }

          
    }
}
