using System;
using System.Collections.Generic;

namespace Server.DBNet.ParamsComponents
{
    public class Stime : IParams
    {
        public string GetKey()
        {
            return "@st";
        }

        public string GetValue()
        {
            return DateTime.Now.ToString("yyyy-01-01 00:00:00");
        }

        public List<string> GetValues()
        {
            return null;
        }
    }
}
