using System;
using System.Collections.Generic;

namespace EPAPI.Models;

public partial class ServiceCategory
{
    public int Id { get; set; }

    public string Category { get; set; } = null!;

    public virtual ICollection<Service> Services { get; } = new List<Service>();
}
