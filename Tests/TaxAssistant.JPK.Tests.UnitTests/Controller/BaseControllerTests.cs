using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Server.Controllers.Data;
using TaxAssistant.JPK.Shared.Model;
using TaxAssistant.JPK.Shared.Model.Abstraction;

namespace TaxAssistant.JPK.Tests.UnitTests.Controller
{
    public abstract class BaseControllerTests<T>
        where T : BaseModel
    {
        protected IRepository<T> _repository;
        protected Func<BaseController<T>> _getSut;
        protected BaseController<T> _sut;

        protected readonly T _mockedResult;

        protected BaseControllerTests(T mockedResult)
        {
            _mockedResult = mockedResult;
        }

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IRepository<T>>();
            _sut = _getSut();
        }

        [Test]
        public async Task GetAll_ForNullResults_ShouldReturn204()
        {
            // Act
            var result = await _sut.GetAll();

            // Assert
            await _repository.Received(1).GetAllAsync();
            result.Should().BeOfType<NoContentResult>();
        }

        [Test]
        public async Task GetAll_ForNoResults_ShouldReturn204()
        {
            // Arrange
            var list = new List<T>();
            _repository.GetAllAsync().Returns(Task.FromResult<IList<T>>(list));

            // Act
            var result = await _sut.GetAll();

            // Assert
            await _repository.Received(1).GetAllAsync();
            result.Should().BeOfType<NoContentResult>();
        }

        [Test]
        public async Task GetAll_ForFoundResult_ShouldReturn200()
        {
            // Arrange
            var list = new List<T>
            {
                _mockedResult
            };

            _repository.GetAllAsync().Returns(Task.FromResult<IList<T>>(list));

            // Act
            var result = await _sut.GetAll();

            // Assert
            await _repository.Received(1).GetAllAsync();
            result.Should().BeOfType<OkObjectResult>();

            var objectResult = result as OkObjectResult;
            objectResult.Should().NotBeNull();
            objectResult.Value.Should().BeAssignableTo<IList<T>>();

            var resultObject = objectResult.Value as IList<T>;
            resultObject.Should().NotBeNull();
            resultObject.Should().HaveCount(1);
        }

        [Test]
        public async Task GetAll_ForError_ShouldReturn400()
        {
            // Arrange
            _repository.GetAllAsync().Throws(new Exception("some error"));

            // Act
            var result = await _sut.GetAll();

            // Assert
            await _repository.Received(1).GetAllAsync();
            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().BeOfType<Error>();

            var error = badRequestResult.Value as Error;
            error.Should().NotBeNull();
            error.Type.Should().Be("Exception");
            error.Message.Should().Be("some error");
        }

        [Test]
        public async Task Get_ForNotExisting_ShouldReturn204()
        {
            // Arrange
            var guid = Guid.NewGuid();

            // Act
            var result = await _sut.Get(guid);

            // Assert
            await _repository.Received(1).GetAsync(guid);
            result.Should().BeOfType<NoContentResult>();
        }

        [Test]
        public async Task Get_ForFoundResult_ShouldReturn200()
        {
            // Arrange
            _repository.GetAsync(_mockedResult.Id).Returns(Task.FromResult(_mockedResult));

            // Act
            var result = await _sut.Get(_mockedResult.Id);

            // Assert
            await _repository.Received(1).GetAsync(_mockedResult.Id);
            result.Should().BeOfType<OkObjectResult>();

            var objectResult = result as OkObjectResult;
            objectResult.Should().NotBeNull();
            objectResult.Value.Should().BeAssignableTo<T>();

            var resultObject = objectResult.Value as T;
            resultObject.Should().NotBeNull();
            resultObject.Id.Should().Be(_mockedResult.Id);
        }

        [Test]
        public async Task Get_ForError_ShouldReturn400()
        {
            // Arrange
            var guid = Guid.NewGuid();

            _repository.GetAsync(guid).Throws(new Exception("some error"));

            // Act
            var result = await _sut.Get(guid);

            // Assert
            await _repository.Received(1).GetAsync(guid);
            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().BeOfType<Error>();

            var error = badRequestResult.Value as Error;
            error.Should().NotBeNull();
            error.Type.Should().Be("Exception");
            error.Message.Should().Be("some error");
        }

        [Test]
        public async Task Delete_ForNotExisting_ShouldReturn200()
        {
            // Arrange
            var guid = Guid.NewGuid();

            // Act
            var result = await _sut.Delete(guid);

            // Assert
            await _repository.Received(1).DeleteAsync(guid);
            result.Should().BeOfType<OkResult>();
        }

        [Test]
        public async Task Delete_ForError_ShouldReturn400()
        {
            // Arrange
            var guid = Guid.NewGuid();
            _repository.DeleteAsync(guid).Throws(new Exception("some error"));

            // Act
            var result = await _sut.Delete(guid);

            // Assert
            await _repository.Received(1).DeleteAsync(guid);
            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().BeOfType<Error>();

            var error = badRequestResult.Value as Error;
            error.Should().NotBeNull();
            error.Type.Should().Be("Exception");
            error.Message.Should().Be("some error");
        }

        [Test]
        public async Task Post_ForNotExisting_ShouldReturn200()
        {
            // Arrange
            var guid = Guid.NewGuid();

            _repository
                .UpdateAsync(Arg.Any<T>())
                .Returns(Task.FromResult(_mockedResult));

            // Act
            var result = await _sut.Post(_mockedResult);

            // Assert
            await _repository.Received().UpdateAsync(Arg.Any<T>());
            result.Should().BeOfType<OkObjectResult>();

            var objectResult = result as OkObjectResult;
            objectResult.Should().NotBeNull();
            objectResult.Value.Should().BeAssignableTo<T>();

            var objectResultValue = objectResult.Value as T;
            objectResultValue.Should().NotBeNull();
        }

        [Test]
        public async Task Post_ForError_ShouldReturn400()
        {
            // Arrange
            var guid = Guid.NewGuid();
            _repository.UpdateAsync(Arg.Any<T>()).Throws(new Exception("some error"));

            // Act
            var result = await _sut.Post(default);

            // Assert
            await _repository.Received().UpdateAsync(Arg.Any<T>());
            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().BeOfType<Error>();

            var error = badRequestResult.Value as Error;
            error.Should().NotBeNull();
            error.Type.Should().Be("Exception");
            error.Message.Should().Be("some error");
        }
    }
}
