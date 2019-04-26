using Newtonsoft.Json;
using Server.DBLocal;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json.Linq;

namespace Server.API
{
    public class ApiPermissionFilter : ActionFilterAttribute
    {        
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            ApiResponseModel apiResultModel = new ApiResponseModel();
            apiResultModel.HttpStatusCode = System.Net.HttpStatusCode.Unauthorized;
            apiResultModel.IsSuccess = false;            
            
            if (actionContext.Request.Headers.Contains("token"))
            {
                string token = actionContext.Request.Headers.GetValues("token").FirstOrDefault();
                bool pass = ServerKeeper.Instance.DBLocalKeeper.DBObject<T_Token>().CheckToken(token);
                if (pass)
                {                    
                    apiResultModel.HttpStatusCode = actionContext.Response.StatusCode;
                    apiResultModel.Data = actionContext.Response.Content.ReadAsAsync<JObject>().Result;
                    apiResultModel.IsSuccess = actionContext.Response.IsSuccessStatusCode;                    
                }
                else
                {
                    apiResultModel.ErrorMessage = "token不正确";
                }
            }
            else
            {
                apiResultModel.ErrorMessage = "未携带token的请求";
            }

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(apiResultModel), Encoding.UTF8, "application/json")
            };
            actionContext.Response = httpResponseMessage;
        }
    }
}
