using System.Collections.Generic;
using TaxAssistant.VatWhiteList.Model.Base;

namespace TaxAssistant.VatWhiteList.Model
{
    public class Entry : BaseEntry
    {
        /// <summary>
        /// Lista podmiotów
        /// </summary>
        /// <value>Lista podmiotów</value>
        public List<Entity> Subjects { get; set; }
    }
}
