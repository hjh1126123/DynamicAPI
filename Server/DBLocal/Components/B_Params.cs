﻿using System;
using System.Collections.Generic;
using System.Linq;
using Tool;

namespace Server.DBLocal
{
    public class Params
    {
        private long id;
        private string pid;
        private string name;
        private string key;
        private bool multiple;
        private string describe;

        public long Id { get => id; set => id = value; }
        public string Pid { get => pid; set => pid = value; }
        public string Name { get => name; set => name = value; }
        public string Key { get => key; set => key = value; }
        public string Describe { get => describe; set => describe = value; }
        public bool Multiple { get => multiple; set => multiple = value; }

    }

    public class B_Params : DBComponent
    {
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public List<BParam> SelectAll()
        {
            return Context(db =>
            {
                return db.BParams.ToList();
            });
        }

        /// <summary>
        /// 根据pid获取某个值
        /// </summary>
        /// <param name="pid">pid</param>
        /// <returns></returns>
        public BParam Select(string pid, string name)
        {
            return Context(db =>
            {
                return db.BParams.Where(i => i.Pid.Equals(pid)).FirstOrDefault();
            });
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public bool Add(Params @params)
        {
            return Context(db =>
            {
                db.BParams.InsertOnSubmit(new BParam
                {
                    Pid = TRandom.Instance.GetRandomString(10),
                    Name = @params.Name,
                    Describe = @params.Describe,
                    Key = @params.Key,
                    Multiple = @params.Multiple,
                    Operator = "hjh",
                    Createtime = DateTime.Now,
                    Systime = DateTime.Now
                });

                db.SubmitChanges();

                return true;
            });
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public bool Update(Params @params)
        {
            return Context(db =>
            {
                BParam bParam = db.BParams.Where(i => i.Id == @params.Id).FirstOrDefault();
                if (bParam == null)
                    return false;

                bParam.Name = string.IsNullOrWhiteSpace(@params.Name) ? bParam.Name : @params.Name;
                bParam.Describe = string.IsNullOrWhiteSpace(@params.Describe) ? bParam.Describe : @params.Describe;
                bParam.Key = string.IsNullOrWhiteSpace(@params.Key) ? bParam.Key : @params.Key;
                bParam.Multiple = @params.Multiple;

                bParam.Systime = DateTime.Now;

                db.SubmitChanges();

                return true;
            });
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public bool Delete(Params @params)
        {
            return Context(db =>
            {
                BParam bParam = db.BParams.Where(i => i.Id == @params.Id).FirstOrDefault();
                if (bParam == null)
                    return false;

                db.BParams.DeleteOnSubmit(bParam);

                db.SubmitChanges();

                return true;
            });
        }
    }
}
