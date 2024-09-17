using AutoMapper;
using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Interface;
using ITI_Project.DAL.Entites;
using ITI_Project.DAL.Repo.Interface;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.Services.Impelemntation
{
    public class NotificationService: INotificationService
    {

        private readonly ICustomerRepo customerRepo;
        private readonly EmailSender emailSender;
        private readonly IMapper _mapper; 

        private readonly IVendorRepo _vendorRepository;

        public IVendorRepo VendorRepository => _vendorRepository;

        public NotificationService(IVendorRepo vendorRepository,
            ICustomerRepo customerRepo , EmailSender emailSender )
        {
            _vendorRepository = vendorRepository;
           
            this.customerRepo = customerRepo;
            this.emailSender = emailSender;
        }




        public async Task SendNotificationToGoogleEmails()
        {
            
            var customers = await customerRepo.GetAllForGmail();

            string subject = "Notification from Furniture WebSite";
            string message = "<p>New products are added , Check the WebSite</p>";

            foreach (var customer in customers)
            {
                await emailSender.SendEmailAsync(customer.Email, subject, message);
            }
        }

    }

}
