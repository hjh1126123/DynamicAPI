using System;
using System.Linq;
using Tool;

namespace Server.DBLocal
{
    public class D_Data : DBComponent
    {
        public string SelectOne(string did)
        {
            return Context(db =>
            {
                return db.DDatas.Where(i => i.Did.Equals(did)).FirstOrDefault().Data;
            });
        }

        public DData Add(string apiId,string data)
        {
            return Context(db =>
            {
                var dd = new DData
                {
                    Did = TRandom.Instance.GetRandomString(10),
                    Data = data,
                    Createtime = DateTime.Now,
                    Systime = DateTime.Now,
                    Operator = "hjh"
                };

                db.DDatas.InsertOnSubmit(dd);

                db.SubmitChanges();

                return dd;
            });
        }
    }
}
