using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tasks.UnitTest
{
    [TestClass]
    public class AsyncTask
    {
        [TestMethod]
        public async Task Run()
        {
            await Task1();
            await Task2();
        }
        [TestMethod]
        public async Task Run1()
        {
            var a = Task1();
            var b = Task2();
            await a;
            await b;
        }

        private async Task Task1()
        {
            await Task.Delay(4000);
        }
        private async Task Task2()
        {
            await Task.Delay(5000);
        }

    }
}