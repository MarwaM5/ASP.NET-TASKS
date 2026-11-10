using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HobbyHub_CodeFirst1.Models
{
    public class IdeaTemplate
    {

        [Key]
        public int TemplateId { get; set; }

        public int? HobbyId { get; set; }

     
        public string Title { get; set; } = null!;

   
        public string Description { get; set; } = null!;

   
        public string? ExperienceLevel { get; set; }

        public short? SuggestedMinutes { get; set; }

     
        public string? PlaceText { get; set; }

        public bool IsActive { get; set; }

        public Hobby? Hobby { get; set; }

        public List<IdeaTemplateStep> IdeaTemplateSteps { get; set; } 
    }
}
