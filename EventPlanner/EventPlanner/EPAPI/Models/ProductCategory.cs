using System;
using System.Collections.Generic;

namespace EPAPI.Models;

public partial class ProductCategory
{
    public int Id { get; set; }

    public string Category { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
