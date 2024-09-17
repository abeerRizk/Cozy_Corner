using ITI_Project.DAL.DB.ApplicationDB;
using ITI_Project.DAL.Entites;
using ITI_Project.DAL.Repo.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Repo.Impelemntation
{
    public class InvoiceRepo : IINvoiceRepo
    {
        private readonly ApplicationDbContext db;

        public InvoiceRepo(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }
        public int Create(Invoice invoice)
            {
            try
            {
                 db.Invoices.Add(invoice);
                 db.SaveChanges();
                return invoice.Id;
                
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public async Task< Invoice> GetByInvoiceId(int id)
        {
            var data = await db.Invoices.FirstOrDefaultAsync(a => a.Id == id);
            return data;
        }

        public async Task< Invoice> GetInvoiceByOrderId(int orderId)
        {
           return await db.Invoices.FirstOrDefaultAsync((a => a.OrderId == orderId));
        }






    }
}
