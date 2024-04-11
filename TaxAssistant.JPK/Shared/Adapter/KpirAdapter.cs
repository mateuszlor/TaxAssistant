using TaxAssistant.JPK.Shared.Extensions;
using TaxAssistant.JPK.Shared.Model.Database;
using TaxAssistant.JPK.Shared.Model.Database.Kpir;
using TaxAssistant.JPK.Shared.Model.Database.Kpir.Enum;
using TaxAssistant.JPK.Shared.Model.Xml.JPK_PKPIR;

namespace TaxAssistant.JPK.Shared.Adapter
{
    public class KpirAdapter
    {
        public Kpir Adapt(JPK_PKPIR item) => new Kpir
        {
            Subject = new KpirCompany
            {
                TaxIdentificationNumber = item.Podmiot1.IdentyfikatorPodmiotu.NIP,
                NationalStatisticNumber = item.Podmiot1.IdentyfikatorPodmiotu.REGON,
                Name = item.Podmiot1.IdentyfikatorPodmiotu.PelnaNazwa,
                Address = Adapt(item.Podmiot1.AdresPodmiotu)
            },
            ControlData = new KpirControlData
            {
                RowCount = int.Parse(item.PKPIRCtrl.LiczbaWierszy),
                TotalIncome = item.PKPIRCtrl.SumaPrzychodow
            },
            Header = new KpirHeader
            {
                Currency = item.Naglowek.DomyslnyKodWaluty.ToString(),
                DateFrom = item.Naglowek.DataOd,
                DateTo = item.Naglowek.DataDo,
                FormCode = item.Naglowek.KodFormularza.Value.ToString(),
                FormVariant = item.Naglowek.WariantFormularza,
                Purpose = (KpirPurpose)item.Naglowek.CelZlozenia,
                TaxOfficeCode = item.Naglowek.KodUrzedu.ToString()
            },
            Summary = new KpirSummary
            {
                PhysicalInventoryYearStart = item.PKPIRInfo.P_1,
                PhysicalInventoryYearEnd = item.PKPIRInfo.P_2,
                TotalCost = item.PKPIRInfo.P_3,
                TotalIncome = item.PKPIRInfo.P_4
            },
            PhysicalInventories = item.PKPIRSpis?.Select(Adapt)?.ToList(),
            Rows = item.PKPIRWiersz?.Select(Adapt)?.ToList()
        };

        private KpirCompanyAddress Adapt(TAdresJPK x) => new KpirCompanyAddress
        {
            CountryCode = x.KodKraju.ToString(),
            Voivodeship = x.Wojewodztwo,
            City = x.Miejscowosc,
            Street = x.Ulica,
            BuildingNumber = x.NrDomu,
            LocalNumber = x.NrLokalu,
            PostalCode = x.KodPocztowy,
            PostOffice = x.Poczta,
        };

        private KpirPhysicalInventory Adapt(JPKPKPIRSpis x) => new KpirPhysicalInventory
        {
            Date = x.P_5A,
            Value = x.P_5B
        };

        private KpirRow Adapt(JPKPKPIRWiersz x) => new KpirRow
        {
            Number = int.Parse(x.K_1),
            Date = x.K_2,
            DocumentNumber = x.K_3,
            CompanyData = x.K_4,
            CompanyAddress = x.K_5,
            Description = x.K_6,
            RevenueSold = x.K_7.NullifyZero(),
            RevenueOther = x.K_8.NullifyZero(),
            RevenueTotal = x.K_9.NullifyZero(),
            CostGoods = x.K_10.NullifyZero(),
            CostGoodsRelated = x.K_11.NullifyZero(),
            CostSalaries = x.K_12.NullifyZero(),
            CostOther = x.K_13.NullifyZero(),
            CostTotal = x.K_14.NullifyZero(),
            AdditionalField15 = x.K_15.ToString(),
            ResearchAndDevelopmentDescription = x.K_16A,
            ResearchAndDevelopmentCost = x.K_16B.NullifyZero(),
            AdditionalDescription = x.K_17,
            Type = Enum.Parse<KpirType>(x.typ)
        };
    }
}
