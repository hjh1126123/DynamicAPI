using Server.DBNet.Components;
using System.Collections.Generic;

namespace Server.DBNet.ParamsComponents
{
    public class CenterCode : IParams
    {
        public string GetKey()
        {
            return "@cc";
        }

        public string GetValue()
        {
            return string.Empty;
        }

        public List<string> GetValues()
        {
            return new BussCenter().GetAllCenterCode();
        }
    }
}
