using System.Text.Json.Serialization;
using TaxAssistant.JPK.Shared.Model.Domain.Company.Events;
using TaxAssistant.VatWhiteList.Model;

namespace TaxAssistant.JPK.Shared.Model.Domain.Company
{
    [JsonSerializable(typeof(Company))]
    public class Company : BaseDomainModel
    {
        [JsonConstructor]
        public Company(Origin origin, string? taxIdentificationNumber, string name, string? nationalStatisticNumber = null)
            : base(origin)
        {
            TaxIdentificationNumber = taxIdentificationNumber;
            Name = name;
            NationalStatisticNumber = nationalStatisticNumber;
        }

        public Company(Origin origin, string? taxIdentificationNumber, string name, Address.Address? address, string? nationalStatisticNumber = null)
            : this(origin, taxIdentificationNumber, name, nationalStatisticNumber)
        {
            Address = address;
            AddressId = address?.Id;
        }

        public Company(Origin origin, string? taxIdentificationNumber, string name, Guid addressId, string? nationalStatisticNumber = null)
            : this(origin, taxIdentificationNumber, name, nationalStatisticNumber)
        {
            AddressId = addressId;
        }

        public string? TaxIdentificationNumber { get; set; }

        public string Name { get; set; }

        public string? NationalStatisticNumber { get; set; }

        public string? RegistryNumber { get; set; }

        public DateTime? VatWhiteListSynchronizationDate { get; set; }

        public Guid? AddressId { get; internal set; }

        [JsonIgnore]
        public virtual Address.Address? Address { get; set; }

        public void SynchronizeVatWhiteListData(Entity entity)
        {
            if (TaxIdentificationNumber != entity.Nip)
            {
                throw new InvalidOperationException("Tax identification number does not match");
            }

            ChangeProperty<Company>(x => x.Name, entity.Name);
            ChangeProperty<Company>(x => x.NationalStatisticNumber, entity.Regon);
            ChangeProperty<Company>(x => x.RegistryNumber, entity.Krs);
            ChangeProperty<Company>(x => x.Address, SplitAddress(entity.ResidenceAddress));

            VatWhiteListSynchronizationDate = DateTime.UtcNow;

            Events.Add(new CompanySynchronizedWithVatWhiteListEvent(Id, entity));
        }

        private static Address.Address? SplitAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return null;
            }

            var addressMainParts = address.Split(',');

            if (addressMainParts.Length != 2)
            {
                throw new InvalidOperationException($"Invalid address: '{address}'");
            }

            var addresStreetParts = addressMainParts[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var addresCityParts = addressMainParts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

            return new Address.Address(Origin.VatWhiteList, string.Empty, addresCityParts[0], addresCityParts[1], string.Join(" ", addresStreetParts.Take(addresStreetParts.Length - 1)), addresStreetParts.Last());
        }
    }
}
