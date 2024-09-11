using ITI_Project.DAL.DB.ApplicationDB;
using ITI_Project.DAL.Entites;
using ITI_Project.DAL.Repo.Interface;
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
        public bool Create(Invoice invoice)
        {
            try
            {
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = db.Invoices.Where(a => a.Id == id).FirstOrDefault();
                if (data.isDeleted == true)
                {
                    throw new Exception("The Invoice is already deleted");

                }
                data.isDeleted = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Invoice> GetAll()
        {
            var result = db.Invoices.ToList();
            return result;
        }

        public Invoice GetByInvoiceId(int id)
        {
            var data = db.Invoices.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }

        public bool Update(Invoice invoice)
        {
            try
            {
                var data = db.Invoices.Where(a => a.Id == invoice.Id).FirstOrDefault();

                data.OrderId = invoice.OrderId; 
                data.TotallPrice    = invoice.TotallPrice;
                data.TotallPrice = invoice.TotallPrice;
                data.PaymentMethod =  invoice.PaymentMethod;
                data.IsPaid = invoice.IsPaid;
                data.PaymentDate = invoice.PaymentDate;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
