using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DBNet
{
    public interface IParams
    {
        string GetKey();
        string GetValue();
        List<string> GetValues();
    }
}
