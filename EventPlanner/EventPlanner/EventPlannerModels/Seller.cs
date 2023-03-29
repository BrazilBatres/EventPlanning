using System;
using System.Collections.Generic;

namespace EventPlannerModels;

public partial class Seller
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string CompanyAddress { get; set; } = null!;

    public string CompanyPhone { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();

    public virtual ICollection<Service> Services { get; } = new List<Service>();

    public virtual User? User { get; set; }
}
