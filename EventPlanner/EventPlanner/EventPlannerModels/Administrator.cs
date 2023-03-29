using System;
using System.Collections.Generic;

namespace EventPlannerModels;

public partial class Administrator
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual User User { get; set; } = null!;
}
