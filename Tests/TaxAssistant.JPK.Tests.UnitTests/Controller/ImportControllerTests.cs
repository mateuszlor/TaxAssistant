using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework.Internal;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Server.Controllers;
using TaxAssistant.JPK.Shared.Adapter;
using TaxAssistant.JPK.Shared.Model;
using TaxAssistant.JPK.Shared.Model.Database;
using TaxAssistant.JPK.Shared.Model.Database.Ewp;
using TaxAssistant.JPK.Shared.Model.Database.Fa;
using TaxAssistant.JPK.Shared.Model.Database.Kpir;

namespace TaxAssistant.JPK.Tests.UnitTests.Controller
{
    public class ImportControllerTests
    {
        private ImportController _sut;

        private ILogger<ImportController> _logger;
        private KpirAdapter _kpirAdapter;
        private IRepository<Kpir> _kpirRepository;
        private EwpAdapter _ewpAdapter;
        private IRepository<Ewp> _ewpRepository;
        private FaAdapter _faAdapter;
        private IRepository<Fa> _faRepository;
        private IRepository<Import> _importRepository;

        [SetUp]
        public void Setup()
        {
            _logger = Substitute.For<ILogger<ImportController>>();
            _kpirAdapter = Substitute.For<KpirAdapter>();
            _kpirRepository = Substitute.For<IRepository<Kpir>>();
            _ewpAdapter = Substitute.For<EwpAdapter>();
            _ewpRepository = Substitute.For<IRepository<Ewp>>();
            _faAdapter = Substitute.For<FaAdapter>();
            _faRepository = Substitute.For<IRepository<Fa>>();
            _importRepository = Substitute.For<IRepository<Import>>();

            _sut = new ImportController(_logger, _kpirAdapter, _kpirRepository, _ewpAdapter, _ewpRepository, _faAdapter, _faRepository, _importRepository);
        }

        [Test]
        public async Task Import_WithNullContent_ShouldReturn400()
        {
            // Act
            var result = await _sut.Import(null);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().BeOfType<ImportResult>();

            var error = badRequestResult.Value as ImportResult;
            error.Should().NotBeNull();
            error.IsSuccessful.Should().BeFalse();
            error.Data.Should().BeNull();
            error.Error.Should().NotBeNull();
            error.Error.Type.Should().Be("ArgumentException");
            error.Error.Message.Should().Be("Empty JPK file content");

            //_kpirAdapter.DidNotReceiveWithAnyArgs().Adapt(Arg.Any<JPK_PKPIR>());
            //await _kpirRepository.DidNotReceiveWithAnyArgs().AddAsync(Arg.Any<Kpir>());
            //_ewpAdapter.DidNotReceiveWithAnyArgs().Adapt(Arg.Any<JPK_EWP>());
            //await _ewpRepository.DidNotReceiveWithAnyArgs().AddAsync(Arg.Any<Ewp>());
            //_faAdapter.DidNotReceiveWithAnyArgs().Adapt(Arg.Any<JPK_FA>());
            //await _faRepository.DidNotReceiveWithAnyArgs().AddAsync(Arg.Any<Fa>());
            //await _importRepository.DidNotReceiveWithAnyArgs().AddAsync(Arg.Any<Import>());
        }

        [Test]
        public async Task Import_WithEmptyContent_ShouldReturn400()
        {
            // Act
            var result = await _sut.Import(string.Empty);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().BeOfType<ImportResult>();

            var error = badRequestResult.Value as ImportResult;
            error.Should().NotBeNull();
            error.IsSuccessful.Should().BeFalse();
            error.Data.Should().BeNull();
            error.Error.Should().NotBeNull();
            error.Error.Type.Should().Be("ArgumentException");
            error.Error.Message.Should().Be("Empty JPK file content");

            //_kpirAdapter.DidNotReceiveWithAnyArgs().Adapt(Arg.Any<JPK_PKPIR>());
            //await _kpirRepository.DidNotReceiveWithAnyArgs().AddAsync(Arg.Any<Kpir>());
            //_ewpAdapter.DidNotReceiveWithAnyArgs().Adapt(Arg.Any<JPK_EWP>());
            //await _ewpRepository.DidNotReceiveWithAnyArgs().AddAsync(Arg.Any<Ewp>());
            //_faAdapter.DidNotReceiveWithAnyArgs().Adapt(Arg.Any<JPK_FA>());
            //await _faRepository.DidNotReceiveWithAnyArgs().AddAsync(Arg.Any<Fa>());
            //await _importRepository.DidNotReceiveWithAnyArgs().AddAsync(Arg.Any<Import>());
        }

        [Test]
        public async Task Import_WithInvalidXmlContent_ShouldReturn400()
        {
            // Act
            var result = await _sut.Import("invalid content");

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().BeOfType<ImportResult>();

            var error = badRequestResult.Value as ImportResult;
            error.Should().NotBeNull();
            error.IsSuccessful.Should().BeFalse();
            error.Data.Should().BeNull();
            error.Error.Should().NotBeNull();
            error.Error.Type.Should().Be("XmlException");
            error.Error.Message.Should().StartWith("Data at the root level is invalid");

            //_kpirAdapter.DidNotReceiveWithAnyArgs().Adapt(Arg.Any<JPK_PKPIR>());
            //await _kpirRepository.DidNotReceiveWithAnyArgs().AddAsync(Arg.Any<Kpir>());
            //_ewpAdapter.DidNotReceiveWithAnyArgs().Adapt(Arg.Any<JPK_EWP>());
            //await _ewpRepository.DidNotReceiveWithAnyArgs().AddAsync(Arg.Any<Ewp>());
            //_faAdapter.DidNotReceiveWithAnyArgs().Adapt(Arg.Any<JPK_FA>());
            //await _faRepository.DidNotReceiveWithAnyArgs().AddAsync(Arg.Any<Fa>());
            //await _importRepository.DidNotReceiveWithAnyArgs().AddAsync(Arg.Any<Import>());
        }

