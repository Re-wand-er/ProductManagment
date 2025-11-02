namespace ProductManagment.Application.Interfaces
{
    public interface ICurrencyProvider
    {
        Task<decimal?> GetRateAsync();
    }
}
