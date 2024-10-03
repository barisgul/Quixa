using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Quixa.Api.Controllers;
using Quixa.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Quixa.Core.Dtos;
using Xunit;

namespace Quixa.Api.UnitTests.Controllers
{
    public class CarsControllerTests : ControllerTestsBase<CarsController>
    {
        private readonly Mock<ICarService> _carServiceMock;

        public CarsControllerTests()
        {
            _carServiceMock = Mocker.GetMock<ICarService>();
        }

        [Theory, AutoData]
        public async Task GetAll_should_return_Ok_with_expected_result(IEnumerable<CarDto> cars)
        {
            //given
            _carServiceMock.Setup(x => x.GetAllSortedByPlateAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(cars);

            //when
            var result = await Controller.GetAllAsync(default) as OkObjectResult;

            //then
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeAssignableTo<IEnumerable<CarDto>>();
            var value = result.Value as IEnumerable<CarDto>;
            value.Should().HaveCount(cars.Count());

            _carServiceMock.VerifyAll();
        }
    }
}
