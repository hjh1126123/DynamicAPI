using EntityLocal;
using System.Collections.Generic;
using System.Linq;

namespace Server.Local
{
    public class U_User : DBComponent
    {
        /// <summary>
        /// 检查用户密码
        /// </summary>
        /// <returns></returns>
        public List<UUser> CheckUser(string userid, string password)
        {
            return Context(db =>
            {
                return db.UUsers.Where(n => n.Userid.Equals(userid) && n.Password.Equals(password)).ToList();
            });
        }
    }
}
