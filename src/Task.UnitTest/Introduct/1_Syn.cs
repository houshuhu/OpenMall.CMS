using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tasks.UnitTest.Introduct
{
    /// <summary>
    /// 同步方法：计算耗时
    /// </summary>
    [TestClass]
    public class Syn
    {
        [TestMethod]
        public void Main()
        {
            Stopwatch stopwatch=new Stopwatch();
            stopwatch.Start();
            MainMethod();
            ElapsedtimeMethod();
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