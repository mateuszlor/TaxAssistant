using System.Linq.Expressions;
using NSubstitute;
using TaxAssistant.JPK.ApplicationLogic.DomainEventHandlers;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Shared.Model.Domain;
using TaxAssistant.JPK.Shared.Model.Domain.Company;
using TaxAssistant.JPK.Shared.Model.Domain.Events;
using TaxAssistant.JPK.Shared.Model.Domain.Invoice;

namespace TaxAssistant.JPK.Tests.UnitTests.DDD.EventHandler
{
    public class NewInvoiceFromJpkEwpEventHandlerTests
    {
        private IRepository<Company> _companyRepository;
        private IRepository<Invoice> _invoiceRepository;
        private NewInvoiceFromJpkEwpEventHandler _sut;

        [SetUp]
        public void Setup()
        {
            _companyRepository = Substitute.For<IRepository<Company>>();
            _invoiceRepository = Substitute.For<IRepository<Invoice>>();
            _sut = new NewInvoiceFromJpkEwpEventHandler(_invoiceRepository, _companyRepository);
        }

        [Test]
        public async Task HandleAsync_ForNullEvent_ShouldSkip()
        {
            // Act
            await _sut.HandleAsync(null);

            // Assert
            await _companyRepository.DidNotReceive().AddAsync(Arg.Any<Company>());
            await _invoiceRepository.DidNotReceive().AddAsync(Arg.Any<Invoice>());
        }

        [Test]
        public async Task HandleAsync_ForNewInvoiceAndCompany_ShouldAdd()
        {
            // Arrange
            var newEvent = new NewInvoiceFromJpkEwpEvent("1234567890", "FV/2020/01", string.Empty, DateTime.Today, DateTime.Today, 10000, 0, 0, 0, 0, 10000, 0, 0, 0, 0);

            // Act
            await _sut.HandleAsync(newEvent);

            // Assert
            await _companyRepository.Received(1).AddAsync(Arg.Any<Company>());
            await _invoiceRepository.Received(1).AddAsync(Arg.Any<Invoice>());
        }

        [Test]
        public async Task HandleAsync_ForNewInvoiceAndExistingCompany_ShouldAdd()
        {
            // Arrange
            var company = new Company(Origin.JPK, "1234567890", "Some company");
            IList<Company> companyList = new List<Company> { company };
            _companyRepository.GetAllAsync(Arg.Any<Expression<Func<Company, bool>>>()).Returns(Task.FromResult(companyList));
            var newEvent = new NewInvoiceFromJpkEwpEvent("1234567890", "FV/2020/01", string.Empty, DateTime.Today, DateTime.Today, 10000, 0, 0, 0, 0, 10000, 0, 0, 0, 0);

            // Act
            await _sut.HandleAsync(newEvent);

            // Assert
            await _companyRepository.DidNotReceive().AddAsync(Arg.Any<Company>());
            await _invoiceRepository.Received(1).AddAsync(Arg.Any<Invoice>());
        }

        [Test]
        public async Task HandleAsync_ForExistingInvoice_ShouldSkip()
        {
            // Arrange
            _invoiceRepository.AnyAsync(Arg.Any<Expression<Func<Invoice, bool>>>()).Returns(true);
            var newEvent = new NewInvoiceFromJpkEwpEvent("1234567890", "FV/2020/01", string.Empty, DateTime.Today, DateTime.Today, 10000, 0, 0, 0, 0, 10000, 0, 0, 0, 0);

            // Act
            await _sut.HandleAsync(newEvent);

            // Assert
            await _companyRepository.DidNotReceive().AddAsync(Arg.Any<Company>());
            await _invoiceRepository.DidNotReceive().AddAsync(Arg.Any<Invoice>());
        }
    }
}