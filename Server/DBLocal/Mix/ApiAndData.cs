using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

namespace Server.DBLocal
{
    public class ApiAndData : DBComponent
    {
        public object SelectData(string requestKey, Dictionary<string,string> @params = null)
        {
            return Context(db =>
            {
                IApi api = db.IApis.Where(i => i.RequestKey.Equals(requestKey)).FirstOrDefault();

                object data;
                if (api.Pattern.Equals("缓存查询"))
                {
                    data = JsonConvert.DeserializeObject(db.DDatas.Where(i => i.Did.Equals(api.Did)).FirstOrDefault().Data);
                }
                else if (api.Pattern.Equals("即时查询"))
                {
                    string commandText = db.DMsSQLs.Where(i => i.Sid.Equals(api.Sid)).FirstOrDefault().Sql;
                    data = null;
                }
                else
                {
                    data = null;
                }


                return data;
            });
        }
    }
}
