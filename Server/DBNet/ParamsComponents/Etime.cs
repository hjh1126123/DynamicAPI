using System;
using System.Collections.Generic;

namespace Server.DBNet.ParamsComponents
{
    public class Etime : IParams
    {
        public string GetKey()
        {
            return "@et";
        }

        public string GetValue()
        {
            return DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
        }

        public List<string> GetValues()
        {
            return null;
        }
    }
}
