using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.API
{
    public class ApiRequestModel
    {
        string requestKey;
        JObject @params;

        public string RequestKey { get => requestKey; set => requestKey = value; }
        public JObject Params { get => @params; set => @params = value; }
    }
}
