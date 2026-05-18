using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.PressurizationStationPro
{
    public class SysAdminService
    {
        /// <summary>
        /// 获取所有用户对象
        /// </summary>
        /// <returns></returns>
        public List<SysAdmin> QuerySysAdmins()
        {
            string sql = "SELECT LoginId, LoginName, LoginPwd, RoleName FROM SysAdmin";

            SQLiteDataReader dataReader = SQLiteHelper.ExecuteReader(sql);

            List<SysAdmin> sysAdmins = new List<SysAdmin>();

            while (dataReader.Read())
            {

                sysAdmins.Add(new SysAdmin
                {
                    LoginId = Convert.ToInt32(dataReader["LoginId"]),
                    LoginName = dataReader["LoginName"].ToString(),
                    LoginPwd = dataReader["LoginPwd"].ToString(),
                    RoleName = (RoleName)Enum.Parse(typeof(RoleName), dataReader["RoleName"].ToString())

                });
            }
            dataReader.Close();
            return sysAdmins;
        }

        /// <summary>
        /// 用户登录验证
        /// </summary>
        /// <param name="sysAdmin"></param>
        /// <returns></returns>
        public SysAdmin AdminLogin(SysAdmin sysAdmin)
        {
            string sql = "SELECT RoleName FROM SysAdmin WHERE LoginName = @LoginName AND LoginPwd = @LoginPwd";

            SQLiteParameter[] parametes = new SQLiteParameter[]
            {
                new SQLiteParameter("@LoginName", sysAdmin.LoginName),
                new SQLiteParameter("@LoginPwd", sysAdmin.LoginPwd)
            };

            SQLiteDataReader dataReader = SQLiteHelper.ExecuteReader(sql, parametes);

            if (dataReader.Read())
            {
                sysAdmin.RoleName = (RoleName)Enum.Parse(typeof(RoleName), dataReader["RoleName"].ToString());
            }
            else
            {
                sysAdmin = null;
            }
            dataReader.Close();

            return sysAdmin;
        }


        /// <summary>
        /// 增加用户对象
        /// </summary>
        /// <param name="sysAdmin"></param>
        /// <returns></returns>
        public bool AddSysAdmin(SysAdmin sysAdmin)
        {
            string sql = "INSERT INTO SysAdmin (LoginName, LoginPwd, RoleName) VALUES (@LoginName, @LoginPwd, @RoleName)";
            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@LoginName", sysAdmin.LoginName),
                new SQLiteParameter("@LoginPwd", sysAdmin.LoginPwd),
                new SQLiteParameter("@RoleName", sysAdmin.RoleName.ToString())
            };
            return SQLiteHelper.ExecuteNonQuery(sql, parameters) == 1;
        }

        /// <summary>
        /// 修改用户对象
        /// </summary>
        /// <param name="sysAdmin"></param>
        /// <returns></returns>
        public bool ModifySysAdmin(SysAdmin sysAdmin)
        {
            string sql = "UPDATE SysAdmin SET LoginName = @LoginName, LoginPwd = @LoginPwd, RoleName = @RoleName WHERE LoginId = @LoginId";
            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@LoginName", sysAdmin.LoginName),
                new SQLiteParameter("@LoginPwd", sysAdmin.LoginPwd),
                new SQLiteParameter("@RoleName", sysAdmin.RoleName.ToString()),
                new SQLiteParameter("@LoginId", sysAdmin.LoginId)
            };
            return SQLiteHelper.ExecuteNonQuery(sql, parameters) == 1;
        }

        public bool DeleteSysAdmin(int loginId)
        {
            string sql = "DELETE FROM SysAdmin WHERE LoginId = @LoginId";
            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@LoginId", loginId)
            };      
            return SQLiteHelper.ExecuteNonQuery(sql, parameters) == 1;
        }
    }
}
