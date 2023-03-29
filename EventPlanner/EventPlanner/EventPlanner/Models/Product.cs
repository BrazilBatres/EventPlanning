using System;
using System.Collections.Generic;

namespace EventPlanner.Models;

public partial class Product
{
    public int Id { get; set; }

    public int SellerId { get; set; }

    public string ProductName { get; set; } = null!;

    public string ProductDescription { get; set; } = null!;

    public decimal ProductPrice { get; set; }

    public int ProductCategoryId { get; set; }

    public virtual ICollection<EventProduct> EventProducts { get; } = new List<EventProduct>();

    public virtual ProductCategory ProductCategory { get; set; } = null!;

    public virtual Seller Seller { get; set; } = null!;
}
