namespace TaxAsistant.VatWhiteList.Client.Model.Base
{
    public abstract class BaseEntry
    {
        /// <summary>
        /// Przekazany identyfikator (Nip, Regon, Numer rachunku bankowego) 
        /// </summary>
        /// <value>Przekazany identyfikator (Nip, Regon, Numer rachunku bankowego) </value>
        public string Identifier { get; set; }
    }
}
