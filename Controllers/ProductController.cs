using ezapiekanka.JwtService;
using ezapiekanka.Services.ProductService;
using ezapiekanka.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ezapiekanka.Controllers;

[Route("/product")]
[ApiController]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;
    
    public ProductController(IProductService service)
    {
        _service = service;
    }

    [Authorize(Policy = JwtPolicies.User)]
    [HttpPost("/order")]
    public async Task<IActionResult> Order(Guid product) => await _service.Order(Guid.Parse(HttpContext.User.Claims.First(x => x.Type == "id").Value), product);
    
    [Authorize(Policy = JwtPolicies.User)]
    [HttpDelete("/delete")]
    public async Task<IActionResult> CancelOrder(Guid order) => await _service.CancelOrder(order);
    
    [Authorize(Policy = JwtPolicies.User)]
    [HttpGet("/getMyOrder")]
    public async Task<IActionResult> GetMyOrder(Guid order) => await _service.GetMyOrder(order);
    
    [Authorize(Policy = JwtPolicies.Vendor)]
    [HttpPost("/getAllOrder")]
    public async Task<IActionResult> GetAllOrder(Guid order) => await _service.GetAllOrder(order);
}