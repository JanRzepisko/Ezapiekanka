using Microsoft.AspNetCore.Mvc;

namespace ezapiekanka.Services.ProductService;

public interface IProductService
{
    public Task<IActionResult> Order(Guid user, Guid product);
    public Task<IActionResult> CancelOrder(Guid order);
    public Task<IActionResult> GetMyOrder(Guid user);
    public Task<IActionResult> GetAllOrder(Guid user);
}