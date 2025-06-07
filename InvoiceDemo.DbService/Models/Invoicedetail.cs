using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InvoiceDemo.DbService.Models;

public partial class Invoicedetail
{
    public int DetailId { get; set; }

    public int InvoiceId { get; set; }

    public int SrNo { get; set; }

    public string StockCode { get; set; } = null!;

    public string? Description { get; set; }

    public int Qty { get; set; }

    public decimal Price { get; set; }

    public decimal Amount { get; set; }

    [JsonIgnore]
    public virtual Invoice Invoice { get; set; } = null!;
}
