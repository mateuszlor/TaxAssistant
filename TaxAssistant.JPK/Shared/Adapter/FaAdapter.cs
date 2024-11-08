using TaxAssistant.JPK.Shared.Extensions;
using TaxAssistant.JPK.Shared.Model.Database.Fa;
using TaxAssistant.JPK.Shared.Model.Database.Kpir;
using TaxAssistant.JPK.Shared.Model.Database.Kpir.Enum;
using TaxAssistant.JPK.Shared.Model.Xml.JPK_FA;
using TaxAssistant.JPK.Shared.Model.Xml.JPK_PKPIR;

namespace TaxAssistant.JPK.Shared.Adapter
{
    public class FaAdapter : IJpkAdapter<JPK_FA, Fa>
    {
        public Fa Adapt(JPK_FA item) => new Fa
        {
            Subject = new FaCompany
            {
                Name = item.Podmiot1.IdentyfikatorPodmiotu.PelnaNazwa,
                Address = AdaptAddress(item.Podmiot1.Item)
            },
            ControlData = new FaControlData
            {
                InvoiceRowsCount = int.Parse(item.FakturaWierszCtrl.LiczbaWierszyFaktur),
                TotalIncomeFromRows = item.FakturaWierszCtrl.WartoscWierszyFaktur,
				InvoicesCount = int.Parse(item.FakturaCtrl.LiczbaFaktur),
				TotalIncomeFromInvoices = item.FakturaCtrl.WartoscFaktur,
				OrdersCount = item.ZamowienieCtrl == null
					? 0
					: int.Parse(item.ZamowienieCtrl.LiczbaZamowien),
				TotalIncomeFromOrders = item.ZamowienieCtrl?.WartoscZamowien ?? 0
			},
            Header = new FaHeader
			{
                DateFrom = item.Naglowek.DataOd,
                DateTo = item.Naglowek.DataDo,
				GenerationDate = item.Naglowek.DataWytworzeniaJPK,
				FormCode = item.Naglowek.KodFormularza.Value.ToString(),
                FormVariant = item.Naglowek.WariantFormularza,
                Purpose = (KpirPurpose)item.Naglowek.CelZlozenia,
                TaxOfficeCode = item.Naglowek.KodUrzedu.ToString(),
            },
			Orders = item.Zamowienie?.Select(Adapt).ToList(),
			Invoices = item.Faktura.Select(x => Adapt(x, item.FakturaWiersz)).ToList(),
        };

        private FaCompanyAddress? AdaptAddress(object item) => item switch
        {
            TAdresPolski1 address => AdaptAddress(address),
            TAdresZagraniczny address => AdaptAddress(address),
            _ => null
        };

		private FaOrder Adapt(JPKZamowienie x) => new FaOrder
		{
			InvoiceNumber = x.P_2AZ,
			Amount = x.WartoscZamowienia,
			Rows = x.ZamowienieWiersz.Select(Adapt).ToList()
		};

		private FaOrderRow Adapt(JPKZamowienieZamowienieWiersz x) => new FaOrderRow
		{
			Name = x.P_7Z,
			MetricUnit = x.P_8AZ,
			Count = x.P_8BZSpecified
				? x.P_8BZ 
				: null,
			UnitPriceNet = x.P_9AZSpecified
				? x.P_9AZ
				: null,
			TotalPriceNet = x.P_11NettoZSpecified
				? x.P_11NettoZ
				: null,
			TotalVat = x.P_11VatZSpecified
				? x.P_11VatZ
				: null,
			VatRate = x.P_12ZSpecified
				? Enum.Parse<VatRate>(x.P_12Z.ToString())
				: null,
			VatSpecialRate = x.P_12Z_XIISpecified
				? x.P_12Z_XII
				: null,

		};


