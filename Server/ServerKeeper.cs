using Server.DBLocal;
using Server.DBNet;
using Server.Quartz;
using Server.Strategy;
using System;

namespace Server
{
    public class ServerKeeper : IDisposable
    {
        private static readonly Lazy<ServerKeeper> lazyInstance = new Lazy<ServerKeeper>(() => new ServerKeeper());
        public static ServerKeeper Instance
        {
            get
            {
                return lazyInstance.Value;
            }
        }


        public DBLocalKeeper DBLocalKeeper { get; private set; }
        public DBNetKeeper DBNetKeeper { get; private set; }
        public QuartzKeeper QuartzKeeper { get; private set; }
        public StrategyKeeper StrategyKeeper { get; private set; }
        private ServerKeeper()
        {
            DBLocalKeeper = new DBLocalKeeper(this);
            DBNetKeeper = new DBNetKeeper(this, "Net");
            QuartzKeeper = new QuartzKeeper(this);
            StrategyKeeper = new StrategyKeeper(this);
        }

        public void Dispose()
        {
            QuartzKeeper.QuartzShutDown();
        }
    }
}
