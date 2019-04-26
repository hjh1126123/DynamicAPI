using System.Net;
using Newtonsoft.Json.Linq;

namespace Server.API
{
    public class ApiResponseModel
    {
        private HttpStatusCode httpStatusCode;
        private JObject data;
        private string errorMessage;
        private bool isSuccess;

        public HttpStatusCode HttpStatusCode { get => httpStatusCode; set => httpStatusCode = value; }
        public JObject Data { get => data; set => data = value; }
        public string ErrorMessage { get => errorMessage; set => errorMessage = value; }
        public bool IsSuccess { get => isSuccess; set => isSuccess = value; }
    }
}
