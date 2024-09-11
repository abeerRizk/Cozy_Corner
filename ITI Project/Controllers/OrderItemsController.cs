using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ITI_Project.Controllers
{
    public class OrderItemsController : Controller
    {
        private readonly IOrderItemsService _orderItemsService;

        public OrderItemsController(IOrderItemsService orderItemsService)
        {
            _orderItemsService = orderItemsService;
        }
        public IActionResult Index()
        {
            var items = _orderItemsService.GetAll();
            return View(items);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(OrderItemsVM orderItems)
        {
            if (ModelState.IsValid)
            {
                _orderItemsService.Create(orderItems);
                return RedirectToAction("Index");
            }
            return View(orderItems);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = _orderItemsService.GetById(id);
            if(data == null)
            {
                return NotFound();
            }
            return View(data);  
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteItem(int id)
        {
            _orderItemsService.Delete(id);
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = _orderItemsService.GetById(id);
            if(item==null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost , ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditItem(int id , OrderItemsVM orderItemsVM)
        {
            if (ModelState.IsValid)
            {
                _orderItemsService.Update(orderItemsVM);
                return RedirectToAction(nameof(Index));
            }
            return View(orderItemsVM);
        }

    }
}
