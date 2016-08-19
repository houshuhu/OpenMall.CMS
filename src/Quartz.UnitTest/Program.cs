using Quartz.UnitTest.Job;

namespace Quartz.UnitTest
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //new SimpleTrigger1().Run();
            new CronTrigger1().Run();
        }
    }
}