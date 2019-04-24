using Quartz;
using Server.Local;
using Server.Net;
using System.Collections.Generic;

namespace Server.Quartz.Jobs
{
    class CacheBuild : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            List<DMsSQL> dMsSQLs = DBKeeper.Instance.DBObject<D_MsSQL>().Select();
            foreach(var dMsSql in dMsSQLs)
            {
                List<SqlParameterModel> sqlParameterModels = new List<SqlParameterModel>();
                string[] pids = dMsSql.Paramskey.Split(',');
                foreach(var pid in pids)
                {

                }

                //ConnNet.Instance.Select(dMsSql.Sql,new List<SqlParameterModel>);
            }
        }
    }
}
