using Microsoft.Extensions.Options;
using TaxAsistant.VatWhiteList.Client.Configuration;
using TaxAsistant.VatWhiteList.Client.Model;

namespace TaxAsistant.VatWhiteList.Client.Client
{
    public class VatWhiteListClient : ApiClient, IVatWhiteListClient
    {
        public VatWhiteListClient(HttpClient httpClient, IOptionsSnapshot<VatWhiteListConfiguration> options) : base(httpClient, options)
        {
        }

        public async Task<EntityListResponse> SearchByBankAccount(string bankAccount, DateTime date)
        {
            return await GetEntityListAsync($"api/search/bank-account/{bankAccount}", date);
        }

        public async Task<EntryListResponse> SearchByBankAccounts(IList<string> bankAccounts, DateTime date)
        {
            return await GetEntryListAsync($"api/search/bank-account/{ListToString(bankAccounts)}", date);
        }

        public async Task<EntityResponse> SearchByNip(string nip, DateTime date)
        {
            return await GetEntityAsync($"api/search/nip/{nip}", date);
        }

        public async Task<EntryListResponse> SearchByNips(IList<string> nips, DateTime date)
        {
            return await GetEntryListAsync($"api/search/nips/{ListToString(nips)}", date);
        }

        public async Task<EntityResponse> SearchByRegon(string regon, DateTime date)
        {
            return await GetEntityAsync($"api/search/regon/{regon}", date);
        }

        public async Task<EntryListResponse> SearchByRegons(IList<string> regons, DateTime date)
        {
            return await GetEntryListAsync($"api/search/regons/{ListToString(regons)}", date);
        }

        public async Task<EntityCheckResponse> CheckByNip(string nip, string bankAccount, DateTime date)
        {
            return await GetEntityCheckAsync($"api/check/nip/{nip}/bank-account/{bankAccount}", date);
        }

        public async Task<EntityCheckResponse> CheckByRegon(string regon, string bankAccount, DateTime date)
        {
            return await GetEntityCheckAsync($"api/check/regon/{regon}/bank-account/{bankAccount}", date);
        }
    }
}
