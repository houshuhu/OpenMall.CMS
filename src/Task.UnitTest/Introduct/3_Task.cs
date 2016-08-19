using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tasks.UnitTest.Introduct
{
    [TestClass]
    public class TaskClass
    {
        [TestMethod]
        public void Main()
        {
            Stopwatch stopwatch=new Stopwatch();
            stopwatch.Start();
            var task1 = Task.Run(() =>
            {
                Task1();
            });
            var task2 = Task.Run(() =>
            {
                Task2();
            });
            task1.Wait();
            task2.Wait();
            stopwatch.Stop();

            var result = stopwatch.ElapsedMilliseconds;
            Console.WriteLine(result);
        }

        public void Task1()
        {
            Task.Delay(2000);
        }

        public void Task2()
        {
            Task.Delay(4000);
        }
    }
}