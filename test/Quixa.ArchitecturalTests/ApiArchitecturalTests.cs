using System;
using Quixa.Api;
using Quixa.Api.Controllers;
using NetArchTest.Rules;
using Xunit;

namespace Quixa.ArchitecturalTests
{
    public class ApiArchitecturalTests
    {
        private static readonly Types _apiTypes = Types.InAssembly(typeof(Startup).Assembly);

        [Fact]
        public void Controllers_should_inherit_from_ApiControllerBase()
        {
            var result = _apiTypes
                .That()
                .ResideInNamespace("Quixa.Api.Controllers")
                .And()
                .AreNotAbstract()
                .Should()
                .Inherit(typeof(ApiControllerBase))
                .GetResult();

            Assert.True(result.IsSuccessful, $"Failing Types: {string.Join("; ", result.FailingTypeNames ?? Array.Empty<string>())}");
        }

        [Fact]
        public void Controllers_should_have_Controller_suffix()
        {
            var result = _apiTypes
                .That()
                .ResideInNamespace("Quixa.Api.Controllers")
                .And()
                .AreNotAbstract()
                .Should()
                .HaveNameEndingWith("Controller")
                .GetResult();

            Assert.True(result.IsSuccessful, $"Failing Types: {string.Join("; ", result.FailingTypeNames ?? Array.Empty<string>())}");
        }
    }
}
