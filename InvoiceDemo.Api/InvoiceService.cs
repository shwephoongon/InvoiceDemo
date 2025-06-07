using AutoMapper;
using InvoiceDemo.DbService.Models;
using InvoiceDemo.Dtos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDemo.Api
{
    public class InvoiceService
    {
        private readonly InvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<InvoiceService> _logger;

        public InvoiceService(
       InvoiceRepository invoiceRepository,
       IMapper mapper,
       ILogger<InvoiceService> logger)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Invoice> Create(CreateInvoiceDto requestDto)
        {
            _logger.LogInformation("Creating Invoice with No: {InvoiceNo}", requestDto.InvoiceNo);

            //// Business validation
            //if (await _productRepository.ExistsAsync(requestDto.Name))
            //{
            //    throw new BusinessValidationException("A product with this name already exists");
            //}

            // Map DTO to domain entity
            //var invoice = _mapper.Map<Invoice>(requestDto);
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
            // invoice.CreatedAt = DateTime.UtcNow;

            // Additional business logic here
            // await ValidateCategory(product.CategoryId);

            // Save to database
          //  var createdInvoice = await _invoiceRepository.Create(invoice);

          //  _logger.LogInformation("Invoice created successfully with Id: {Invoice Id}", createdInvoice.InvoiceId);

            // Map domain entity to response DTO
            //return _mapper.Map<ResponseDto>(createdInvoice);
          //  return createdInvoice;
        }

    }
}