        [Test]
        public async Task Import_WithNoXmlNamespace_ShouldReturn400()
        {
            // Act
            var result = await _sut.Import("<root />");

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().BeOfType<ImportResult>();

            var error = badRequestResult.Value as ImportResult;
            error.Should().NotBeNull();
            error.IsSuccessful.Should().BeFalse();
            error.Data.Should().BeNull();
            error.Error.Should().NotBeNull();
            error.Error.Type.Should().Be("NotImplementedException");
            error.Error.Message.Should().StartWith("XML has no namespace");

            //_kpirAdapter.DidNotReceiveWithAnyArgs().Adapt(Arg.Any<JPK_PKPIR>());
            //await _kpirRepository.DidNotReceiveWithAnyArgs().AddAsync(Arg.Any<Kpir>());
            //_ewpAdapter.DidNotReceiveWithAnyArgs().Adapt(Arg.Any<JPK_EWP>());
            //await _ewpRepository.DidNotReceiveWithAnyArgs().AddAsync(Arg.Any<Ewp>());
            //_faAdapter.DidNotReceiveWithAnyArgs().Adapt(Arg.Any<JPK_FA>());
            //await _faRepository.DidNotReceiveWithAnyArgs().AddAsync(Arg.Any<Fa>());
            //await _importRepository.DidNotReceiveWithAnyArgs().AddAsync(Arg.Any<Import>());
        }

        [Test]
        public async Task Import_WithUknownXmlNamespace_ShouldReturn400()
        {
            // Act
            var result = await _sut.Import("<root xmlns=\"http://example.org/namespace\" />");

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().BeOfType<ImportResult>();

            var error = badRequestResult.Value as ImportResult;
            error.Should().NotBeNull();
            error.IsSuccessful.Should().BeFalse();
            error.Data.Should().BeNull();
            error.Error.Should().NotBeNull();
            error.Error.Type.Should().Be("NotImplementedException");
            error.Error.Message.Should().StartWith("Namespace \"http://example.org/namespace\" has no handler");

            //_kpirAdapter.DidNotReceiveWithAnyArgs().Adapt(Arg.Any<JPK_PKPIR>());
            //await _kpirRepository.DidNotReceiveWithAnyArgs().AddAsync(Arg.Any<Kpir>());
            //_ewpAdapter.DidNotReceiveWithAnyArgs().Adapt(Arg.Any<JPK_EWP>());
            //await _ewpRepository.DidNotReceiveWithAnyArgs().AddAsync(Arg.Any<Ewp>());
            //_faAdapter.DidNotReceiveWithAnyArgs().Adapt(Arg.Any<JPK_FA>());
            //await _faRepository.DidNotReceiveWithAnyArgs().AddAsync(Arg.Any<Fa>());
            //await _importRepository.DidNotReceiveWithAnyArgs().AddAsync(Arg.Any<Import>());
        }

        [TestCase(ExampleJpkData.V7MVersion1Empty, "JPK_V7M_1")]
        [TestCase(ExampleJpkData.V7MVersion2Empty, "JPK_V7M_2")]
        public async Task Import_WithXmlNamespaceButNoAdapter_ShouldReturn400(string content, string typeName)
        {
            // Act
            var result = await _sut.Import(content);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().BeOfType<ImportResult>();

            var error = badRequestResult.Value as ImportResult;
            error.Should().NotBeNull();
            error.IsSuccessful.Should().BeFalse();
            error.Data.Should().BeNull();
            error.Error.Should().NotBeNull();
            error.Error.Type.Should().Be("NotImplementedException");
            error.Error.Message.Should().Be($"No adapter for {typeName}");
        }

        [Test]
        public async Task Import_WithKpirXmlNamespaceButInvalidContent_ShouldReturn400()
        {
            // Act
            var result = await _sut.Import(ExampleJpkData.KpirEmpty);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().BeOfType<ImportResult>();

            var error = badRequestResult.Value as ImportResult;
            error.Should().NotBeNull();
            error.IsSuccessful.Should().BeFalse();
            error.Data.Should().BeNull();
            error.Error.Should().NotBeNull();
            error.Error.Type.Should().Be("NullReferenceException");
            error.Error.Message.Should().Be("Object reference not set to an instance of an object.");
        }

        [Test]
        public async Task Import_WithEwpXmlNamespaceButInvalidContent_ShouldReturn400()
        {
            // Act
            var result = await _sut.Import(ExampleJpkData.EwpEmpty);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().BeOfType<ImportResult>();

            var error = badRequestResult.Value as ImportResult;
            error.Should().NotBeNull();
            error.IsSuccessful.Should().BeFalse();
            error.Data.Should().BeNull();
            error.Error.Should().NotBeNull();
            error.Error.Type.Should().Be("NullReferenceException");
            error.Error.Message.Should().Be("Object reference not set to an instance of an object.");
        }

        [Test]
        public async Task Import_WithFaXmlNamespaceButInvalidContent_ShouldReturn400()
        {
            // Act
            var result = await _sut.Import(ExampleJpkData.FaEmpty);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().BeOfType<ImportResult>();

            var error = badRequestResult.Value as ImportResult;
            error.Should().NotBeNull();
            error.IsSuccessful.Should().BeFalse();
            error.Data.Should().BeNull();
            error.Error.Should().NotBeNull();
            error.Error.Type.Should().Be("NullReferenceException");
            error.Error.Message.Should().Be("Object reference not set to an instance of an object.");
        }
    }
}
