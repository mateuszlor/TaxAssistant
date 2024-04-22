using TaxAssistant.JPK.Shared.Extensions;
using TaxAssistant.JPK.Shared.Model.Database.Ewp;
using TaxAssistant.JPK.Shared.Model.Database.Ewp.Enum;
using TaxAssistant.JPK.Shared.Model.Xml.JPK_EWP;
using TaxAssistant.JPK.Shared.Model.Xml.JPK_PKPIR;

namespace TaxAssistant.JPK.Shared.Adapter
{
    public class EwpAdapter : IJpkAdapter<JPK_EWP, Ewp>
    {
        public Ewp Adapt(JPK_EWP item) => new Ewp
        {
            Subject = new EwpCompany
            {
                TaxIdentificationNumber = item.Podmiot1.IdentyfikatorPodmiotu.NIP,
                Name = item.Podmiot1.IdentyfikatorPodmiotu.PelnaNazwa,
                Address = Adapt(item.Podmiot1.AdresPodmiotu)
            },
            RowsControlData = new EwpControlData
            {
                RowCount = int.Parse(item.EWPCtrl.LiczbaWierszy),
                TotalValue = item.EWPCtrl.SumaPrzychodow
            },
            FixedAssetsControlData = new EwpControlData
            {
                RowCount = int.Parse(item.WykazCtrl.LiczbaWierszy),
                TotalValue = item.WykazCtrl.SumaWartosci
            },
            Header = new EwpHeader
            {
                Currency = item.Naglowek.DomyslnyKodWaluty.ToString(),
                DateFrom = item.Naglowek.DataOd,
                DateTo = item.Naglowek.DataDo,
                FormCode = item.Naglowek.KodFormularza.Value.ToString(),
                FormVariant = item.Naglowek.WariantFormularza,
                Purpose = (EwpPurpose)item.Naglowek.CelZlozenia,
                TaxOfficeCode = item.Naglowek.KodUrzedu.ToString()
            },
            FixedAssets = item.Wykaz?.Select(Adapt)?.ToList(),
            Rows = item.EWPWiersz.Select(Adapt).ToList()
        };

        private static EwpCompanyAddress Adapt(TAdresPolski1 x) => new EwpCompanyAddress
        {
            Voivodeship = x.Wojewodztwo,
            City = x.Miejscowosc,
            Street = x.Ulica,
            BuildingNumber = x.NrDomu,
            LocalNumber = x.NrLokalu,
            PostalCode = x.KodPocztowy,
        };

        private static EwpFixedAsset Adapt(JPKWykaz x) => new EwpFixedAsset
        {
            Number = int.Parse(x.KW_1),
            TransferDate = x.KW_2,
            AcceptanceDate = x.KW_3,
            DocumentNumber = x.KW_4,
            Description = x.KW_5,
            CategoryCode = x.KW_6,
            InitialValue = x.KW_7.NullifyZero(),
            DepreciationRate = x.KW_8.NullifyZero(),
            UpdatedInitialValue = x.KW_9.NullifyZero(),
            //EndDate = x.Items as DateTime?, // TODO: fixed assets
        };

        private static EwpRow Adapt(JPKEWPWiersz x) => new EwpRow
        {
            Number = int.Parse(x.K_1),
            EntryDate = x.K_2,
            RevenueDate = x.K_3,
            DocumentNumber = x.K_4,
            RevenueTaxed17Percent = x.K_5.NullifyZero(),
            RevenueTaxed15Percent = x.K_6.NullifyZero(),
            RevenueTaxed14Percent = x.K_7.NullifyZero(),
            RevenueTaxed12Point5Percent = x.K_8.NullifyZero(),
            RevenueTaxed12Percent = x.K_9.NullifyZero(),
            RevenueTaxed10Percent = x.K_10.NullifyZero(),
            RevenueTaxed8Point5Percent = x.K_11.NullifyZero(),
            RevenueTaxed5Point5Percent = x.K_12.NullifyZero(),
            RevenueTaxed3Percent = x.K_13.NullifyZero(),
            RevenueTotal = x.K_14.NullifyZero(),
            AdditionalDescription = x.K_15
        };
    }
}
