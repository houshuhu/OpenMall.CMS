using System.Threading.Tasks;
using CPy.ResultDto.ExcuteResult;

namespace CMS.IApplication
{
    public interface IUserApplication
    {
        Task<WebExcuteResult<long>> Insert();
        WebExcuteResult<EmptyResult> Insert1();

        void Insert2();


    }
}