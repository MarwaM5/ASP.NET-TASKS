using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HobbyHub_CodeFirst1.Models
{
    public class Category
    {
        
        public int CategoryId { get; set; }

        public string Slug { get; set; } = null!;


        public string Name { get; set; } = null!;


        public string? IconKey { get; set; }

     
        public string TintColor { get; set; } = null!;

        public int SortOrder { get; set; }

        public bool IsActive { get; set; }

        public List<Hobby> Hobbies { get; set; } 
    }
}
