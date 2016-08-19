using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Tasks.UnitTest
{
    [TestClass]
    public class NewTask
    {
        /// <summary>
        /// 创建与启动线程任务
        /// </summary>
        [TestMethod]
        public  void CreateTask()
        {
            Test1();
        }

        public async void Test1()
        {
            var a = await Async1();
            Console.WriteLine(a);
        }

        public async Task<long> Async1()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var a = await GetTask();
            var b = await GetTask1();
            watch.Stop();
            long c = watch.ElapsedMilliseconds;
            return c;
        }

        public Task<int> GetTask()
        {
            Thread.Sleep(4000);
            return Task.FromResult(1);
        }
        public Task<int> GetTask1()
        {
            Thread.Sleep(2000);
            return Task.FromResult(2);
        }
    }
}