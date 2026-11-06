
using System.Collections.Generic;

namespace UniversityCafeteria.Models
{
    public class SupplyRequest
    {
        public int RequestId { get; set; }

        public string RequestStatus { get; set; } = string.Empty;

        public List<SupplyItem> Items { get; set; }
            = new List<SupplyItem>();

        public decimal Subtotal { get; set; }

        public decimal PreparationFee { get; set; }

        public decimal Discount { get; set; }

        public decimal FinalTotal { get; set; }
    }
}
