using TaxAssistant.JPK.Shared.Model.Xml.JPK_PKPIR;
using TaxAssistant.JPK.Shared.Model.Xml.JPK_V7M_1;
using TaxAssistant.JPK.Shared.Model.Xml.JPK_V7M_2;

namespace TaxAssistant.JPK.Shared.Model
{
    public class ImportResult
    {
        public bool IsSuccessful { get; set; }
        public Error? Error { get; set; }
        public JPK_PKPIR? Kpir { get; set; }
        public JPK_V7M_1? VatV1 { get; set; }
        public JPK_V7M_2? VatV2 { get; set; }
    }
}
