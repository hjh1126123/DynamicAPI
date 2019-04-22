using EntityLocal;
using System;
using System.Linq;

namespace Server.Local
{
    public class MsSQL
    {
        private int id;
        private string group;
        private string active;
        private string sql;
        private string pid;

        public int Id { get => id; set => id = value; }
        public string Group { get => group; set => group = value; }
        public string Active { get => active; set => active = value; }
        public string Sql { get => sql; set => sql = value; }
        public string Pid { get => pid; set => pid = value; }

    }

    public class D_MsSQL : DBComponent
    {
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="msSQL">查询对象</param>
        /// <returns></returns>
        public DMsSQL Select(MsSQL msSQL)
        {
            return Context(db =>
            {
                if (string.IsNullOrWhiteSpace(msSQL.Group) || string.IsNullOrWhiteSpace(msSQL.Active))
                {
                    return new DMsSQL();
                }
                else
                {
                    return db.DMsSQLs.Where(i => i.Group.Equals(msSQL.Group) && i.Active.Equals(msSQL.Active)).FirstOrDefault();
                }
            });
        }

        /// <summary>
        /// 添加一行数据
        /// </summary>
        /// <param name="msSQL">数据对象</param>
        /// <returns></returns>
        public bool Add(MsSQL msSQL)
        {
            return Context(db =>
            {
                db.DMsSQLs.InsertOnSubmit(new DMsSQL
                {
                    Group = msSQL.Group,
                    Active = msSQL.Active,
                    Sql = msSQL.Sql,
                    Pid = msSQL.Pid,
                    Operator = "hjh",
                    Systime = DateTime.Now,
                    Createtime = DateTime.Now
                });
                db.SubmitChanges();
                return true;
            });
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="msSQL">更新对象</param>
        /// <returns></returns>
        public bool Update(MsSQL msSQL)
        {
            return Context(db =>
            {
                var tmp = db.DMsSQLs.Where(i => i.Id == msSQL.Id).FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(msSQL.Group))
                    tmp.Group = msSQL.Group;

                if (!string.IsNullOrWhiteSpace(msSQL.Active))
                    tmp.Active = msSQL.Active;

                if (!string.IsNullOrWhiteSpace(msSQL.Sql))
                    tmp.Sql = msSQL.Sql;

                if (!string.IsNullOrWhiteSpace(msSQL.Pid))
                    tmp.Pid = msSQL.Pid;

                tmp.Systime = DateTime.Now;

                db.SubmitChanges();

                return true;
            });
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">数据id</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            return Context(db =>
            {
                var tmp = db.DMsSQLs.Where(i => i.Id == id).FirstOrDefault();
                if (tmp == null)
                    return false;

                db.DMsSQLs.DeleteOnSubmit(tmp);

                db.SubmitChanges();

                return true;
            });
        }
    }
}
