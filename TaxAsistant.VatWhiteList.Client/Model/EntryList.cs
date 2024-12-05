using System.Text.Json.Serialization;
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

        [JsonIgnore]
        public IEnumerable<Entry> FoundEntries => Entries.OfType<Entry>();

        [JsonIgnore]
        public IEnumerable<EntryError> ErrorEntries => Entries.OfType<EntryError>();
    }
}
