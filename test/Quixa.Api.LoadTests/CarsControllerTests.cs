using Xunit;

namespace Quixa.Api.LoadTests
{
    public class CarsControllerTests : LoadTestsBase
    {
        protected override string ResourceUrl => "api/cars";

        [Fact]
        public void GetAll_load_test()
        {
            ExecuteLoadTest(action: "/");
        }
    }
}
