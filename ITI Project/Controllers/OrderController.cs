using AutoMapper;
using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Impelemntation;
using ITI_Project.BLL.Services.Interface;
using ITI_Project.DAL.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITI_Project.Controllers
{
    [Authorize(Roles = "Customer")]
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
            var vendorId = await vendorService.GetVendorId_ByUserId(user.Id);

            var temp = await _orderService.GetAllOrders();
             var orders = temp.Where(a=> a.Status == "confirmed");

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
                    new_order.Items = new List<OrderItemsVM>();
                    foreach (var item in order.Items)
                    {
                        if (item.VendorId == vendorId)
                        {
                            if (item != null)
                            {
                                new_order.Items.Add(item);
                            }
                        }
                    }
                    lst.Add(new_order);
                }
            }
            return View(lst);
        }


        public async Task <IActionResult> Details()
        {
            var user = await userManager.GetUserAsync(User);
            var customerId =   await customerService.GetCustomerId_ByUserId(user.Id);

            var customer = await customerService.GetByCustomerId(customerId);
            var order = new OrderModelVM();
            if (customer.hasOrder == true)
            {
                 order = await _orderService.GetOrderById(customer.CurrentOrderId);
            }


            return View(order);
        }



        public async Task<IActionResult> AddToCart(OrderItemsVM new_order)
        {
            var user = await userManager.GetUserAsync(User);
            var customerId = await customerService.GetCustomerId_ByUserId(user.Id);

            // Calculate the total price of the new order item
            new_order.TotalPrice = new_order.UnitPrice * new_order.Quantity;

            var product = await productService.GetByProductId(new_order.ProductId);
            product.Quantity -= new_order.Quantity;

            UpdateProductVM updateProduct = mapper.Map<UpdateProductVM>(product);
            await productService.Update(updateProduct);

         
             await _orderService.AddOrderItem(customerId, new_order); // Add the order item
            return RedirectToAction("ViewProduct", "Product", new { id = new_order.ProductId });
        }


        public async Task<IActionResult> RemoveOrderFromCart(OrderItemsVM new_order)
        {
            var user = await userManager.GetUserAsync(User);
            var customerId = await customerService.GetCustomerId_ByUserId(user.Id);

            new_order.TotalPrice = new_order.UnitPrice * new_order.Quantity;

            var product = await productService.GetByProductId(new_order.ProductId);

            product.Quantity += new_order.Quantity;

            UpdateProductVM updateProduct = mapper.Map<UpdateProductVM>(product);
            await productService.Update(updateProduct);

            
             await _orderService.RemoveOrderItem(customerId, new_order); // Add the order item
            return RedirectToAction("details", "Order", new { id = new_order.ProductId });
        }



        public async Task<IActionResult> EmptyTheCart()
        {
            var user = await userManager.GetUserAsync(User);
            var customerId = await customerService.GetCustomerId_ByUserId(user.Id);
            var customer = await customerService.GetByCustomerId(customerId); 

            var order = await _orderService.GetOrderById(customer.CurrentOrderId);
            
            foreach (var item in order.Items)
            {
                var product = await productService.GetByProductId(item.ProductId);
                product.Quantity += item.Quantity;
                UpdateProductVM updateProduct = mapper.Map<UpdateProductVM>(product);
                await productService.Update(updateProduct);
            }
          
            customer.hasOrder = false;
            UpdateCustomerVM updateCustomer = mapper.Map<UpdateCustomerVM>(customer);

            await customerService.Update(updateCustomer);
            return RedirectToAction("Details", "Order");
        }






        public async Task<IActionResult>  ConfirmOrder(int orderId,int customerId)
        {

        
            var order = await _orderService.GetOrderById(orderId);
            var customer= await customerService.GetByCustomerId(customerId);
            if (order == null)
            {
                return NotFound("Order not found.");
            }

            order.Status = "confirmed";
            await  _orderService.UpdateOrderStatus(order);

            // update customer status
            customer.hasOrder = false;
            UpdateCustomerVM updateCustomer = mapper.Map<UpdateCustomerVM>(customer);
            await customerService.Update(updateCustomer);

            // create invoice
            CreateInvoiceVM invoiceVM = new CreateInvoiceVM();
            invoiceVM.CustomerId = order.CustomerId;
            invoiceVM.CustomerName = order.CustomerName;
            invoiceVM.TotallPrice = order.TotalPrice;
            invoiceVM.IsPaid = false;
            invoiceVM.OrderId = order.Id;

            invoiceVM.PaymentMethod = order.PaymentMethod;
            invoiceVM.InvoiceDate = DateTime.Now;
            int InvoiceId =  invoiceService.Create(invoiceVM);



            return RedirectToAction("Read", "Invoice"  , new {id =InvoiceId});
        }


    }

}
