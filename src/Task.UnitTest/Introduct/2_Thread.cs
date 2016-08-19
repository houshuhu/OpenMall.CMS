using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tasks.UnitTest.Introduct
{
    /// <summary>
    /// 开启线程：计算耗时
    /// </summary>
    [TestClass]
    public class ThreadClass
    {
        public delegate void Dd();

        [TestMethod]
        public void Main()
        {
            Stopwatch stopwatch=new Stopwatch();
            stopwatch.Start();
            //开启两个线程
            Thread mainThread=new Thread(MainMethod);
            Thread elapTread=new Thread(ElapsedtimeMethod);
            mainThread.Start();
            elapTread.Start();
            //阻塞两个线程，等待两个线程全部执行完毕
            mainThread.Join();
            elapTread.Join();

            stopwatch.Stop();
            var result = stopwatch.ElapsedMilliseconds;
            Console.WriteLine(result);

        }

        public void MainMethod()
        {
            Thread.Sleep(1000);
        }

        /// <summary>
        /// 耗时方法：模拟耗时操作，例如IO，Http等
        /// </summary>
        /// <returns></returns>
        public void ElapsedtimeMethod()
        {
            Thread.Sleep(3000);
        }
        
    }
}