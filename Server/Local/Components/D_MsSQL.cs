using EntityLocal;
using System;
using System.Linq;
using Tool;

namespace Server.Local
{
    public class MsSQL
    {
        private int id;
        private string aid;
        private string apiKey;
        private string sql;
        private string paramskey;
        private string strategy;

        public int Id { get => id; set => id = value; }
        public string ApiKey { get => apiKey; set => apiKey = value; }
        public string Aid { get => aid; set => aid = value; }
        public string Sql { get => sql; set => sql = value; }
        public string Paramskey { get => paramskey; set => paramskey = value; }
        public string Strategy { get => strategy; set => strategy = value; }
    }

    public class D_MsSQL : DBComponent
    {
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
                    Apikey = msSQL.ApiKey,
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
                tmp.Apikey = string.IsNullOrWhiteSpace(msSQL.ApiKey) ? tmp.Apikey : msSQL.ApiKey;
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
