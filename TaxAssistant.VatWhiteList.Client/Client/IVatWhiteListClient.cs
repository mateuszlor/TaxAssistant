using TaxAssistant.VatWhiteList.Model;

namespace TaxAsistant.VatWhiteList.Client.Client
{
    public interface IVatWhiteListClient
    {
        /// <summary>
        ///  Wyszukiwanie podmiotow po numerze konta
        /// </summary>
        /// <param name="bankAccount">Numer rachunku bankowego</param>
        /// <param name="date"></param>
        /// <returns>Task<EntityListResponse></returns>
        Task<EntityListResponse> SearchByBankAccount(string bankAccount, DateTime date);
        /// <summary>
        ///  Wyszukiwanie podmiotow po numerach kont
        /// </summary>
        /// <param name="bankAccounts">Lista maksymalnie 30 numerow rachunkow bankowych rozdzielonych przecinkami </param>
        /// <param name="date"></param>
        /// <returns>Task<EntryListResponse></returns>
        Task<EntryListResponse> SearchByBankAccounts(IList<string> bankAccounts, DateTime date);
        /// <summary>
        ///  Wyszukiwanie pojedynczego podmiotu po nip
        /// </summary>
        /// <param name="nip">Nip</param>
        /// <param name="date"></param>
        /// <returns>Task<EntityResponse></returns>
        Task<EntityResponse> SearchByNip(string nip, DateTime date);
        /// <summary>
        ///  Wyszukiwanie podmiotow po numerach nip
        /// </summary>
        /// <param name="nips">Lista maksymalnie 30 numerow NIP rozdzielonych przecinkami</param>
        /// <param name="date"></param>
        /// <returns>Task<EntryListResponse></returns>
        Task<EntryListResponse> SearchByNips(IList<string> nips, DateTime date);
        /// <summary>
        ///  Wyszukiwanie pojedynczego podmiotu po regon
        /// </summary>
        /// <param name="regon">Regon</param>
        /// <param name="date"></param>
        /// <returns>Task<EntityResponse></returns>
        Task<EntityResponse> SearchByRegon(string regon, DateTime date);
        /// <summary>
        ///  Wyszukiwanie podmiotow po numerach regon
        /// </summary>
        /// <param name="regons">Regon</param>
        /// <param name="date"></param>
        /// <returns>Task<EntryListResponse></returns>
        Task<EntryListResponse> SearchByRegons(IList<string> regons, DateTime date);
        /// <summary>
        ///  Sprawdzenie pojedynczego podmiotu po nip i numerze konta
        /// </summary>
        /// <param name="nip">Nip</param>
        /// <param name="bankAccount">Numer rachunku bankowego</param>
        /// <param name="date"></param>
        /// <returns>EntityCheckResponse</returns>
        Task<EntityCheckResponse> CheckByNip(string nip, string bankAccount, DateTime date);
        /// <summary>
        ///  Sprawdzenie pojedynczego podmiotu po regon i numerze konta
        /// </summary>
        /// <param name="regon">Regon</param>
        /// <param name="bankAccount">Numer rachunku bankowego</param>
        /// <param name="date"></param>
        /// <returns>EntityCheckResponse</returns>
        Task<EntityCheckResponse> CheckByRegon(string regon, string bankAccount, DateTime date);
    }
}
