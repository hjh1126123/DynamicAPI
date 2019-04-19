using EntityLocal;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Local
{
    /// <summary>
    /// 查询条件对象
    /// </summary>
    public class Visiter
    {
        public string Ip { get; set; }
        public DateTime? SysTimeStart { get; set; }
        public DateTime? SysTimeEnd { get; set; }
    }

    public class U_Visiter : DBComponent
    {
        /// <summary>
        /// 获取某一条数据
        /// </summary>
        /// <param name="visiter">查询条件对象</param>
        /// <returns></returns>
        public List<UVisiter> Select(Visiter visiter)
        {
            return Context(db =>
            {
                var pre = PredicateBuilder.New<UVisiter>();
                if (!string.IsNullOrWhiteSpace(visiter.Ip))
                {
                    pre.And(v => v.Ip.Equals(visiter.Ip));
                }
                if (visiter.SysTimeStart != null)
                {
                    pre.And(v => v.Systime >= visiter.SysTimeStart);
                }
                if (visiter.SysTimeEnd != null)
                {
                    pre.And(v => v.Systime <= visiter.SysTimeEnd);
                }

                return db.UVisiters.AsExpandable().Where(pre).ToList();
            });
        }
    }
}
