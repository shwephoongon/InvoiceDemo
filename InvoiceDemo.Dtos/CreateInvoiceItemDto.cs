using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDemo.Dtos
{
    public class CreateInvoiceItemDto
    {
        [Required]
        public string StockCode { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")] 
        public int Qty { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
