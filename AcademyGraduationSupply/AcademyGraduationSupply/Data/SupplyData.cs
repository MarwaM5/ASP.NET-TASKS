using AcademyGraduationSupply.Models;

namespace AcademyGraduationSupply.Data
{
    public static class SupplyData
    {
        public static List<Category> Categories = new() 
        {
            new Category { Id = 1, Name = "Graduation Certificates", Suppliers = new List<Supplier>() // التصنيف 1
            {
                new Supplier { Id = 1, Name = "Supplier A", CategoryId = 1, Offer = new Offer { Id = 1, Name = "Premium Certificate Package", Quantity = 35, UnitPrice = 2m, SupplierId = 1 } },
                new Supplier { Id = 2, Name = "Supplier B", CategoryId = 1, Offer = new Offer { Id = 2, Name = "Standard Certificate Package", Quantity = 35, UnitPrice = 1.5m, SupplierId = 2 } },
                new Supplier { Id = 3, Name = "Supplier C", CategoryId = 1, Offer = new Offer { Id = 3, Name = "Luxury Certificate Package", Quantity = 35, UnitPrice = 2.5m, SupplierId = 3 } }
            }},

            new Category { Id = 2, Name = "Hospitality", Suppliers = new List<Supplier>() // التصنيف 2
            {
                new Supplier { Id = 4, Name = "Supplier D", CategoryId = 2, Offer = new Offer { Id = 4, Name = "Coffee & Drinks Package", Quantity = 100, UnitPrice = 2m, SupplierId = 4 } },
                new Supplier { Id = 5, Name = "Supplier E", CategoryId = 2, Offer = new Offer { Id = 5, Name = "Sandwiches & Snacks Package", Quantity = 100, UnitPrice = 3m, SupplierId = 5 } },
                new Supplier { Id = 6, Name = "Supplier F", CategoryId = 2, Offer = new Offer { Id = 6, Name = "Full Hospitality Package", Quantity = 100, UnitPrice = 4m, SupplierId = 6 } }
            }},

            new Category { Id = 3, Name = "Graduation Gifts", Suppliers = new List<Supplier>() // التصنيف 3
            {
                new Supplier { Id = 7, Name = "Supplier G", CategoryId = 3, Offer = new Offer { Id = 7, Name = "Customized Mugs Package", Quantity = 35, UnitPrice = 2m, SupplierId = 7 } },
                new Supplier { Id = 8, Name = "Supplier H", CategoryId = 3, Offer = new Offer { Id = 8, Name = "Graduation Bags Package", Quantity = 35, UnitPrice = 3m, SupplierId = 8 } },
                new Supplier { Id = 9, Name = "Supplier I", CategoryId = 3, Offer = new Offer { Id = 9, Name = "Premium Gift Boxes Package", Quantity = 35, UnitPrice = 4m, SupplierId = 9 } }
            }}
        };
    }
}