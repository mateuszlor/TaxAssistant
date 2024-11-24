using FluentAssertions;
using TaxAssistant.JPK.Shared.Adapter;
using TaxAssistant.JPK.Shared.Model.Database.Fa.Enum;
using TaxAssistant.JPK.Shared.Model.Xml.JPK_FA;

namespace TaxAssistant.JPK.Tests.Adapter
{
    public class FaAdapterTests
    {
        private FaAdapter _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new FaAdapter();
        }

        [Test]
        public void Adapt_ForNullModel_ReturnsNull()
        {
            // Act
            var result = _sut.Adapt(null);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public void Adapt_ForEmptyModel_ReturnsEmptyResult()
        {
            // Act
            var result = _sut.Adapt(new JPK_FA());

            // Assert
            result.Should().BeNull();
		}

		[Test]
		public void Adapt_ForSourcelWithEmptyChildren_ReturnsEmptyResult()
		{
			// Act
			var result = _sut.Adapt(new JPK_FA
			{
				FakturaCtrl = new JPKFakturaCtrl(),
				FakturaWierszCtrl = new JPKFakturaWierszCtrl(),
				Naglowek = new JPKNaglowek(),
				Podmiot1 = new JPKPodmiot1
				{
					IdentyfikatorPodmiotu = new TIdentyfikatorOsobyNiefizycznej1()
				}
			});

			// Assert
			result.Should().NotBeNull();

			result!.ControlData.Should().NotBeNull();
			result!.Header.Should().NotBeNull();
			result!.Invoices.Should().NotBeNull();
			result!.Subject.Should().NotBeNull();
			result!.Orders.Should().NotBeNull();
		}

		[Test]
		public void Adapt_ForValidSource_ReturnsAdaptedModel()
		{
			// Act
			var result = _sut.Adapt(new JPK_FA
			{
				FakturaCtrl = new JPKFakturaCtrl(),
				FakturaWierszCtrl = new JPKFakturaWierszCtrl(),
				Naglowek = new JPKNaglowek(),
				Podmiot1 = new JPKPodmiot1
				{
					IdentyfikatorPodmiotu = new TIdentyfikatorOsobyNiefizycznej1()
				},
				Faktura = [
					new JPKFaktura { P_2A = "Doc1" },
					new JPKFaktura { P_2A = "Doc2" },
				],
				FakturaWiersz = [
					new JPKFakturaWiersz { P_2B = "Doc1" },
					new JPKFakturaWiersz { P_2B = "Doc2" },
					new JPKFakturaWiersz { P_2B = "Doc2" },
					new JPKFakturaWiersz { P_2B = "Doc2" },
				],
				Zamowienie = [
					new JPKZamowienie {
						ZamowienieWiersz = [
							new JPKZamowienieZamowienieWiersz(),
							new JPKZamowienieZamowienieWiersz()
						]
					}
				],
				ZamowienieCtrl = new JPKZamowienieCtrl()
			});

			// Assert
			result.Should().NotBeNull();

			result!.ControlData.Should().NotBeNull();
			result!.Header.Should().NotBeNull();
			result!.Subject.Should().NotBeNull();

			result!.Orders.Should().HaveCount(1);
			var order1 = result!.Orders[0];
			order1.Should().NotBeNull();
			order1!.Rows.Should().HaveCount(2);

			result!.Invoices.Should().HaveCount(2);

			var invoice1 = result!.Invoices[0];
			invoice1!.Should().NotBeNull();
			invoice1!.DocumentNumber.Should().Be("Doc1");
			invoice1!.Rows.Should().HaveCount(1);

			var invoice2 = result!.Invoices[1];
			invoice2!.Should().NotBeNull();
			invoice2!.DocumentNumber.Should().Be("Doc2");
			invoice2!.Rows.Should().HaveCount(3);
		}

		[Test]
		public void Adapt_ForPolishAddress_ReturnsAdaptedModel()
		{
			// Act
			var result = _sut.Adapt(new JPK_FA
			{
				FakturaCtrl = new JPKFakturaCtrl(),
				FakturaWierszCtrl = new JPKFakturaWierszCtrl(),
				Naglowek = new JPKNaglowek(),
				Podmiot1 = new JPKPodmiot1
				{
					IdentyfikatorPodmiotu = new TIdentyfikatorOsobyNiefizycznej1
					{
						PelnaNazwa = "Monsters Inc.",
						ItemElementName = ItemChoiceType.NIP,
						Item = "1234567890"
					},
					Item = new TAdresPolski1
					{
						Gmina = "Pcim",
						KodKraju = TKodKraju.PL,
						KodPocztowy = "01-234",
						Miejscowosc = "Pcim Dolny",
						Powiat = "pcimski",
						NrDomu = "123A",
						NrLokalu = "4 Kl. 2",
						Ulica = "Krótka",
						Wojewodztwo = "Nieistniejąca"
					}
				}
			});

			// Assert
			result.Should().NotBeNull();

			result!.ControlData.Should().NotBeNull();
			result!.Header.Should().NotBeNull();
			result!.Invoices.Should().NotBeNull();
			result!.Orders.Should().NotBeNull();

			result!.Subject.Should().NotBeNull();
			result!.Subject.Name.Should().Be("Monsters Inc.");
			result!.Subject.TaxIdentificationNumber.Should().Be("1234567890");
			result!.Subject.TaxIdentificationNumberType.Should().Be(TaxIdentificationNumberType.NIP);
			result!.Subject.Address.Should().NotBeNull();
			result!.Subject.Address!.Municipality.Should().Be("Pcim");
			result!.Subject.Address!.CountryCode.Should().Be("PL");
			result!.Subject.Address!.PostalCode.Should().Be("01-234");
			result!.Subject.Address!.City.Should().Be("Pcim Dolny");
			result!.Subject.Address!.District.Should().Be("pcimski");
			result!.Subject.Address!.BuildingNumber.Should().Be("123A");
			result!.Subject.Address!.LocalNumber.Should().Be("4 Kl. 2");
			result!.Subject.Address!.Street.Should().Be("Krótka");
			result!.Subject.Address!.Voivodeship.Should().Be("Nieistniejąca");
		}

		[Test]
		public void Adapt_ForForeignAddress_ReturnsAdaptedModel()
		{
			// Act
			var result = _sut.Adapt(new JPK_FA
			{
				FakturaCtrl = new JPKFakturaCtrl(),
				FakturaWierszCtrl = new JPKFakturaWierszCtrl(),
				Naglowek = new JPKNaglowek(),
				Podmiot1 = new JPKPodmiot1
				{
					IdentyfikatorPodmiotu = new TIdentyfikatorOsobyNiefizycznej1
					{
						PelnaNazwa = "Queen LTD",
						ItemElementName = ItemChoiceType.IMPLVATID,
						Item = "GB123456789"
					},
					Item = new TAdresZagraniczny
					{
						KodKraju = TKodKraju.GB,
						KodPocztowy = "SW1A 1AA",
						Miejscowosc = "London",
						NrDomu = "Room No 1",
						NrLokalu = "Second bed to the left",
						Ulica = "Buckingham Palace"
					}
				}
			});

			// Assert
			result.Should().NotBeNull();

			result!.ControlData.Should().NotBeNull();
			result!.Header.Should().NotBeNull();
			result!.Invoices.Should().NotBeNull();
			result!.Orders.Should().NotBeNull();

			result!.Subject.Should().NotBeNull();
			result!.Subject.Name.Should().Be("Queen LTD");
			result!.Subject.TaxIdentificationNumber.Should().Be("GB123456789");
			result!.Subject.TaxIdentificationNumberType.Should().Be(TaxIdentificationNumberType.IMPLVATID);
			result!.Subject.Address.Should().NotBeNull();
			result!.Subject.Address!.CountryCode.Should().Be("GB");
			result!.Subject.Address!.PostalCode.Should().Be("SW1A 1AA");
			result!.Subject.Address!.City.Should().Be("London");
			result!.Subject.Address!.BuildingNumber.Should().Be("Room No 1");
			result!.Subject.Address!.LocalNumber.Should().Be("Second bed to the left");
			result!.Subject.Address!.Street.Should().Be("Buckingham Palace");

			result!.Subject.Address!.Municipality.Should().BeNull();
			result!.Subject.Address!.District.Should().BeNull();
			result!.Subject.Address!.Voivodeship.Should().BeNull();
		}
	}
}
