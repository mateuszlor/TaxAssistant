using TaxAsistant.VatWhiteList.Client.Model.Base;

namespace TaxAsistant.VatWhiteList.Client.Model
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
