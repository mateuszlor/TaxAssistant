using TaxAssistant.VatWhiteList.Model.Base;

namespace TaxAssistant.VatWhiteList.Model
{
    public class EntryError : BaseEntry
    {
        public Error Error { get; set; }
    }
}
