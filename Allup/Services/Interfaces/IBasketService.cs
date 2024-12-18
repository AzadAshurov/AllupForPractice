using Allup.ViewModels;

namespace Allup.Services.Interfaces
{
    public interface IBasketService
    {
        Task<List<BasketItemVM>> GetBasketAsync();
    }
}
