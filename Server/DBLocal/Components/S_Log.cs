using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.DBLocal
{
    public class Log
    {
        private string name;
        private string describe;
        private string gid;

        public string Name { get => name; set => name = value; }
        public string Describe { get => describe; set => describe = value; }
        public string Gid { get => gid; set => gid = value; }
    }

    public class S_Log : DBComponent
    {
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public List<SLog> SelectAll()
        {
            return Context(db =>
            {
                return db.SLogs.ToList();
            });
        }

        public void Info(string name, string log)
        {
            Add(new SLog
            {
                Systime = DateTime.Now,
                Createtime = DateTime.Now,
                Level = 0,
                Name = name,
                Operator = "hjh",
                Log = log,
                Isok = true
            });
        }

        public void Error(string name, string log)
        {
            Add(new SLog
            {
                Systime = DateTime.Now,
                Createtime = DateTime.Now,
                Level = 1,
                Name = name,
                Operator = "hjh",
                Log = log,
                Isok = false
            });
        }

        /// <summary>
        /// 完成某异常日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsOk(int id)
        {
            return ContextNoLog(db =>
            {
                SLog sLog = db.SLogs.Where(i => i.Id == id).FirstOrDefault();
                sLog.Isok = true;

                db.SubmitChanges();

                return true;
            });
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="sLog"></param>
        /// <returns></returns>
        private bool Add(SLog sLog)
        {
            return ContextNoLog(db =>
            {
                db.SLogs.InsertOnSubmit(sLog);

                db.SubmitChanges();

                return true;
            });
        }
    }
}
