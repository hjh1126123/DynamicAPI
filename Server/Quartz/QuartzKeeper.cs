using Quartz;
using Quartz.Impl;
using System;
using Tool;
using System.Collections.Generic;

namespace Server.Quartz
{
    public class QuartzKeeper : IServer
    {
        StdSchedulerFactory schedulerFactory;
        IScheduler scheduler;

        public IJobDetail CreateJob(Type job, string name, string group)
        {
            IJobDetail jobDetail = JobBuilder.Create(job).Build();

            jobDetail = jobDetail.GetJobBuilder().WithIdentity(name, group).Build();

            return jobDetail;
        }

        public ITrigger CreateTriggerStartNow(string name, string group)
        {            
            return TriggerBuilder.Create().StartNow().WithIdentity(name, group).Build();
        }

        public ITrigger CreateTrigger(string name, string group, string cron)
        {
            return TriggerBuilder.Create().WithIdentity(name, group).WithCronSchedule(cron).Build();
        }

        public void QuartzStart()
        {
            if (scheduler != null)
                scheduler.Start();
        }

        public void QuartzShutDown()
        {
            if (scheduler != null)
                scheduler.Shutdown();
        }

        public QuartzKeeper(ServerKeeper serverKeeper) : base(serverKeeper)
        {
            schedulerFactory = new StdSchedulerFactory();
            scheduler = schedulerFactory.GetScheduler();
            
            ITrigger trigger = CreateTrigger("erveTime", "Core", "0 30 8 * * ? *");

            List<IJob> jobs = TReflection.Instance.GetClasses<IJob>("Server.Quartz.Jobs");

            foreach (var job in jobs)
            {
                IJobDetail jobDetail = CreateJob(job.GetType(), job.GetType().Name, "Core");
                scheduler.ScheduleJob(jobDetail, trigger);
                scheduler.TriggerJob(jobDetail.Key);
            }

            QuartzStart();   
        }
    }
}
