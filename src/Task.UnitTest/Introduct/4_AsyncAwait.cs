using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tasks.UnitTest.Introduct
{
    [TestClass]
    public class AsyncAwait
    {
        [TestMethod]
        public async Task Main()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            await Task1();
            await Task2();
            stopwatch.Stop();

            var result = stopwatch.ElapsedMilliseconds;
            Console.WriteLine(result);

        }

        [TestMethod]
        public async Task Main1()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var a=Task1();
            var b=Task2();
            await a;
            await b;
            stopwatch.Stop();

            var result = stopwatch.ElapsedMilliseconds;
            Console.WriteLine(result);

        }

       

        public async Task Task1()
        {
            await Task.Delay(2000);
        }
        public async Task Task2()
        {
            await Task.Delay(4000);
        }

        public async Task<int> Task3()
        {
            await Task.Delay(2000);
            return await Task.FromResult(2);
        }



        [TestMethod]
        public async Task Main3()
        {
            var result = await Task3();
            await Consol(result);
        }

        public Task Consol(int begin)
        {
            return Task.Run(() =>
            {
                for (int i = begin; i < 10; i++)
                {
                    Console.WriteLine(i);
                }
            });
        }
    }
}