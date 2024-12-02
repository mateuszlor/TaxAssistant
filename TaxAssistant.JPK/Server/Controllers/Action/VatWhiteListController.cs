using Microsoft.AspNetCore.Mvc;
using TaxAsistant.VatWhiteList.Client.Client;
using TaxAsistant.VatWhiteList.Client.Model;

namespace TaxAssistant.JPK.Server.Controllers.Action
{
    [Route("api/[controller]/[action]")]
    public class VatWhiteListController : ControllerBase
    {
        private readonly IVatWhiteListClient _client;

        public VatWhiteListController(IVatWhiteListClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        [HttpGet]
        public async Task<IActionResult> SearchByNip(string nip)
        {
            try
            {
                var result = await _client.SearchByNip(nip, DateTime.Today);
                return result.Result.Subject == null
                    ? NoContent()
                    : Ok(result);
            }
            catch (ApiException ex)
            {
                var error = new Error
                {
                    Message = ex.ErrorMessage,
                    Code = ex.ErrorCode
                };

                return StatusCode((int)ex.HttpCode, error);
            }
        }
    }
}
