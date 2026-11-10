using System;
using System.Collections.Generic;

namespace HobbyHub_Management_System.Models;

public partial class Community
{
    public int CommunityId { get; set; }

    public int HobbyId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual Hobby Hobby { get; set; } = null!;
}
