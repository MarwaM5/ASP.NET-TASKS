using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UniversityCafeteria.Controllers
{
    public class SupplyController : Controller
    {
        public IActionResult Index()

        {
            string[] categories = {"Drinks","Food","Snacks","Hot Drinks","Cold Drinks"};
            

            ViewBag.Categories = categories;
            return View();
        }

        public IActionResult Products()
        {

            ViewData["Title"] = "Cafeteria Products";
            ViewData["ManagerName"] = "Ahmad Ali";
            ViewData["NumberOfProducts"] = 5;
            string[] products = { "Water", "Orange Juice", "Coffee", "Sandwich", "Chocolate"};
            ViewData["products"]= products;
            return View();
        }

        public IActionResult Suppliers()
        {
            ViewBag.Title= "Our Suppliers";
            ViewBag.NumberOfSuppliers= 3;
            string[] suppliers = { "Fresh Food Company", "Happy Drinks Company", "Daily Supplies Company" };
            ViewBag.Suppliers = suppliers;



                return View();
        }

        [HttpGet]
        public IActionResult CreateRequest()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SubmitRequest()
        {
            string employeeName = Request.Form["EmployeeName"];
            string productName = Request.Form["ProductName"];
            string quantity = Request.Form["Quantity"];
            string requiredDate = Request.Form["RequiredDate"];

            if (string.IsNullOrWhiteSpace(employeeName))
            {
                ViewBag.Message = "Employee Name is required.";
                ViewBag.MessageType = "error";
                return View();
            }
            else
            {
                //ViewBag.Message = "Request submitted successfully!";
                //ViewBag.MessageType = "success";
                TempData["Message"] = $"Success: Request for {quantity} - {productName} " +
                      $"submitted by {employeeName} (required by {requiredDate}).";

                return RedirectToAction("About");
            }
            
        }


        public IActionResult About()
        {
            return View();
        }


        public IActionResult CheckQuantity(int quantity)
        {
            if (quantity < 20)
            {
                return Content("Small Order");
            }
            else if (quantity >= 20 && quantity <= 50)
            {
                return Content("Medium Order");
            }
            else
            {
                return Content("Large Order");
            }
        }
    }
}
