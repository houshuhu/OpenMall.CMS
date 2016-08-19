using Microsoft.Practices.Unity;

namespace CodeFrist.EF.Test.Test
{
    public class TestBase
    {
        public UnityContainer Container;
        public TestBase()
        {
            DbContextInitialize.Initialize();
            Container = UnityConfig.GetUnityContainer();
        }
    }
}