using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HobbyHub_CodeFirst1.Models
{
    public class Community
    {
        public int CommunityId { get; set; }

        public int HobbyId { get; set; }

      
        public string Name { get; set; } = null!;

     
        public string Description { get; set; } = null!;

     
        public string Status { get; set; } = null!;

        public Hobby Hobby { get; set; } = null!;
    }
}
