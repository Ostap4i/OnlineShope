using Microsoft.AspNetCore.Mvc;
using WebApplication3.Model;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;



        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult processOrder()
        {
            _orderService.processOrder("Hello world!");
            return Ok("Order processed");
        }
    }
}
