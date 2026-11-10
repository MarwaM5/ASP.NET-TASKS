using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityCafeteriaNew.Data;
using UniversityCafeteriaNew.Models;

namespace UniversityCafeteriaNew.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var orders = _context.Orders
                .Include(e => e.Employee)
                .ToList();

            return View(orders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Employees = _context.Employees.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var order = _context.Orders.Find(id);

            ViewBag.Employees = _context.Employees.ToList();

            return View(order);
        }

        [HttpPost]
        public IActionResult Edit(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var order = _context.Orders
                .Include(e => e.Employee)
                .FirstOrDefault(e => e.Id == id);

            return View(order);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var order = _context.Orders.Find(id);

            _context.Orders.Remove(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
