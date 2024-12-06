using System.Net;

namespace TaxAsistant.VatWhiteList.Client.Client
{
    public class ApiException : Exception
    {
        public HttpStatusCode HttpCode { get; private set; }
        public string ErrorCode { get; private set; }
        public string ErrorMessage{ get; private set; }

        public ApiException(HttpStatusCode httpCode, string errorCode, string errorMessage) : base($"(HTTP {(int)httpCode} - {httpCode}){errorCode}: {errorMessage}")
        {
            HttpCode = httpCode;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}
