using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CMS.EnityFrameWork;
using CMS.IApplication;
using CMS.Model.Models.Sys;
using CPy.Domain.BaseRepositories;
using CPy.EntityFramework.Uow;
using CPy.ResultDto.ExcuteResult;

namespace CMS.Application
{
    public class UserApplication:IUserApplication
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork<CmsDbContext> _unitOfWork;
        private readonly IRepository<People> _peopleRepository;

        public UserApplication(IUnitOfWork<CmsDbContext> unitOfWork, IRepository<User> userRepository, IRepository<People> peopleRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _peopleRepository = peopleRepository;
        }


        public async Task<WebExcuteResult<long>> Insert()
        {
            var list = new List<User>();
            for (int i = 0; i < 50; i++)
            {
                list.Add(new User()
                {
                    UName = i.ToString()
                });
            }
            Stopwatch watch=new Stopwatch();
            watch.Start();
            await _userRepository.InsertAsync(list);
            Task.Run(async () =>
            {
                await InsertPeople();
            });
            //InsertPeople();
            watch.Stop();
            var c = watch.ElapsedMilliseconds;
            
            

            return new WebExcuteResult<long>(c);
        }

        public WebExcuteResult<EmptyResult> Insert1()
        {
            var list = new List<User>();
            for (int i = 1; i < 10; i++)
            {
                list.Add(new User()
                {
                    UName = i.ToString()
                });
            }
            using (var context = new CmsDbContext())
            {
                context.Users.AddRange(list);
                context.SaveChangesAsync();
            }

            return new WebExcuteResult<EmptyResult>();
        }

        public void Insert2()
        {
            var list = new List<User>();
            for (int i = 1; i < 10; i++)
            {
                list.Add(new User()
                {
                    UName = i.ToString()
                });
            }
            using (var context=new CmsDbContext())
            {
                context.Users.AddRange(list);
                context.SaveChangesAsync();
            }
        }


        public async Task<int> InsertPeople()
        {
            var list = new List<People>();
            for (int i = 1; i < 10; i++)
            {
                list.Add(new People()
                {
                });
            }
            //Thread.Sleep(10000);
            await Task.Delay(10000);
            return await _peopleRepository.InsertAsync(list);
        }

        public void InsertPeopleTongbu()
        {
            var list = new List<People>();
            for (int i = 1; i < 10; i++)
            {
                list.Add(new People()
                {
                });
            }
            Thread.Sleep(10000);
            _peopleRepository.Insert(list);
        }







    }
}