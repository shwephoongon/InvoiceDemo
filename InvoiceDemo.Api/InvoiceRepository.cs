using InvoiceDemo.DbService.Data;
using InvoiceDemo.DbService.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceDemo.Api
{
    public class InvoiceRepository
    {
        private readonly InvoiceDbContext _context;
        public InvoiceRepository(InvoiceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Invoice>> GetAll()
        {
            return await _context.Invoices.Include(i => i.Invoicedetails).ToListAsync();
        }
        public async Task<Invoice> Create(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }

        public async Task<bool> InvoiceNoExistsAsync(string invoiceNo)
        {
            return await _context.Invoices.AnyAsync(i => i.InvoiceNo == invoiceNo);
        }

        public async Task<bool> StockCodeExistsAsync(string stockcode)
        {
            return await _context.Invoicedetails.AnyAsync(i => i.StockCode == stockcode);
        }

    }
}
