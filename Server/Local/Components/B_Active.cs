using System;
using System.Collections.Generic;
using System.Linq;
using Tool;

namespace Server.Local
{
    public class Active
    {        
        private string name;
        private string describe;
        private string gid;

        public string Name { get => name; set => name = value; }
        public string Describe { get => describe; set => describe = value; }
        public string Gid { get => gid; set => gid = value; }
    }

    public class B_Active : DBComponent
    {
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public List<BActive> SelectAll()
        {
            return Context(db =>
            {
                return db.BActives.ToList();
            });
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public BActive Add(Active active)
        {
            if (string.IsNullOrWhiteSpace(active.Name) || string.IsNullOrWhiteSpace(active.Describe))
            {                
                return null;
            }

            return Context(db =>
            {
                var bActive = new BActive
                {
                    Aid = TRandom.Instance.GetRandomString(10),
                    Gid = active.Gid,
                    Aname = active.Name,
                    Adescribe = active.Describe,
                    Operator = "hjh",
                    Createtime = DateTime.Now,
                    Systime = DateTime.Now
                };

                db.BActives.InsertOnSubmit(bActive);

                db.SubmitChanges();

                return bActive;
            });
        }
    }
}
