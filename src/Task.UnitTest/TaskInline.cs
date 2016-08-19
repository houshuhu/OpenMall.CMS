using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tasks.UnitTest
{
    [TestClass]
    public class TaskInline
    {
        [TestMethod]
        public void Main()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Task<int> headTask = new Task<int>(DoSomeWork);
            headTask.Start();
            watch.Stop();
            long c = watch.ElapsedMilliseconds;
            Console.WriteLine("3个任务总共用时{0}毫秒",c);
            Console.WriteLine("结果和{0}", headTask.Result);
        }
        public int DoSomeWork()
        {
            Console.WriteLine("任务headTask运行在线程“{0}”上",
                Thread.CurrentThread.ManagedThreadId);
            var result = new List<int>();
            var taskTop = new Task(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("任务taskTop运行在线程“{0}”上",
                    Thread.CurrentThread.ManagedThreadId);

                result.Add(1);
            });
            var taskCenter = new Task(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("任务taskCenter运行在线程“{0}”上",
                    Thread.CurrentThread.ManagedThreadId);
                result.Add(2);
            });
            var taskBottom = new Task(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("任务taskBottom运行在线程“{0}”上",
                    Thread.CurrentThread.ManagedThreadId);
                result.Add(3);
            });
            taskTop.Start();
            taskCenter.Start();
            taskBottom.Start();
            Task.WaitAll(new Task[] { taskTop, taskCenter, taskBottom });
            return result.Sum(t => t);
        }
    }
}