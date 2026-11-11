using Microsoft.AspNetCore.Identity;

namespace UniversityCafeteriaSystem_Identity1.Models
{
    public class SupplyRequest
    {
        public int Id { get; set; }

        public DateTime RequestDate { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; }

        public string UserId { get; set; }

        public IdentityUser User { get; set; }

        public List<RequestItem> RequestItems { get; set; }
    }
}
