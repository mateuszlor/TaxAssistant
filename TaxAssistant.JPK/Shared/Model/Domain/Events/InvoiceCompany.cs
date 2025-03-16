namespace TaxAssistant.JPK.Shared.Model.Domain.Events
{
    public class InvoiceCompany
    {
        public InvoiceCompany(string? taxIdentificationNumber, string? name, string? address)
        {
            TaxIdentificationNumber = taxIdentificationNumber;
            Name = name;
            Address = address;
        }

        public string? TaxIdentificationNumber { get; }
        public string? Name { get; }
        public string? Address { get; }
    }
}