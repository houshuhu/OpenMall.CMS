using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CodeFrist.EF.Test.Domain.Context;
using CodeFrist.EF.Test.Domain.Model;
using NUnit.Framework;

namespace CodeFrist.EF.Test.Test
{
    
    public class Class1
    {
        private readonly ICMSDbContext _context;

        public Class1()
        {
            _context = new CMSDbContext();
        }

        [Test]
        public async Task Get()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Task<string> headTask = new Task<string>(DoSomeWork1);
            headTask.Start();
            watch.Stop();
            long c = watch.ElapsedMilliseconds;
            watch.Restart();
            var result = headTask.Result;
            watch.Stop();
            Console.WriteLine("3个任务总共用时{0}毫秒", c);
            Console.WriteLine("结果:{0}", headTask.Result);
            Console.WriteLine("3个任务总共用时{0}毫秒", watch.ElapsedMilliseconds);
            //var a=Te1();
            //var b=Te2();
            //await a;
            //await b;

        }
        public string DoSomeWork1()
        {
            
            var result = new List<string>();
            var taskTop = new Task(() =>
            {
                Thread.Sleep(1000);
                var a = _context.Roles.ToList();
                result.Add(a[0].RName);
            });
            var taskCenter = new Task(() =>
            {
                Thread.Sleep(2000);
                var a = _context.Roles.ToList();
                result.Add(a[1].RName);
            });
            var taskBottom = new Task(() =>
            {
                Thread.Sleep(3000);
                var a = _context.Roles.ToList();
                result.Add(a[2].RName);
            });
            taskTop.Start();
            taskCenter.Start();
            taskBottom.Start();
            Task.WaitAll(new Task[] { taskTop, taskCenter, taskBottom });
            return string.Join(";", result);
        }

        public async Task Te1()
        {
            await _context.Roles.ToListAsync();
        }
        public async Task Te2()
        {
            await _context.Roles.ToListAsync();
        }

        [Test]
        public void Te3()
        {
            var role = new Role()
            {
                Id = Guid.NewGuid(),
                RName = "te3"
            };
            using (var context = new CMSDbContext())
            {
                context.Roles.Add(role);
                context.SaveChanges();
            }
        }
    }
    
}