using TaxAsistant.VatWhiteList.Client.Model.Base;

namespace TaxAsistant.VatWhiteList.Client.Model
{
    public class EntryError : BaseEntry
    {
        public Error Error { get; set; }
    }
}
