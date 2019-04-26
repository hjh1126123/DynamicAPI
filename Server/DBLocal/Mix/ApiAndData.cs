using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Server.DBLocal
{
    public class ApiAndData : DBComponent
    {
        public JObject SelectData(string requestKey, JObject jObjs)
        {
            return Context(db =>
            {
                IApi api = db.IApis.Where(i => i.RequestKey.Equals(requestKey)).FirstOrDefault();

                JObject data;
                if (api.Pattern.Equals("缓存查询"))
                {
                    data = JObject.FromObject(db.DDatas.Where(i => i.Did.Equals(api.Did)).FirstOrDefault().Data);
                }
                else if (api.Pattern.Equals("即时查询"))
                {
                    string commandText = db.DMsSQLs.Where(i => i.Sid.Equals(api.Sid)).FirstOrDefault().Sql;
                    Dictionary<string, List<string>> @params = new Dictionary<string, List<string>>();
                    foreach(var item in jObjs)
                    {
                        @params.Add(item.Key, item.Value.ToString().Split(',').ToList());
                    }
                    data = JObject.FromObject(ServerKeeper.Instance.DBNetKeeper.Select(commandText, @params));
                }
                else
                {
                    data = new JObject();
                    data.Add(new JProperty("ex", "无查询模式"));
                    data.Add(new JProperty("message", "没有为该接口设定任意一个查询模式"));
                }

                return data;
            });
        }
    }
}
