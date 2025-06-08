using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDemo.Dtos
{
    public class ErrorResponseDto
    {
        public string Message { get; set; }

        public ErrorResponseDto(string message)
        {
            Message = message;
        }
    }
}
