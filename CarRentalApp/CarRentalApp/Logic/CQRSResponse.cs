using System.Net;

namespace CarRentalApp.Logic
{
    public abstract class CQRSResponse
    {
        public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.OK;
        public string ErrorMessage { get; init; }
    }
}
