using TaxAsistant.VatWhiteList.Client.Model.Base;

namespace TaxAsistant.VatWhiteList.Client.Model
{
    public class EntryList
    {
        /// <summary>
        /// Lista odpowiedzi
        /// </summary>
        /// <value>Lista odpowiedzi</value>
        public IList<BaseEntry> Entries { get; set; }

        public string RequestDateTime { get; set; }

        public string RequestId { get; set; }

        public IList<Entry> FoundEntries => Entries.OfType<Entry>().ToList();

        public IList<EntryError> ErrorEntries => Entries.OfType<EntryError>().ToList();
    }
}
