using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDemo.Dtos
{
    public class CreateInvoiceItemDto
    {
        public string StockCode { get; set; }
        public string? Description { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
