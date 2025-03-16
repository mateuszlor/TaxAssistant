using System.Linq.Expressions;
using NSubstitute;
using TaxAssistant.JPK.ApplicationLogic.DomainEventHandlers;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Shared.Model.Database.Fa.Enum;
using TaxAssistant.JPK.Shared.Model.Domain;
using TaxAssistant.JPK.Shared.Model.Domain.Company;
using TaxAssistant.JPK.Shared.Model.Domain.Events;
using TaxAssistant.JPK.Shared.Model.Domain.Invoice;

namespace TaxAssistant.JPK.Tests.UnitTests.DDD.EventHandler
{
    public class NewInvoiceFromJpkFaEventHandlerTests
    {
        private IRepository<Company> _companyRepository;
        private IRepository<Invoice> _invoiceRepository;
        private NewInvoiceFromJpkFaEventHandler _sut;
        private InvoiceCompany _company1;
        private InvoiceCompany _company2;
        private InvoiceAmounts _amounts;

        [SetUp]
        public void Setup()
        {
            _companyRepository = Substitute.For<IRepository<Company>>();
            _invoiceRepository = Substitute.For<IRepository<Invoice>>();
            _sut = new NewInvoiceFromJpkFaEventHandler(_invoiceRepository, _companyRepository);

            _company1 = new InvoiceCompany("1234567890", string.Empty, string.Empty);
            _company2 = new InvoiceCompany("1234567899", string.Empty, string.Empty);
            _amounts = new InvoiceAmounts(10000, 0, 0, 0, 10000, 0, 0, 0, null, 0, 0, 2300, null, 0, null);
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
            _companyRepository.AddAsync(Arg.Any<Company>()).Returns(x => Task.FromResult(new Company(Origin.JPK, x.Arg<Company>().TaxIdentificationNumber, string.Empty)));

            var invoiceRow = new Shared.Model.Domain.Events.InvoiceRow("Something", "kg", 1, VatRate.Item23, 100, 123, 100, 123);
            var newEvent = new NewInvoiceFromJpkFaEvent(_company1, _company2, "FV/2020/01", DateTime.Today, DateTime.Today, false, false, _amounts, [invoiceRow]);

            // Act
            await _sut.HandleAsync(newEvent);

            // Assert
            await _companyRepository.Received(2).AddAsync(Arg.Any<Company>());
            await _invoiceRepository.Received(1).AddAsync(Arg.Any<Invoice>());
        }

        [Test]
        public async Task HandleAsync_ForNewInvoiceAndExistingCompany_ShouldAdd()
        {
            // Arrange
            var company = new Company(Origin.JPK, "1234567890", "Some company");
            IList<Company> companyList = new List<Company> { company };
            _companyRepository.GetAllAsync(Arg.Any<Expression<Func<Company, bool>>>()).Returns(Task.FromResult(companyList));
            var newEvent = new NewInvoiceFromJpkFaEvent(_company1, _company2, "FV/2020/01", DateTime.Today, DateTime.Today, false, false, _amounts, []);

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
            var newEvent = new NewInvoiceFromJpkFaEvent(_company1, _company2, "FV/2020/01", DateTime.Today, DateTime.Today, false, false, null, null);

            // Act
            await _sut.HandleAsync(newEvent);

            // Assert
            await _companyRepository.DidNotReceive().AddAsync(Arg.Any<Company>());
            await _invoiceRepository.DidNotReceive().AddAsync(Arg.Any<Invoice>());
        }
    }
}