using System.Text.Json.Serialization;

namespace TaxAssistant.VatWhiteList.Model.Base
{
    [JsonDerivedType(typeof(Entry), typeDiscriminator: nameof(Entry))]
    [JsonDerivedType(typeof(EntryError), typeDiscriminator: nameof(EntryError))]
    public abstract class BaseEntry
    {
        /// <summary>
        /// Przekazany identyfikator (Nip, Regon, Numer rachunku bankowego) 
        /// </summary>
        /// <value>Przekazany identyfikator (Nip, Regon, Numer rachunku bankowego) </value>
        public string Identifier { get; set; }
    }
}
