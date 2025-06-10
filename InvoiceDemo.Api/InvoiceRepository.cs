using InvoiceDemo.DbService.Data;
using InvoiceDemo.DbService.Models;
using InvoiceDemo.Dtos;
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
        public async Task<Invoice?> GetById(int id)
        {
            return await _context.Invoices.Include(i => i.Invoicedetails).FirstOrDefaultAsync(i => i.InvoiceId == id);
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

        public async Task<bool> InvoiceNoExistsForOtherAsync(string invoiceNo, int excludedInvoiceId)
        {
            return await _context.Invoices
                .AnyAsync(i => i.InvoiceNo == invoiceNo && i.InvoiceId != excludedInvoiceId);
        }

        public async Task<bool> StockCodeExistsForOtherAsync(string stockcode, int excludedInvoiceId)
        {
            return await _context.Invoicedetails
                .AnyAsync(i => i.StockCode == stockcode && i.InvoiceId != excludedInvoiceId);
        }
        public async Task<bool> StockCodeExistsAsync(string stockcode)
        {
            return await _context.Invoicedetails.AnyAsync(i => i.StockCode == stockcode);
        }
        public async Task<bool> InvoiceExistsAsync(int id)
        {
            return await _context.Invoices.AnyAsync(i => i.InvoiceId == id);
        }

        public async Task<Invoice> Update(Invoice invoice)
        {
            _context.Invoices.Update(invoice);
            return invoice;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task RemoveDetailsAsync(Invoice invoice)
        {
            _context.Invoicedetails.RemoveRange(invoice.Invoicedetails);

        }
    }
}
