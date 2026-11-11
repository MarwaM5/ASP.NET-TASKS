using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityCafeteriaSystem_Identity1.Data;
using UniversityCafeteriaSystem_Identity1.Models;

namespace UniversityCafeteriaSystem_Identity1.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = _context.Products.ToList();

            return View(products);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(product);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult Edit(int Id, string Name, string Description, decimal Price, int Quantity)
        {
            var product = _context.Products.Find(Id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = Name;
            product.Description = Description;
            product.Price = Price;
            product.Quantity = Quantity;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);

            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
