using System;
using System.Linq;
using Tool;
using System.Collections.Generic;

namespace Server.DBLocal
{
    public class MsSQL
    {
        private int id;
        private string aid;
        private string apiId;
        private string sql;
        private string paramskey;
        private string strategy;

        public int Id { get => id; set => id = value; }
        public string ApiId { get => apiId; set => apiId = value; }
        public string Aid { get => aid; set => aid = value; }
        public string Sql { get => sql; set => sql = value; }
        public string Paramskey { get => paramskey; set => paramskey = value; }
        public string Strategy { get => strategy; set => strategy = value; }
    }

    public class D_MsSQL : DBComponent
    {
        public List<DMsSQL> Select()
        {
            return Context(db =>
            {
                return db.DMsSQLs.ToList();
            });
        }

        /// <summary>
        /// 查询某一条数据
        /// </summary>
        /// <param name="msSQL">查询对象</param>
        /// <returns></returns>
        public DMsSQL SelectOne(MsSQL msSQL)
        {
            return Context(db =>
            {
                if (string.IsNullOrWhiteSpace(msSQL.Aid))
                    return new DMsSQL();

                return db.DMsSQLs.Where(i => i.Aid.Equals(msSQL.Aid)).FirstOrDefault();
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
                    Sid = TRandom.Instance.GetRandomString(10),
                    Aid = msSQL.Aid,
                    Apiid = msSQL.ApiId,
                    Sql = msSQL.Sql,
                    Paramskey = msSQL.Paramskey,
                    Strategy = msSQL.Strategy,                    
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

                tmp.Sql = string.IsNullOrWhiteSpace(msSQL.Sql) ? tmp.Sql : msSQL.Sql;
                tmp.Apiid = string.IsNullOrWhiteSpace(msSQL.ApiId) ? tmp.Apiid : msSQL.ApiId;
                tmp.Paramskey = string.IsNullOrWhiteSpace(msSQL.Paramskey) ? tmp.Paramskey : msSQL.Paramskey;
                tmp.Strategy = string.IsNullOrWhiteSpace(msSQL.Strategy) ? tmp.Strategy : msSQL.Strategy;

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
