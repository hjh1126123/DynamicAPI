using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public abstract class IServer
    {
        protected ServerKeeper serverKeeper;
        public IServer(ServerKeeper serverKeeper)
        {
            this.serverKeeper = serverKeeper;
        }
    }
}
