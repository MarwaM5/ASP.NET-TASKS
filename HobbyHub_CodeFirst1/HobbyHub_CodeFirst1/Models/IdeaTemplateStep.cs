using System.ComponentModel.DataAnnotations;

namespace HobbyHub_CodeFirst1.Models
{
    public class IdeaTemplateStep
    {
        [Key]
        public int TemplateStepId { get; set; }

        public int TemplateId { get; set; }

     
        public string Title { get; set; } = null!;

      
        public string Description { get; set; } = null!;

        public int SortOrder { get; set; }

        public IdeaTemplate IdeaTemplate { get; set; } = null!;
    }
}
