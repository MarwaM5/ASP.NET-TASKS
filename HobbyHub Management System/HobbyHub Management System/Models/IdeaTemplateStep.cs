using System;
using System.Collections.Generic;

namespace HobbyHub_Management_System.Models;

public partial class IdeaTemplateStep
{
    public int TemplateStepId { get; set; }

    public int TemplateId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int SortOrder { get; set; }

    public virtual IdeaTemplate Template { get; set; } = null!;
}
