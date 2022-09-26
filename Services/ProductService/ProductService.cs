using Microsoft.AspNetCore.Mvc;

namespace ezapiekanka.Services.ProductService;

public class ProductService : IProductService
{
    public Task<IActionResult> Order(Guid user, Guid product)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> CancelOrder(Guid order)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> GetMyOrder(Guid user)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> GetAllOrder(Guid user)
    {
        throw new NotImplementedException();
    }
}