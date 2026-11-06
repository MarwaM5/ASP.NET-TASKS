using System;
using System.Collections.Generic;

namespace UniversityCafeteria.Models;

public partial class Supplier
{
    public int SuppliersId { get; set; }

    public string SuppliersName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
