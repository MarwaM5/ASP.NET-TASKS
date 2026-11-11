using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityCafeteriaSystem_Identity1.Data;
using UniversityCafeteriaSystem_Identity1.Models;

namespace UniversityCafeteriaSystem_Identity1.Controllers
{
    public class SupplyRequestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SupplyRequestController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);

            if (User.IsInRole("Admin") || User.IsInRole("SuperAdmin"))
            {
                var requests = _context.SupplyRequests
                    .Include(r => r.User)
                    .ToList();

                return View(requests);
            }

            var myRequests = _context.SupplyRequests
                .Where(r => r.UserId == userId)
                .ToList();

            return View(myRequests);
        }

 
        public IActionResult Create()
        {
            ViewBag.Products = _context.Products.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(List<int> ProductIds, List<int> Quantities)
        {
            if (ProductIds == null || Quantities == null ||
                ProductIds.Count == 0 || ProductIds.Count != Quantities.Count)
            {
                ModelState.AddModelError("", "Please select at least one product.");
                ViewBag.Products = _context.Products.ToList();
                return View();
            }

            if (Quantities.Any(q => q < 0))
            {
                ModelState.AddModelError("", "Quantity cannot be negative.");
                ViewBag.Products = _context.Products.ToList();
                return View();
            }

            if (!Quantities.Any(q => q > 0))
            {
                ModelState.AddModelError("", "Please select at least one product.");
                ViewBag.Products = _context.Products.ToList();
                return View();
            }

            var request = new SupplyRequest
            {
                RequestDate = DateTime.Now,
                Status = "Pending",
                UserId = _userManager.GetUserId(User),
                RequestItems = new List<RequestItem>()
            };

            decimal totalPrice = 0;

            for (int i = 0; i < ProductIds.Count; i++)
            {
                if (Quantities[i] == 0)
                {
                    continue;
                }

                var product = _context.Products.Find(ProductIds[i]);

                if (product == null)
                {
                    ModelState.AddModelError("", "Selected product was not found.");
                    ViewBag.Products = _context.Products.ToList();
                    return View();
                }

                var item = new RequestItem
                {
                    ProductId = product.Id,
                    Quantity = Quantities[i],
                    Price = product.Price
                };

                request.RequestItems.Add(item);

                totalPrice += product.Price * Quantities[i];
            }

            request.TotalPrice = totalPrice;

            _context.SupplyRequests.Add(request);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

     
        public IActionResult Details(int id)
        {
            var request = _context.SupplyRequests
                .Include(r => r.RequestItems)
                .ThenInclude(item => item.Product)
                .FirstOrDefault(r => r.Id == id);

            if (request == null)
            {
                return NotFound();
            }

            if (!User.IsInRole("Admin") && !User.IsInRole("SuperAdmin"))
            {
                var userId = _userManager.GetUserId(User);

                if (request.UserId != userId)
                {
                    return Forbid();
                }
            }

            return View(request);
        }

 
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult Edit(int id)
        {
            var request = _context.SupplyRequests.Find(id);

            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult Edit(int id, string status)
        {
            if (status != "Pending" &&
                status != "Approved" &&
                status != "Rejected")
            {
                return BadRequest();
            }

            var request = _context.SupplyRequests.Find(id);

            if (request == null)
            {
                return NotFound();
            }

            request.Status = status;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var request = _context.SupplyRequests
                .Include(r => r.RequestItems)
                .FirstOrDefault(r => r.Id == id);

            if (request == null)
            {
                return NotFound();
            }

            if (!User.IsInRole("Admin") && !User.IsInRole("SuperAdmin"))
            {
                var userId = _userManager.GetUserId(User);

                if (request.UserId != userId)
                {
                    return Forbid();
                }
            }

            _context.RequestItems.RemoveRange(request.RequestItems);
            _context.SupplyRequests.Remove(request);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
