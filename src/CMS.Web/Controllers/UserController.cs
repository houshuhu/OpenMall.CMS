using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CMS.EnityFrameWork;
using CMS.IApplication;
using CMS.Model.Models.Sys;
using CPy.Domain.BaseRepositories;
using CPy.EntityFramework.Uow;

namespace CMS.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork<CmsDbContext> _unitOfWork;
        private readonly IUserApplication _userApplication;

        public UserController(IRepository<User> userRepository, IUnitOfWork<CmsDbContext> unitOfWork, IUserApplication userApplication)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _userApplication = userApplication;
        }



        // GET: User
        public ActionResult Index()
        {
            //var a = _userRepository.GetAll().Count();
            return View();
        }

        #region Test1
        public JsonResult Get()
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
            return new JsonResult()
            {
                Data = new
                {
                    c,
                    watch.ElapsedMilliseconds,
                    headTask.Result
                }
            };
        }

        public string DoSomeWork()
        {

            var result = new List<string>();
            var taskTop = new Task(() =>
            {
                Thread.Sleep(1000);
                var a = _userRepository.GetAllList();
                result.Add(a[0].UName);
                result.Add(string.Format("top上下文:{0}", _userRepository.GetHashCode().ToString()));
            });
            var taskCenter = new Task(() =>
            {
                Thread.Sleep(2000);
                var a = _userRepository.GetAllList();
                result.Add(a[1].UName);
                result.Add(string.Format("center上下文:{0}", _userRepository.GetHashCode().ToString()));
            });
            var taskBottom = new Task(() =>
            {
                Thread.Sleep(3000);
                var a = _userRepository.GetAllList();
                result.Add(a[2].UName);
                result.Add(string.Format("bottom上下文:{0}", _userRepository.GetHashCode().ToString()));
            });
            taskTop.Start();
            taskCenter.Start();
            taskBottom.Start();
            Task.WaitAll(new Task[] { taskTop, taskCenter, taskBottom });
            return string.Join(";", result);
        }

        public string DoSomeWork1()
        {
            var context = new CmsDbContext();
            var result = new List<string>();
            var taskTop = new Task(() =>
            {
                Thread.Sleep(1000);
                var a = context.Users.ToList();
                result.Add(a[0].UName);
            });
            var taskCenter = new Task(() =>
            {
                Thread.Sleep(2000);
                var a = context.Users.ToList();
                result.Add(a[1].UName);
            });
            var taskBottom = new Task(() =>
            {
                Thread.Sleep(3000);
                var a = context.Users.ToList();
                result.Add(a[2].UName);
            });
            taskTop.Start();
            taskCenter.Start();
            taskBottom.Start();
            Task.WaitAll(new Task[] { taskTop, taskCenter, taskBottom });
            return string.Join(";", result);
        }
        #endregion

        public async Task<JsonResult> Get1()
        {
            var a = Te1();
            var b = Te2();

            var result1=await a;
            var result2 = await b;
            return new JsonResult()
            {
                Data = string.Join(";", result1.Select(t => t.UName)) + "--" + string.Join(";", result2.Select(t => t.UName))
            };
        }

        public async Task<List<User>> Te1()
        {
           return await _userRepository.GetAllListAsync();
        }
        public async Task<List<User>> Te2()
        {
            return await _userRepository.GetAllListAsync();
        }

        public async Task<JsonResult> Insert()
        {
            Stopwatch watch=new Stopwatch();
            watch.Start();
            _unitOfWork.IsTransactional = true;
            var a = _userApplication.Insert();
            var b = _userApplication.Insert1();
            await a;
            await b;
            watch.Stop();
            var c = watch.ElapsedMilliseconds;
            watch.Restart();
            _unitOfWork.Commit();
            watch.Stop();
            return new JsonResult()
            {
                Data = new
                {
                    watch.ElapsedMilliseconds,
                    c
                }
            };
        }


    }
}