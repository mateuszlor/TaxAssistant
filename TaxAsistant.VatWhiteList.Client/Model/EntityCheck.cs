namespace TaxAsistant.VatWhiteList.Client.Model
{
    public class EntityCheck
    {
        /// <summary>
        /// Czy rachunek przypisany do podmiotu czynnego 
        /// </summary>
        /// <value>Czy rachunek przypisany do podmiotu czynnego </value>
        public string AccountAssigned { get; set; }

        public string RequestDateTime { get; set; }

        public string RequestId { get; set; }
    }
}
