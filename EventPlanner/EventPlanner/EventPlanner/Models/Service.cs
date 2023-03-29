using System;
using System.Collections.Generic;

namespace EventPlanner.Models;

public partial class Service
{
    public int Id { get; set; }

    public int SellerId { get; set; }

    public string ServiceName { get; set; } = null!;

    public string ServiceDescription { get; set; } = null!;

    public decimal ServicePrice { get; set; }

    public int ServiceCategoryId { get; set; }

    public virtual Seller Seller { get; set; } = null!;

    public virtual ServiceCategory ServiceCategory { get; set; } = null!;

    public virtual ICollection<Event> Events { get; } = new List<Event>();
}
