using System.Net;

namespace Server.API
{
    public class ApiResultModel
    {
        private HttpStatusCode httpStatusCode;
        private object data;
        private string errorMessage;
        private bool isSuccess;

        public HttpStatusCode HttpStatusCode { get => httpStatusCode; set => httpStatusCode = value; }
        public object Data { get => data; set => data = value; }
        public string ErrorMessage { get => errorMessage; set => errorMessage = value; }
        public bool IsSuccess { get => isSuccess; set => isSuccess = value; }
    }
}
