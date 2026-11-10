using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityCafeteriaNew.Data;
using UniversityCafeteriaNew.Models;

namespace UniversityCafeteriaNew.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SupplierController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var suppliers = _context.Suppliers.Include(p => p.Products).ToList();

            return View(suppliers);
        }

        [HttpGet]
        public IActionResult Create()
            
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult Create(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var supplier = _context.Suppliers.Find(id);

            return View(supplier);
        }

        [HttpPost]
        public IActionResult Edit(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var supplier= _context.Suppliers.Find(id);
            return View(supplier);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
           var supplier = _context.Suppliers.Find(id);
            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