		private FaInvoice Adapt(JPKFaktura x, JPKFakturaWiersz[] allRows) => new FaInvoice
		{
			Currency = x.KodWaluty.ToString(),
			Type = Enum.Parse<InvoiceTypeEnum>(x.RodzajFaktury.ToString()),
			CreditNoteReason = x.PrzyczynaKorekty,
			CreditNoteChangedDocumentNumber = x.NrFaKorygowanej,
			CreditNoteChangedDocumentDate = x.OkresFaKorygowanej,
			AdvanceInvoiceNumber = x.NrFaZaliczkowej,
			IssueDate = x.P_1,
			DocumentNumber = x.P_2A,
			Buyer = AdaptCompany(x.P_3A, x.P_3B, x.P_4ASpecified ? x.P_4A.ToString() : null, x.P_5B),
			Seller = AdaptCompany(x.P_3C, x.P_3D, x.P_5ASpecified ? x.P_5A.ToString() : null, x.P_4B),
			DeliveryDate = x.P_6,
			TotalPriceNetBaseRate = x.P_13_1,
			TotalVatBaseRate = x.P_14_1,
			TotalVatBaseRateOtherCurrency = x.P_14_1WSpecified
				? x.P_14_1W
				: null,
			TotalPriceNetRate8 = x.P_13_2,
			TotalVatRate8 = x.P_14_2,
			TotalVatRate8OtherCurrency = x.P_14_2WSpecified
				? x.P_14_2W
				: null,
			TotalPriceNetRate5 = x.P_13_3,
			TotalVatRate5 = x.P_14_3,
			TotalVatRate5OtherCurrency = x.P_14_3WSpecified
				? x.P_14_3W
				: null,
			TotalPriceNetReverseCharge = x.P_13_4,
			TotalVatReverseCharge = x.P_14_4,
			TotalVatReverseChargeOtherCurrency = x.P_14_4WSpecified
				? x.P_14_4W
				: null,
			TotalPriceNetForeignTransaction = x.P_13_5,
			TotalVatForeignTransaction = x.P_14_5,
			TotalPriceNetRate0 = x.P_13_6,
			TotalPriceNetVatExempted = x.P_15,
			CashAccountingScheme = x.P_16,
			IssuedByBuyer = x.P_17,
			ReversedCharge = x.P_18,
			SplitPayment = x.P_18A,
			VatExemption = x.P_19,
			VatSubjectiveExemptionReason = x.P_19
				? x.P_19A
				: null,
			VatObjectiveExemptionReason = x.P_19
				? x.P_19B
				: null,
			VatObjectiveExemptionOtherReason = x.P_19
				? x.P_19C
				: null,
			IssuedByExecutionProcedure = x.P_20,
			ExecutionProcedureIssuer = AdaptCompany(x.P_20A, x.P_20B, null, null),
			IssuedByTaxRepresentative = x.P_21,
			TaxRepresentativeIssuer = AdaptCompany(x.P_21A, x.P_21B, null, x.P_21C),
			IntraCommunitySupplyOfGoodsForNewVechicle = x.P_22,
			IntraCommunitySupplyOfGoodsForNewVechicleDateOfAprovalToUse = x.P_22
				? x.P_22A
				: null,
			IntraCommunitySupplyOfGoodsForNewVechicleMileage = x.P_22
				? x.P_22B
				: null,
			IntraCommunitySupplyOfGoodsForNewVechicleMotohours = x.P_22
				? x.P_22B
				: null,
			IssuedByNextTaxpayer = x.P_23,
			VatMarginSchemeForTourism = x.P_106E_2,
			VatMarginScheme = x.P_106E_3,
			VatMarginSchemeType = x.P_106E_3
				? x.P_106E_3A
				: null,

			Rows = allRows.Where(r => r.P_2B == x.P_2A).Select(Adapt).ToList()
		};

        private FaInvoiceCompany AdaptCompany(string name, string address, string? countryCode, string? taxNumber) => new FaInvoiceCompany
        {
            Name = name,
            Address = address,
            TaxIdentificationNumber = taxNumber,
        };

		private static FaCompanyAddress AdaptAddress(TAdresPolski1 x) => new FaCompanyAddress
		{
			CountryCode = x.KodKraju.ToString(),
			Voivodeship = x.Wojewodztwo,
			City = x.Miejscowosc,
			Street = x.Ulica,
			BuildingNumber = x.NrDomu,
			LocalNumber = x.NrLokalu,
			PostalCode = x.KodPocztowy,
			Municipality = x.Gmina,
			District = x.Powiat
		};
		private static FaCompanyAddress AdaptAddress(TAdresZagraniczny x) => new FaCompanyAddress
		{
			CountryCode = x.KodKraju.ToString(),
			City = x.Miejscowosc,
			Street = x.Ulica,
			BuildingNumber = x.NrDomu,
			LocalNumber = x.NrLokalu,
			PostalCode = x.KodPocztowy,
		};

		private static KpirPhysicalInventory Adapt(JPKPKPIRSpis x) => new KpirPhysicalInventory
        {
            Date = x.P_5A,
            Value = x.P_5B
        };

        private static FaInvoiceRow Adapt(JPKFakturaWiersz x) => new FaInvoiceRow
        {
			DocumentNumber = x.P_2B,
            Name = x.P_7,
            MetricUnit = x.P_8A,
            Count = x.P_8BSpecified
                ? x.P_8B
                : null,
            UnitPriceNet = x.P_9A,
            UnitPriceGross = x.P_9B,
            Discount = x.P_10,
            TotalPriceNet = x.P_11,
            TotalPriceGross = x.P_11ASpecified
                ? x.P_11A 
                : null,
            VatRate = x.P_12Specified
                ? Enum.Parse<VatRate>(x.P_12.ToString())
                : null,
			VatRateSpecial = x.P_12_XIISpecified 
                ? x.P_12_XII
                : null
        };
    }
}
