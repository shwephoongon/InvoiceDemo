using InvoiceDemo.DbService.Models;
using InvoiceDemo.Dtos;
using Microsoft.EntityFrameworkCore;

namespace InvoiceDemo.Api
{
    public class InvoiceService
    {
        private readonly InvoiceRepository _invoiceRepository;

        public InvoiceService(InvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<List<Invoice>> GetAllInvoices()
        {
            return await _invoiceRepository.GetAll();
        }

        public async Task<Invoice?> GetInvoiceById(int id)
        {
            if (!await _invoiceRepository.InvoiceExistsAsync(id))
            {
                throw new InvalidOperationException("Invoice does not exist");
            }
            return await _invoiceRepository.GetById(id);
        }
        public async Task<Invoice> Create(CreateInvoiceDto requestDto)
        {


            if (await _invoiceRepository.InvoiceNoExistsAsync(requestDto.InvoiceNo))
            {
                throw new InvalidOperationException("Invoice No already exists");
            }

            foreach (var item in requestDto.Items)
            {
                if (await _invoiceRepository.StockCodeExistsAsync(item.StockCode))
                {
                    throw new InvalidOperationException($"Stock Code '{item.StockCode}' already exists");
                }
            }

            var invoice = new Invoice
            {
                InvoiceNo = requestDto.InvoiceNo,
                InvoiceDate = DateOnly.FromDateTime(requestDto.InvoiceDate),
                Invoicedetails = requestDto.Items.Select(i => new Invoicedetail
                {
                    StockCode = i.StockCode,
                    Description = i.Description,
                    Qty = i.Qty,
                    Price = i.Price
                }).ToList(),
                TotalQty = requestDto.Items.Sum(i => i.Qty),
                TotalAmount = requestDto.Items.Sum(i => i.Qty * i.Price)
            };

            return await _invoiceRepository.Create(invoice);

        }

        public async Task<Invoice> Update(CreateInvoiceDto requestDto, int id)
        {
            var existingInvoice = await _invoiceRepository.GetById(id);
            if (existingInvoice == null)
                throw new InvalidOperationException("Invoice does not exist");

            if (await _invoiceRepository.InvoiceNoExistsForOtherAsync(requestDto.InvoiceNo, id))
                throw new InvalidOperationException("Invoice No already exists");

            foreach (var item in requestDto.Items)
            {
                if (await _invoiceRepository.StockCodeExistsForOtherAsync(item.StockCode, id))
                    throw new InvalidOperationException($"Stock Code '{item.StockCode}' already exists");
            }

            existingInvoice.InvoiceNo = requestDto.InvoiceNo;
            existingInvoice.InvoiceDate = DateOnly.FromDateTime(requestDto.InvoiceDate);
            existingInvoice.TotalQty = requestDto.Items.Sum(i => i.Qty);
            existingInvoice.TotalAmount = requestDto.Items.Sum(i => i.Qty * i.Price);

            await _invoiceRepository.Update(existingInvoice, requestDto.Items);

            await _invoiceRepository.SaveChangesAsync();

            return existingInvoice;
        }

    }
}
