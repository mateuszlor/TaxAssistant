using System.Collections.Generic;

namespace TaxAssistant.VatWhiteList.Model
{
    public class EntityList
    {
        /// <summary>
        /// Lista podmiotów 
        /// </summary>
        /// <value>Lista podmiotów </value>
        public List<Entity> Subjects { get; set; }

        public string RequestDateTime { get; set; }

        public string RequestId { get; set; }
    }
}
