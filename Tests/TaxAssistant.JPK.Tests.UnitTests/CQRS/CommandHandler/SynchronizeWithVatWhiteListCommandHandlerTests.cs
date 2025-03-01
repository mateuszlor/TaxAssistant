using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using TaxAsistant.VatWhiteList.Client.Client;
using TaxAssistant.JPK.ApplicationLogic.CommandHandlers;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Shared.Commands;
using TaxAssistant.JPK.Shared.Model.Domain;
using TaxAssistant.JPK.Shared.Model.Domain.Company;
using TaxAssistant.JPK.Shared.Model.Domain.Company.Events;
using TaxAssistant.JPK.Shared.Model.Domain.Events;
using TaxAssistant.VatWhiteList.Model;

namespace TaxAssistant.JPK.Tests.UnitTests.CQRS.CommandHandler
{
    public class SynchronizeWithVatWhiteListCommandHandlerTests
    {
        private IRepository<Company> _repository;
        private IVatWhiteListClient _client;
        private SynchronizeWithVatWhiteListCommandHandler _sut;

        [SetUp]
        public void Setup()
        {
            var logger = Substitute.For<ILogger<SynchronizeWithVatWhiteListCommandHandler>>();
            _repository = Substitute.For<IRepository<Company>>();
            _client = Substitute.For<IVatWhiteListClient>();
            _sut = new SynchronizeWithVatWhiteListCommandHandler(logger, _repository, _client);
        }

        [Test]
        public async Task HandleAsync_ForNullCommand_ShouldThrow()
        {
            // Act && Assert
            await _sut.Invoking(x => x.HandleAsync(null))
                .Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Invalid command");
        }

        [Test]
        public async Task HandleAsync_ForNoCompany_ShouldThrow()
        {
            // Act && Assert
            await _sut.Invoking(x => x.HandleAsync(new SynchronizeWithVatWhiteListCommand(Guid.Empty)))
                .Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("No company");
        }

        [Test]
        public async Task HandleAsync_ForInvalidWhiteListResponse_ShouldThrow()
        {
            // Arrange
            var command = new SynchronizeWithVatWhiteListCommand(Guid.NewGuid());

            var company = new Company(Origin.JPK, "1234567890", "Monsters Inc.");

            _repository
                .GetAsync(command.CompanyId)
                .Returns(Task.FromResult<Company?>(company));

            // Act && Assert
            await _sut.Invoking(x => x.HandleAsync(command))
                .Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Invalid response from VAT WhiteList");
        }

        [Test]
        public async Task HandleAsync_ForNotMatchingNip_ShouldThrow()
        {
            // Arrange
            var command = new SynchronizeWithVatWhiteListCommand(Guid.NewGuid());

            var company = new Company(Origin.JPK, "1234567890", "Monsters Inc.");

            _repository
                .GetAsync(command.CompanyId)
                .Returns(Task.FromResult<Company?>(company));

            _client
                .SearchByNip(company.TaxIdentificationNumber!, DateTime.Today)
                .Returns(Task.FromResult(new EntityResponse
                {
                    Result = new()
                    {
                        Subject = new()
                        {
                            Nip = "1111111111"
                        }
                    }
                }));

            // Act && Assert
            await _sut.Invoking(x => x.HandleAsync(command))
                .Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Tax identification number does not match");
        }

        [Test]
        public async Task HandleAsync_ForSynchronizedToday_ShouldThrow()
        {
            // Arrange
            var command = new SynchronizeWithVatWhiteListCommand(Guid.NewGuid());

            var company = new Company(Origin.JPK, "1234567890", "Monsters Inc.");
            company.VatWhiteListSynchronizationDate = DateTime.Today;

            _repository
                .GetAsync(command.CompanyId)
                .Returns(Task.FromResult<Company?>(company));

            _client
                .SearchByNip(company.TaxIdentificationNumber!, DateTime.Today)
                .Returns(Task.FromResult(new EntityResponse
                {
                    Result = new()
                    {
                        Subject = new()
                        {
                            Nip = "1234567890"
                        }
                    }
                }));

            // Act && Assert
            await _sut.Invoking(x => x.HandleAsync(command))
                .Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Company already synchronized with VAT WhiteList");
        }

        [Test]
        public async Task HandleAsync_ForValidData_ShouldSynchronize()
        {
            // Arrange
            var command = new SynchronizeWithVatWhiteListCommand(Guid.NewGuid());

            var company = new Company(Origin.JPK, "1234567890", "Monsters Inc.");
            Company updatedCompany = null;

            _repository
                .GetAsync(command.CompanyId)
                .Returns(Task.FromResult<Company?>(company));

            await _repository
                .UpdateAsync(Arg.Do<Company>(x => updatedCompany = x));

            _client
                .SearchByNip(company.TaxIdentificationNumber!, DateTime.Today)
                .Returns(Task.FromResult(new EntityResponse
                {
                    Result = new()
                    {
                        Subject = new()
                        {
                            Nip = "1234567890",
                            Name = "New better company"
                        }
                    }
                }));

            // Act
            _ = await _sut.HandleAsync(command);

            updatedCompany.Should().NotBeNull();
            updatedCompany.Name.Should().Be("New better company");

            updatedCompany.Events.Should().HaveCount(2);
            updatedCompany.Events.OfType<CompanySynchronizedWithVatWhiteListEvent>().Should().HaveCount(1);

            var updatedParameterEvents = updatedCompany.Events.OfType<PropertyValueChangedEvent>().ToList();
            updatedParameterEvents.Should().HaveCount(1);

            var updatedParameterEvent = updatedParameterEvents.Single();
            updatedParameterEvent.ItemId.Should().Be(company.Id);
            updatedParameterEvent.ItemType.Should().Be("TaxAssistant.JPK.Shared.Model.Domain.Company.Company");
            updatedParameterEvent.PropertyName.Should().Be("Name");
            updatedParameterEvent.OldValue.Should().Be("Monsters Inc.");
            updatedParameterEvent.NewValue.Should().Be("New better company");
        }
    }
}