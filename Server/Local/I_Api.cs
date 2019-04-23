﻿using EntityLocal;
using System;
using System.Linq;
using System.Collections.Generic;
using Tool;

namespace Server.Local
{
    public class Api
    {
        long id;
        string requestKey;
        string apiName;
        string apiDescribe;
        string chart;
        string cid;
        string sid;
        string pattern;

        public long Id { get => id; set => id = value; }
        public string RequestKey { get => requestKey; set => requestKey = value; }
        public string ApiName { get => apiName; set => apiName = value; }
        public string ApiDescribe { get => apiDescribe; set => apiDescribe = value; }
        public string Chart { get => chart; set => chart = value; }
        public string Cid { get => cid; set => cid = value; }
        public string Sid { get => sid; set => sid = value; }
        public string Pattern { get => pattern; set => pattern = value; }
    }

    public class I_Api : DBComponent
    {
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public List<IApi> SelectAll()
        {
            return Context(db =>
            {
                return db.IApis.ToList();
            });
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="api"></param>
        /// <returns></returns>
        public bool Add(Api api)
        {
            return Context(db =>
            {
                db.IApis.InsertOnSubmit(new IApi
                {
                    Apikey = TRandom.Instance.GetRandomString(10),
                    Apiname = api.ApiName,
                    Apidescribe = api.ApiDescribe,
                    RequestKey = api.RequestKey,
                    Pattern = api.Pattern,
                    Chart = api.Chart,                    
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
        /// <param name="api"></param>
        /// <returns></returns>
        public bool Update(Api api)
        {
            return Context(db =>
            {
                IApi iApi = db.IApis.Where(i => i.Id == api.Id).FirstOrDefault();
                if (iApi == null)
                    return false;

                iApi.Apiname = string.IsNullOrWhiteSpace(api.ApiName) ? iApi.Apiname : api.ApiName;
                iApi.Sid = string.IsNullOrWhiteSpace(api.Sid) ? iApi.Sid : api.Sid;
                iApi.Cid = string.IsNullOrWhiteSpace(api.Cid) ? iApi.Cid : api.Cid;
                iApi.Pattern = string.IsNullOrWhiteSpace(api.Pattern) ? iApi.Pattern : api.Pattern;
                iApi.Apidescribe = string.IsNullOrWhiteSpace(api.ApiDescribe) ? iApi.Apidescribe : api.ApiDescribe;
                iApi.Chart = string.IsNullOrWhiteSpace(api.Chart) ? iApi.Chart : api.Chart;
                iApi.Systime = DateTime.Now;

                db.SubmitChanges();

                return true;
            });
        }
        
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="api"></param>
        /// <returns></returns>
        public bool Delete(Api api)
        {
            return Context(db =>
            {
                IApi iApi = db.IApis.Where(i => i.Id == api.Id).FirstOrDefault();
                if (iApi == null)
                    return false;

                db.IApis.DeleteOnSubmit(iApi);

                db.SubmitChanges();

                return true;
            });
        }
    }
}
