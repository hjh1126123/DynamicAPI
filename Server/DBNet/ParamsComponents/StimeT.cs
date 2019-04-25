using System;
using System.Collections.Generic;

namespace Server.DBNet.ParamsComponents
{
    public class StimeT : IParams
    {
        public string GetKey()
        {
            return "@stT";
        }

        public string GetValue()
        {
            return DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
        }

        public List<string> GetValues()
        {
            return null;
        }
    }
}
