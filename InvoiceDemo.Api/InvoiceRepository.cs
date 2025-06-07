using InvoiceDemo.DbService.Data;
using InvoiceDemo.DbService.Models;
using InvoiceDemo.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDemo.Api
{
    public class InvoiceRepository
    {
        private readonly InvoiceDbContext _context;
        public InvoiceRepository(InvoiceDbContext context)
        {
            _context = context;
        }
        public async Task<Invoice> Create (Invoice invoice)
        {
            _context.Invoices.Add(invoice);
           await _context.SaveChangesAsync();
            return invoice;

        }
    }
}
