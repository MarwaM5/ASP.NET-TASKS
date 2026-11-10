using System;
using System.Collections.Generic;

namespace HobbyHub_Management_System.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Slug { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? IconKey { get; set; }

    public string TintColor { get; set; } = null!;

    public int SortOrder { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Hobby> Hobbies { get; set; } = new List<Hobby>();
}
