using System;
using System.Collections.Generic;

namespace EventPlannerModels;

public partial class Event
{
    public int Id { get; set; }

    public int BuyerId { get; set; }

    public string EventName { get; set; } = null!;

    public DateTime EventDate { get; set; }

    public string EventLocation { get; set; } = null!;

    public string EventDescription { get; set; } = null!;

    public decimal EventBudget { get; set; }

    public virtual Buyer Buyer { get; set; } = null!;

    public virtual ICollection<EventProduct> EventProducts { get; } = new List<EventProduct>();

    public virtual ICollection<Service> Services { get; } = new List<Service>();
}
