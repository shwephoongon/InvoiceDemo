using System.ComponentModel.DataAnnotations;

namespace InvoiceDemo.Dtos
{
    public class CreateInvoiceDto
    {
        [Required]
        public string InvoiceNo { get; set; }


        [Required]
        public DateTime InvoiceDate { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one item is required.")]
        public List<CreateInvoiceItemDto> Items { get; set; }
    }
}
