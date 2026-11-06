using System;
using System.Collections.Generic;

namespace UniversityCafeteria.Models;

public partial class PurchaseOrderItem
{
    public int ItemId { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual PurchaseOrder? Order { get; set; }

    public virtual Product? Product { get; set; }
}
