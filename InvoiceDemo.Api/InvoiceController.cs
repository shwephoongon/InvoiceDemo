using InvoiceDemo.DbService.Models;
using InvoiceDemo.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceDemo.Api
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceService _invoiceService;
        public InvoiceController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Invoice>>> GetAll()
        {
            var invoices = await _invoiceService.GetAllInvoices();
            return Ok(invoices);

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Invoice>> GetById(int id)
        {
            try
            {
                var invoice = await _invoiceService.GetInvoiceById(id);
                if (invoice == null)
                {
                    return NotFound(new ErrorResponseDto("Invoice not found."));
                }
                return Ok(invoice);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ErrorResponseDto(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponseDto("An unexpected error occurred."));
            }
        }

        [HttpPost]
        public async Task<ActionResult<Invoice>> Create([FromBody] CreateInvoiceDto request)
        {
            try
            {
                var invoice = await _invoiceService.Create(request);
                return Ok(invoice);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ErrorResponseDto(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponseDto("An unexpected error occurred."));
            }

        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult<Invoice>> Update(int id, [FromBody] CreateInvoiceDto request)
        {
            try
            {
                var invoice = await _invoiceService.Update(request, id);
                return Ok(invoice);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ErrorResponseDto(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponseDto("An unexpected error occurred."));
            }
        }
    }
}
