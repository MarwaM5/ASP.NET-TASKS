using System.ComponentModel.DataAnnotations;

namespace HobbyHub_CodeFirst1.Models
{
    public class Hobby
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

        public Category Category { get; set; } = null!;

        public Community? Community { get; set; }

        public List<IdeaTemplate> IdeaTemplates { get; set; }
    }
}
