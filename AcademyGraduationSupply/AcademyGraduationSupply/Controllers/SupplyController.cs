using AcademyGraduationSupply.Data;
using AcademyGraduationSupply.Models;
using Microsoft.AspNetCore.Mvc;

using System.Text.Json;


namespace AcademyGraduationSupply.Controllers
{
    public class SupplyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(SupplyData.Categories);
        }

        [HttpPost]
        public IActionResult Create(int category1, int category2, int category3)
        {

            if (category1 == 0 || category2 == 0 || category3 == 0)
            {
                TempData["Error"] = "Please select one offer from each category.";
                return RedirectToAction("Create");
            }


            //HttpContext.Session.SetInt32("Category1Offer", category1);
            //HttpContext.Session.SetInt32("Category2Offer", category2);
            //HttpContext.Session.SetInt32("Category3Offer", category3);
            List<int> selectedOffers = new List<int>{category1,category2,category3};

            string json = JsonSerializer.Serialize(selectedOffers);

            HttpContext.Session.SetString("SelectedOffers", json);

            return RedirectToAction("Review");
        }


        [HttpGet]
        public IActionResult Review()
        {

            //int? category1 = HttpContext.Session.GetInt32("Category1Offer");
            //int? category2 = HttpContext.Session.GetInt32("Category2Offer");
            //int? category3 = HttpContext.Session.GetInt32("Category3Offer");

            string json = HttpContext.Session.GetString("SelectedOffers");
            if (json == null)
            {
                TempData["Error"] = "Please select your offers first.";
                return RedirectToAction("Create");
            }

            List<int> selectedOffers = JsonSerializer.Deserialize<List<int>>(json);


            //if (category1 == null || category2 == null || category3 == null)
            //{
            //    TempData["Error"] = "Please select your offers first.";
            //    return RedirectToAction("Create");
            //}

            if (selectedOffers == null || selectedOffers.Count != 3)
            {
                TempData["Error"] = "Please select your offers first.";
                return RedirectToAction("Create");
            }
            var selectedCategories = new List<Category>();

            var offerTotals = new Dictionary<int, decimal>();

            decimal totalPrice = 0;

            foreach (var category in SupplyData.Categories)
            {
                //int selectedOffer = 0;

                //if (category.Id == 1)
                //    selectedOffer = category1.Value;
                //else if (category.Id == 2)
                //    selectedOffer = category2.Value;
                //else if (category.Id == 3)
                //    selectedOffer = category3.Value;

                int selectedOffer = selectedOffers[category.Id - 1];

                foreach (var supplier in category.Suppliers)
                {
                    if (supplier.Offer.Id == selectedOffer)
                    {

                        decimal offerTotal = supplier.Offer.Quantity * supplier.Offer.UnitPrice;


                        offerTotals.Add(supplier.Offer.Id, offerTotal);

                        totalPrice += offerTotal;

                        selectedCategories.Add(new Category
                        {
                            Id = category.Id,
                            Name = category.Name,
                            Suppliers = new List<Supplier> { supplier }
                        });
                    }
                }
            }


            decimal discountPercent = 0;

            if (totalPrice > 500)
                discountPercent = 15;
            else if (totalPrice > 300)
                discountPercent = 10;


            decimal discountAmount = totalPrice * discountPercent / 100;
            decimal finalTotal = totalPrice - discountAmount;

            ViewBag.OfferTotals = offerTotals;
            ViewBag.TotalPrice = totalPrice;
            ViewBag.DiscountPercent = discountPercent;
            ViewBag.DiscountAmount = discountAmount;
            ViewBag.FinalTotal = finalTotal;

            return View(selectedCategories);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
