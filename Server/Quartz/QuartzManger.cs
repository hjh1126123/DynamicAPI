using Quartz;
using Quartz.Impl;
using System.Collections.Generic;

namespace Server.Quartz
{
    public class QuartzManger
    {
        StdSchedulerFactory schedulerFactory;
        IScheduler scheduler;
        
        public QuartzManger()
        {
            schedulerFactory = new StdSchedulerFactory();
            scheduler = schedulerFactory.GetScheduler();            
        }
    }
}
