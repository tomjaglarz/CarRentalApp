namespace CarRentalApp.Logic
{
    public class CQRSQueryResponse<T> : CQRSResponse
    {
        public T QueryResult { get; init; }
    }
}
