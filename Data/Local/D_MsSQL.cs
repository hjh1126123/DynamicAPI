using EntityLocal;
using System.Collections.Generic;
using System.Linq;

namespace Data.Local
{
    public class MsSQL
    {
        private int id;
        private string group;
        private string active;
        private string sql;
        private string pid;
        private string @operator;

        public int Id { get => id; set => id = value; }
        public string Group { get => group; set => group = value; }
        public string Active { get => active; set => active = value; }
        public string Sql { get => sql; set => sql = value; }
        public string Pid { get => pid; set => pid = value; }
        public string Operator { get => @operator; set => @operator = value; }

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
            bool complete = false;
            return Context(db =>
            {
                db.DMsSQLs.InsertOnSubmit(new DMsSQL
                {
                    Group = msSQL.Group,
                    Active = msSQL.Active,
                    Sql = msSQL.Sql,
                    Pid = msSQL.Pid,
                    Operator = msSQL.Operator
                });
                db.SubmitChanges();

                complete = true;

                return complete;
            });
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="msSQL">更新对象</param>
        /// <returns></returns>
        public bool Update(MsSQL msSQL)
        {
            bool complete = false;
            return Context(db =>
            {
                var tmp = db.DMsSQLs.Where(i => i.Id == msSQL.Id).FirstOrDefault();

                db.DMsSQLs.Attach(tmp);

                if (!string.IsNullOrWhiteSpace(msSQL.Sql))
                {
                    tmp.Sql = msSQL.Sql;
                }
                db.SubmitChanges();

                complete = true;
                return complete;
            });
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">数据id</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            bool complete = false;
            return Context(db =>
            {
                var tmp = db.DMsSQLs.Where(i => i.Id == id).FirstOrDefault();
                db.DMsSQLs.DeleteOnSubmit(tmp);

                db.SubmitChanges();

                complete = true;
                return complete;
            });
        }
    }
}
