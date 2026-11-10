using System;
using System.Collections.Generic;

namespace HobbyHub_Management_System.Models;

public partial class Hobby
{
    public int HobbyId { get; set; }

    public int CategoryId { get; set; }

    public string Slug { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Definition { get; set; } = null!;

    public string StarterStep { get; set; } = null!;

    public string StartingRequirements { get; set; } = null!;

    public string? IconKey { get; set; }

    public bool CanStartAtHome { get; set; }

    public bool NeedsOtherPeople { get; set; }

    public bool IsCompetitive { get; set; }

    public bool NeedsLongFirstSession { get; set; }

    public bool SupportsRegularPractice { get; set; }

    public bool SupportsOccasionalPractice { get; set; }

    public bool SupportsOutingPractice { get; set; }

    public bool IsActive { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Community? Community { get; set; }

    public virtual ICollection<IdeaTemplate> IdeaTemplates { get; set; } = new List<IdeaTemplate>();
}
