using System;
using System.Collections.Generic;

namespace InvoiceDemo.DbService.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public string InvoiceNo { get; set; } = null!;

    public DateOnly InvoiceDate { get; set; }

    public int TotalQty { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual ICollection<Invoicedetail> Invoicedetails { get; set; } = new List<Invoicedetail>();
}
