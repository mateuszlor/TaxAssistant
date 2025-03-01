using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using TaxAssistant.VatWhiteList.Model.Base;

namespace TaxAssistant.VatWhiteList.Model
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
