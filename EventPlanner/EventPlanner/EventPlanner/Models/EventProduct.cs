using System;
using System.Collections.Generic;

namespace EventPlanner.Models;

public partial class EventProduct
{
    public int EventId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
