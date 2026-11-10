using System;
using System.Collections.Generic;

namespace HobbyHub_Management_System.Models;

public partial class IdeaTemplate
{
    public int TemplateId { get; set; }

    public int? HobbyId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? ExperienceLevel { get; set; }

    public short? SuggestedMinutes { get; set; }

    public string? PlaceText { get; set; }

    public bool IsActive { get; set; }

    public virtual Hobby? Hobby { get; set; }

    public virtual ICollection<IdeaTemplateStep> IdeaTemplateSteps { get; set; } = new List<IdeaTemplateStep>();
}
