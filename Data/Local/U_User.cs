using DB;
using System.Collections.Generic;
using System.Linq;

namespace Data.Local
{
    public class U_User
    {
        /// <summary>
        /// 检查用户密码
        /// </summary>
        /// <returns></returns>
        public List<UUser> CheckUser(string userid, string password)
        {
            List<UUser> uUsers = new List<UUser>();
            using (DBContext reportDataContext = new DBContext())
            {
                uUsers = reportDataContext.UUsers.Where(n => n.Userid.Equals(userid) && n.Password.Equals(password)).ToList();
            }
            return uUsers;
        }
    }
}
