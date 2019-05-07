using Quartz;
using Server.DBLocal;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;

namespace Server.Quartz.Jobs
{
    class CacheBuild : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            List<DMsSQL> dMsSQLs = ServerKeeper.Instance.DBLocalKeeper.DBObject<D_MsSQL>().Select();
            foreach (var dMsSql in dMsSQLs)
            {
                DataTable dataTable = ServerKeeper.Instance.DBNetKeeper.Select(dMsSql.Sql, new List<string>(dMsSql.Paramskey.Split(',')));
                if (!dMsSql.Strategy.Equals("不选择"))
                {                    
                    ServerKeeper.Instance.StrategyKeeper.Strategys[dMsSql.Strategy].Strategy.Operator(dataTable);
                }
                DData dData = ServerKeeper.Instance.DBLocalKeeper.DBObject<D_Data>().Add(dMsSql.Aid, JsonConvert.SerializeObject(dataTable));
                ServerKeeper.Instance.DBLocalKeeper.DBObject<I_Api>().Update(new Api { ApiId = dMsSql.Apiid, Did = dData.Did });
            }
        }
    }
}
