namespace ProductManagment.Application.Interfaces
{
    public interface ICurrencyService
    {
        Task<decimal> ConvertToUsd(decimal value);
    }
}
