using EntityLocal;
using System;
using System.Collections.Generic;
using System.Linq;
using Util;

namespace Data.Local
{
    public class Active
    {
        private string name;
        private string describe;

        public string Name { get => name; set => name = value; }
        public string Describe { get => describe; set => describe = value; }
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
        public bool Add(Active active)
        {
            bool isComplete = false;

            if (string.IsNullOrWhiteSpace(active.Name) || string.IsNullOrWhiteSpace(active.Describe))
                return isComplete;

            return Context(db =>
            {
                db.BActives.InsertOnSubmit(new BActive
                {
                    Aid = URandom.Instance.GetRandomString(10),
                    Aname = active.Name,
                    Adescribe = active.Describe,
                    Operator = "hjh",
                    Createtime = DateTime.Now,
                    Systime = DateTime.Now
                });

                db.SubmitChanges();

                isComplete = true;
                return isComplete;
            });
        }
    }
}
