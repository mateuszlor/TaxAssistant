using TaxAssistant.JPK.Shared.Model.Database;
using TaxAssistant.JPK.Shared.Model.Xml.JPK_EWP;
using TaxAssistant.JPK.Shared.Model.Xml.JPK_PKPIR;
using TaxAssistant.JPK.Shared.Model.Xml.JPK_V7M_1;
using TaxAssistant.JPK.Shared.Model.Xml.JPK_V7M_2;

namespace TaxAssistant.JPK.Shared.Model
{
    [Serializable]
    public class ImportResult
    {
        public bool IsSuccessful { get; set; }
        public Error? Error { get; set; }
        public Import Data { get; set; }
    }
}
