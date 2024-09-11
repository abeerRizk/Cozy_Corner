using AutoMapper;
using ITI_Project.BLL.Helper;
using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Interface;
using ITI_Project.DAL.Entites;
using ITI_Project.DAL.Repo.Impelemntation;
using ITI_Project.DAL.Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.Services.Impelemntation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo customerRepo;
        private readonly IOrderRepo orderRepo;
        private readonly IMapper mapper;

        public CustomerService(ICustomerRepo customerRepo, IMapper mapper)
        {
            this.customerRepo = customerRepo;
            this.mapper = mapper;
        }

        public (bool sucess, string message) Create(CreateCustomerVM customer)
        {
            try
            {
                Customer new_customer = mapper.Map<Customer>(customer);
                if (customerRepo.IsEmailExist(new_customer.Email) )
                    return (false, "Email is already exist");
                customerRepo.Create(new_customer);
                return (true , string.Empty);
            }
            catch (Exception e)
            {
                return (false , e.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                customerRepo.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<GetCustomerVM> GetAll()
        {
            var result = customerRepo.GetAll().Where(a => a.IsDeleted != true).ToList();
            List<GetCustomerVM> newResult = mapper.Map<List<GetCustomerVM>>(result);
            return newResult;
        }

        public GetCustomerVM GetByCustomerId(int id)
        {
            Customer customer = customerRepo.GetByCustomerId(id);
            return mapper.Map<GetCustomerVM>(customer);
        }

        public bool Update(UpdateCustomerVM customer)
        {
            try
            {
                Customer new_customer = mapper.Map<Customer>(customer);     
                customerRepo.Update(new_customer);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool? HasOrderNow(int CustomerId)
        {
            var customer = customerRepo.GetByCustomerId(CustomerId);
            return customer.hasOrder == true;
        }
        
        public void AddToMyCart(int CustomerId , GetProductVM product)
        {
            var customer = customerRepo.GetByCustomerId(CustomerId);
            Product new_product = mapper.Map<Product>(product);
            if (customer.hasOrder == true)
            {
                orderRepo.AddOrderItem(customer.CurrentOrderId, new_product);
            }
            else
            {
                Order new_order = new Order();
                new_order.CustomerId = CustomerId;
                orderRepo.AddOrderItem(new_order.Id, new_product);
                customer.hasOrder = true;
                customer.CurrentOrderId = new_order.Id;
                customer.Orders.Add(new_order);
               
            }
            customerRepo.SaveChanges();
        }

        public void RemoveFromMyCart(int CustomerId, GetProductVM product)
        {
            var customer = customerRepo.GetByCustomerId(CustomerId);
            Product new_product = mapper.Map<Product>(product);
            if (customer.hasOrder == true)
            {
                orderRepo.RemoveItem(customer.CurrentOrderId, new_product);
            }
            customerRepo.SaveChanges();
        }


        public void MakeCartEmpty(int customerId)
        {
            var customer = customerRepo.GetByCustomerId(customerId);
            customer.hasOrder = false;
            customer.CurrentOrderId = 0;
            customerRepo.SaveChanges();

        }

    }
}
