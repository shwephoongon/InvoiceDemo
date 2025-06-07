using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDemo.Dtos
{
    public class CreateInvoiceDto
    {
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<CreateInvoiceItemDto> Items { get; set; }
    }
}
