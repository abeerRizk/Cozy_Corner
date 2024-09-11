using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Entites
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public DateTime InvoiceDate {  get; set; }= DateTime.Now;
        public double? TotallPrice { get; set; }
        public double? NetPrice { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool? IsPaid { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
        public bool? isDeleted { get; set; }




    }
}
