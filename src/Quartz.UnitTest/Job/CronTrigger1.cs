using System;
using Quartz.Impl;

namespace Quartz.UnitTest.Job
{
    public class CronTrigger1
    {
        public void Run()
        {
            //1.创建作业调度池
            var factory = new StdSchedulerFactory();
            var sched = factory.GetScheduler();

            //2.创建作业(配置任务的名称，分组名称，描述)
            IJobDetail job = JobBuilder.Create<SimpleJob1>().WithIdentity("jobname_c1","jobgroup_c1").WithDescription("jobdeccription_c1").Build();
            
           //NextGivenSecondDate：如果第一个参数为null则表名当前时间往后推迟2秒的时间点。（开始时间，结束时间）
            DateTimeOffset startTime = DateBuilder.NextGivenSecondDate(DateTime.Now.AddSeconds(1), 2);
            DateTimeOffset endTime = DateBuilder.NextGivenSecondDate(DateTime.Now.AddYears(2), 3);

            //3.创建配置（复杂任务）触发器（配置任务调度开始时间，结束时间，间隔时间）
            //触发器的名称，分组名称，描述
            ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create().StartAt(startTime).EndAt(endTime)
                .WithCronSchedule("0/3 * * * * ?")
                .WithIdentity("cron_name","cron_grop")
                .WithDescription("cron_description")
                .Build();


            //4.加入作业调度池中
            sched.ScheduleJob(job, trigger);
            sched.Start();
        }
    }
}