using System;
using System.Linq;
using Tool;
using System.Collections.Generic;

namespace Server.DBLocal
{
    public class D_Conn : DBComponent
    {
        public bool Add(string name, string conn)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(conn))
                return false;

            return Context(db =>
            {
                DConn dConn = new DConn
                {
                    Cid = TRandom.Instance.GetRandomString(20),
                    Connname = name,
                    Conntext = conn,
                    Operator = "hjh",
                    Createtime = DateTime.Now,
                    Systime = DateTime.Now
                };

                db.DConns.InsertOnSubmit(dConn);

                db.SubmitChanges();

                return true;
            });
        }

        public string GetConn(string name)
        {
            return Context(db =>
            {
                return db.DConns.Where(i => i.Connname.Equals(name)).FirstOrDefault().Conntext;
            });
        }

        public List<string> GetAllName()
        {
            return Context(db =>
            {
                List<DConn> dConns = db.DConns.ToList();
                List<string> dConnNames = new List<string>();

                foreach(var item in dConns)
                {
                    dConnNames.Add(item.Connname);
                }

                return dConnNames;
            });
        }
    }
}
