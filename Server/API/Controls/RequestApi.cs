using Server.DBLocal;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace Server.API.Controls
{
    [ApiPermissionFilter]
    public class RequestApi : ApiController
    {
        [HttpPost]
        public JObject GetData([FromBody] ApiRequestModel request)
        {
            return ServerKeeper.Instance.DBLocalKeeper.DBObject<ApiAndData>().SelectData(request.RequestKey, request.Params);
        }
    }
}
