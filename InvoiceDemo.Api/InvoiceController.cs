using InvoiceDemo.DbService.Models;
using InvoiceDemo.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceDemo.Api
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InvoiceController : ControllerBase // Fix: Inherit from ControllerBase to access StatusCode method
    {
        private readonly InvoiceService _invoiceService;
        public InvoiceController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost]
        public async Task<ActionResult<Invoice>> CreateUser([FromBody] CreateInvoiceDto request)
        {
            try
            {
                var invoice = await _invoiceService.Create(request);
                //return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
                return invoice;
            }
            //catch (BusinessException ex)
            //{
            //    return BadRequest(new { message = ex.Message });
            //}
            catch (Exception ex)
            {
                // Log the full exception
                return StatusCode(500, new { message = "An error occurred while creating the invoice" });
            }
        }
    }
}
