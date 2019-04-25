using Newtonsoft.Json.Linq;
using Server.DBLocal;
using System.Web.Http;

namespace Server.API.Controls
{
    [ApiPermissionFilter]
    public class RequestApi : ApiController
    {
        [HttpPost]
        public object GetData([FromBody] JObject obj)
        {
            if (obj.ContainsKey("api"))
            {
                return ServerKeeper.Instance.DBLocalKeeper.DBObject<ApiAndData>().SelectData(obj["api"].Value<string>());
            }
            else
            {
                return new
                {
                    name = "错误",
                    message = "找不到对应的obj"
                };
            }
        }
    }
}
