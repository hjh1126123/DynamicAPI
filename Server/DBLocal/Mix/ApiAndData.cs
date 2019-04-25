using Newtonsoft.Json;
using System.Linq;

namespace Server.DBLocal
{
    public class ApiAndData : DBComponent
    {
        public object SelectData(string requestKey, params string[] parvls)
        {
            return Context(db =>
            {
                IApi api = db.IApis.Where(i => i.RequestKey.Equals(requestKey)).FirstOrDefault();

                object data;
                if (api.Pattern.Equals("缓存查询"))
                {
                    data = JsonConvert.DeserializeObject(db.DDatas.Where(i => i.Did.Equals(api.Did)).FirstOrDefault().Data);
                }
                else
                {
                    string commandText = db.DMsSQLs.Where(i => i.Sid.Equals(api.Sid)).FirstOrDefault().Sql;
                    
                }


                //return data;
            });
        }
    }
}
