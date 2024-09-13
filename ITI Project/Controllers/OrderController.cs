using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Impelemntation;
using ITI_Project.BLL.Services.Interface;
using ITI_Project.DAL.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITI_Project.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<User> userManager;
        private readonly ICustomerService customerService;

        public OrderController(IOrderService orderService , UserManager<User> userManager , ICustomerService customerService)
        {
            _orderService = orderService;
            this.userManager = userManager;
            this.customerService = customerService;
        }
        public IActionResult Index()
        {
            var orders = _orderService.GetAllOrders();
            return View(orders);
        }
        public IActionResult Details(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound($"Order with id {id} not found."); 
            }
            return View(order);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(OrderModelVM model)
        {
            if (ModelState.IsValid)
            {
                // Do not set model.Id here
                _orderService.AddOrder(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null) return NotFound();
            return View(order);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, OrderModelVM orderViewModel)
        {
            if (ModelState.IsValid)
            {
                _orderService.UpdateOrder(orderViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(orderViewModel);
        }
        public IActionResult Delete(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null) return NotFound();
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _orderService.DeleteOrder(id);
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> AddToCart(OrderItemsVM new_order)
        {
            var user = await userManager.GetUserAsync(User);

            var customerId = customerService.GetCustomerId_ByUserId(user.Id);
            new_order.Status = "ordered";
            _orderService.AddOrderItem(customerId, new_order);
            return RedirectToAction("ViewProduct" , "Product" , new { id = new_order.ProductId });
        }

        
    }

}
