using System;
using Quartz.Impl;

namespace Quartz.UnitTest.Job
{
    /// <summary>
    /// 简单任务触发器
    /// </summary>
    public class SimpleTrigger1
    {
        public void Run()
        {
            //1.创建作业调度池
            var factory = new StdSchedulerFactory();
            var sched=factory.GetScheduler();
            
            //2.创建作业
            IJobDetail job = JobBuilder.Create<SimpleJob1>().Build();
            //3.创建配置（简单任务）触发器
            ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create().WithSimpleSchedule(x => x.WithIntervalInSeconds(3).WithRepeatCount(int.MaxValue)).Build();
            //ISimpleTrigger trigger = new SimpleTriggerImpl("SimpleJob1", int.MaxValue,new TimeSpan(0,0,0,2));

            //4.加入作业调度池中
            sched.ScheduleJob(job, trigger);
            sched.Start();


        }
    }

    public class SimpleJob1 : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("DateTime Now: {0} \n",DateTime.Now);
        }
    }
}