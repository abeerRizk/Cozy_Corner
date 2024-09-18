using AutoMapper;
using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Impelemntation;
using ITI_Project.BLL.Services.Interface;
using ITI_Project.DAL.Entites;

using MailKit.Search;

using Microsoft.AspNetCore.Authorization;

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
        private readonly IOrderItemsService orderItemsService;

        public OrderController(IOrderService orderService , UserManager<User> userManager
            , ICustomerService customerService , IMapper mapper,      
          IInvoiceService  invoiceService , IProductService productService
            , IVendorService vendorService , 
          IOrderItemsService orderItemsService)
        {
            _orderService = orderService;
            this.userManager = userManager;
            this.customerService = customerService;
            this.mapper = mapper;
            this.invoiceService = invoiceService;
            this.productService = productService;
            this.vendorService = vendorService;
            this.orderItemsService = orderItemsService;
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
                var items = await orderItemsService.GetAll(order.Id, vendorId);
  
                if(items.Count() != 0)
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
                    new_order.VendorId = vendorId;
                    new_order.Id = order.Id;
                    new_order.Phone_Number = order.Phone_Number;
                    lst.Add(new_order);
                    
                }
            }
            return View(lst);
        }

        [Authorize(Roles = "Vendor,Customer")]
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


        [Authorize(Roles = "Customer")]
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
            return RedirectToAction("Details", "Order");
        }

        [Authorize(Roles = "Customer")]
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


        [Authorize(Roles = "Customer")]
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





        [Authorize(Roles = "Customer")]
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


        public async Task<IActionResult> ViewOrderItems(int OrderId , int vendorId )
        {


            var items = await orderItemsService.GetAll(OrderId , vendorId);
            

            return View(items);
        }


        

         public async Task<IActionResult> DeleteOrder(int orderId , int VendorId)
        {
            var items = await orderItemsService.GetAll(orderId, VendorId);
            foreach(var item in items )
            {
             
                await orderItemsService.Delete(item.Id);
            }
           
            
            return RedirectToAction("Index", "Order");
        }


        public async Task<IActionResult> DeleteOrders( )
        {
            var user = await userManager.GetUserAsync(User);
            var VendorId = await vendorService.GetVendorId_ByUserId(user.Id);

            var temp = await _orderService.GetAllOrders();
            var orders = temp.Where(a => a.Status == "confirmed");
            foreach(var order in orders)
            {
                var items = await orderItemsService.GetAll(order.Id, VendorId);
                foreach (var item in items)
                {
                   
                    await orderItemsService.Delete(item.Id);
                }
            }
            return RedirectToAction("Index", "Order");
        }
        

    }

}
