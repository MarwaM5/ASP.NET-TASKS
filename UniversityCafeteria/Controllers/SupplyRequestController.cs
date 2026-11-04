
using Microsoft.AspNetCore.Mvc;
using UniversityCafeteria.Models;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace UniversityCafeteria.Controllers
{
    public class SupplyRequestController : Controller
    {
        private const string DraftSessionKey = "SupplyRequestDraft";
        private const string PreferenceCookieKey = "PreferredPriority";

        // Store all confirmed requests in a List
        private static List<SupplyRequest> confirmedRequests
            = new List<SupplyRequest>();

        [HttpGet]
        public IActionResult Index()
        {
            SetPreferenceViewData();

            // Retrieve saved draft from Session
            var savedDraft = HttpContext.Session.GetString(DraftSessionKey);

            if (savedDraft != null)
            {
                var request = JsonSerializer.Deserialize<SupplyRequest>(savedDraft);

                return View(request);
            }

            return View(new SupplyRequest());
        }

        [HttpPost]
        public IActionResult AddItem(SupplyRequest request)
        {
            if (request.Items == null || request.Items.Count == 0)
            {
                ModelState.AddModelError("Items",
                    "You cannot submit an empty supply request.");
            }

            if (!ModelState.IsValid)
            {
                SetPreferenceViewData();
                return View("Index", request);
            }

            // Reset calculation values
            request.Subtotal = 0;
            request.PreparationFee = 0;
            request.Discount = 0;
            request.FinalTotal = 0;

            // Calculate each product cost and preparation fee
            foreach (var item in request.Items)
            {
                item.TotalCost = item.Quantity * item.UnitPrice;

                switch (item.Priority)
                {
                    case "Normal":
                        item.PreparationFee = 2;
                        break;

                    case "Urgent":
                        item.PreparationFee = 5;
                        break;

                    case "Emergency":
                        item.PreparationFee = 10;
                        break;

                    default:
                        item.PreparationFee = 0;
                        break;
                }

                request.Subtotal += item.TotalCost;
                request.PreparationFee += item.PreparationFee;
            }

            // Calculate discount based on subtotal
            if (request.Subtotal > 200)
            {
                request.Discount = request.Subtotal * 0.15m;
            }
            else if (request.Subtotal > 100)
            {
                request.Discount = request.Subtotal * 0.10m;
            }
            else
            {
                request.Discount = 0;
            }

            // Calculate final total
            request.FinalTotal =
                request.Subtotal + request.PreparationFee - request.Discount;

            // Save the complete supply request in Session
            var requestJson = JsonSerializer.Serialize(request);

            HttpContext.Session.SetString(DraftSessionKey, requestJson);

            return RedirectToAction("Review");
        }

        public IActionResult Review()
        {
            // Retrieve saved draft from Session
            var savedDraft = HttpContext.Session.GetString(DraftSessionKey);

            if (savedDraft != null)
            {
                var request = JsonSerializer.Deserialize<SupplyRequest>(savedDraft);

                return View(request);
            }

            return View(new SupplyRequest());
        }

        [HttpPost]
        public IActionResult ConfirmRequest()
        {
            var savedDraft = HttpContext.Session.GetString(DraftSessionKey);

            if (savedDraft == null)
            {
                return RedirectToAction("MyRequests");
            }

            var request = JsonSerializer.Deserialize<SupplyRequest>(savedDraft);

            if (request == null || request.Items == null || request.Items.Count == 0)
            {
                return RedirectToAction("Index");
            }

            request.RequestId = confirmedRequests.Count + 1;
            request.RequestStatus = "Confirmed";

            confirmedRequests.Add(request);

            HttpContext.Session.Remove(DraftSessionKey);

            return RedirectToAction("MyRequests");
        }

        [HttpPost]
        public IActionResult ClearDraft()
        {
            // Remove all saved draft data from Session
            HttpContext.Session.Remove(DraftSessionKey);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SavePreference(string priority)
        {
            if (priority != "Normal" &&
                priority != "Urgent" &&
                priority != "Emergency")
            {
                return RedirectToAction("Index");
            }

            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7)
            };

            Response.Cookies.Append(
                PreferenceCookieKey,
                priority,
                options
            );

            return RedirectToAction("Index");
        }

        public IActionResult MyRequests()
        {
            return View(confirmedRequests);
        }

        public IActionResult About()
        {
            return View();
        }

        private void SetPreferenceViewData()
        {
            string preferredPriority =
                Request.Cookies[PreferenceCookieKey] ?? "Normal";

            ViewBag.PreferredPriority = preferredPriority;

            if (Request.Cookies[PreferenceCookieKey] != null)
            {
                ViewBag.PreferenceMessage =
                    $"Your preferred request priority is {preferredPriority}.";
            }
        }
    }
}
