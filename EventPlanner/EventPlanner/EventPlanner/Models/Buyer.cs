using System;
using System.Collections.Generic;

namespace EventPlanner.Models;

public partial class Buyer
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string ShippingAddress { get; set; } = null!;

    public string ContactPhone { get; set; } = null!;

    public virtual ICollection<Event> Events { get; } = new List<Event>();

    public virtual User User { get; set; } = null!;
}
