﻿using System;
using System.Collections.Generic;

namespace EventPlanner.Models;

public partial class UserType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
