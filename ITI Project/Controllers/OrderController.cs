using AutoMapper;
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
        private readonly IMapper mapper;
        private readonly IInvoiceService invoiceService;
        private readonly IProductService productService;
        private readonly IVendorService vendorService;

        public OrderController(IOrderService orderService , UserManager<User> userManager
            , ICustomerService customerService , IMapper mapper,      
          IInvoiceService  invoiceService , IProductService productService
            , IVendorService vendorService)
        {
            _orderService = orderService;
            this.userManager = userManager;
            this.customerService = customerService;
            this.mapper = mapper;
            this.invoiceService = invoiceService;
            this.productService = productService;
            this.vendorService = vendorService;
        }

        public async Task  <IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            var vendorId = vendorService.GetVendorId_ByUserId(user.Id);

            var orders = _orderService.GetAllOrders().Where(a=> a.Status == "confirmed");
            List <OrderModelVM> lst = new List <OrderModelVM>();
            foreach(var order in orders)
            {
                bool exist = false;
                foreach(var item in order.Items)
                {
                    if (item.VendorId == vendorId)
                    {
                        exist = true;
                        break;
                    }
                }
                if(exist)
                {
                   OrderModelVM new_order = new OrderModelVM();
                    new_order.OrderDate = order.OrderDate;
                    new_order.CustomerId = order.CustomerId;
                    new_order.CustomerName = order.CustomerName; 
                    new_order.ShippingAddress = order.ShippingAddress;
                    new_order.PaymentMethod = order.PaymentMethod;
                    new_order.CustomerLocation = order.CustomerLocation;
                    new_order.ExpectedDeliveryDate = order.ExpectedDeliveryDate;
                    new_order.Status = order.Status;
                    
                    foreach (var item in order.Items)
                    {
                        if (item.VendorId == vendorId)
                        {
                            new_order.Items.Add(item);
                        }
                    }
                    lst.Add(new_order);
                }
            }
            return View(lst);
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

            // Calculate the total price of the new order item
            new_order.TotalPrice = new_order.UnitPrice * new_order.Quantity;

            var product = productService.GetByProductId(new_order.ProductId);
            product.Quantity -= new_order.Quantity;

            UpdateProductVM updateProduct = mapper.Map<UpdateProductVM>(product);
            productService.Update(updateProduct);

         
            _orderService.AddOrderItem(customerId, new_order); // Add the order item
            return RedirectToAction("ViewProduct", "Product", new { id = new_order.ProductId });
        }


        public async Task<IActionResult> RemoveOrderFromCart(OrderItemsVM new_order)
        {
            var user = await userManager.GetUserAsync(User);
            var customerId = customerService.GetCustomerId_ByUserId(user.Id);

            // Calculate the total price of the new order item
            new_order.TotalPrice = new_order.UnitPrice * new_order.Quantity;

            var product = productService.GetByProductId(new_order.ProductId);

            product.Quantity += new_order.Quantity;

            UpdateProductVM updateProduct = mapper.Map<UpdateProductVM>(product);
            productService.Update(updateProduct);

            
            _orderService.RemoveOrderItem(customerId, new_order); // Add the order item
            return RedirectToAction("ViewProduct", "Product", new { id = new_order.ProductId });
        }



        public async Task<IActionResult> EmptyTheCart(int id)
        {
            var user = await userManager.GetUserAsync(User);
            var customerId = customerService.GetCustomerId_ByUserId(user.Id);

            var customer = customerService.GetByCustomerId(customerId); 

            var order = _orderService.GetOrderById(id);

            foreach (var item in order.Items)
            {
                var product = productService.GetByProductId(item.ProductId);
                product.Quantity += item.Quantity;
                UpdateProductVM updateProduct = mapper.Map<UpdateProductVM>(product);
                productService.Update(updateProduct);
            }

            customer.hasOrder = false;
            UpdateCustomerVM updateCustomer = mapper.Map<UpdateCustomerVM>(customer);

            customerService.Update(updateCustomer);
            return RedirectToAction("read", "Product");
        }






        public async Task<IActionResult>  ConfirmOrder(int orderId)
        {

            var user = await userManager.GetUserAsync(User);

            var customerId = customerService.GetCustomerId_ByUserId(user.Id);

           var customer = customerService.GetByCustomerId(customerId);
            var order = _orderService.GetOrderById(customer.CurrentOrderId);

            if (order == null)
            {
                return NotFound("Order not found.");
            }

            order.Status = "confirmed";
            _orderService.UpdateOrder(order);


            CreateInvoiceVM invoiceVM = new CreateInvoiceVM();
            invoiceVM.CustomerId = order.CustomerId;
            invoiceVM.CustomerName = order.CustomerName;
            invoiceVM.TotallPrice = order.TotalPrice;
            invoiceVM.IsPaid = false;
            invoiceVM.OrderId = order.Id;
       
            invoiceVM.PaymentMethod = order.PaymentMethod;
            invoiceVM.InvoiceDate = DateTime.Now;
            invoiceService.Create(invoiceVM);

            int InvoiceId = invoiceService.getInvoiceByOrderId(order.Id);

            customer.hasOrder = false;
            UpdateCustomerVM updateCustomer = mapper.Map<UpdateCustomerVM>(customer);

            customerService.Update(updateCustomer);
            return RedirectToAction("Read", "Invoice"  , new {id = InvoiceId});
        }


    }

}
