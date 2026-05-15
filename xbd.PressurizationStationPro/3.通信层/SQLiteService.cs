using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.PressurizationStationPro
{
    public class SQLiteService
    {
        public void SetConnectStr(string connStr)
        {
            SQLiteHelper.ConnString = connStr;
        }
    }
}
