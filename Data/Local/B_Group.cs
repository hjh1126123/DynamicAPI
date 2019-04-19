using EntityLocal;
using System;
using System.Linq;
using System.Collections.Generic;
using Util;

namespace Data.Local
{
    public class Group
    {
        string name;
        string describe;

        public string Name { get => name; set => name = value; }
        public string Describe { get => describe; set => describe = value; }
    }

    public class B_Group : DBComponent
    {
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public List<BGroup> SelectAll()
        {
            return Context(db =>
            {
                return db.BGroups.ToList();
            });
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public bool Add(Group group)
        {
            bool isComplete = false;

            if (string.IsNullOrWhiteSpace(group.Name) || string.IsNullOrWhiteSpace(group.Describe))
                return isComplete;

            return Context(db =>
            {
                db.BGroups.InsertOnSubmit(new BGroup
                {
                    Gid = URandom.Instance.GetRandomString(10),
                    Gname = group.Name,
                    Gdescribe = group.Describe,
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
