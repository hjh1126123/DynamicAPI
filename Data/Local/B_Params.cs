using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Local
{
    public class Params
    {
        private string pid;
        private string name;
        private string key;
        private string describe;

        public string Pid { get => pid; set => pid = value; }
        public string Name { get => name; set => name = value; }
        public string Key { get => key; set => key = value; }
        public string Describe { get => describe; set => describe = value; }
    }

    public class B_Params : DBComponent
    {

    }
}
