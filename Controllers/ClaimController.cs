using Microsoft.AspNetCore.Mvc;
using ContractMonthlyClaimSystem.Models;
using System;
using System.Collections.Generic;


namespace ContractMonthlyClaimSystem.Controllers
{
    public class ClaimController : Controller
    {
        // Dummy data for Part 1 prototype (no DB connection yet)
        private static readonly List<Claim> DummyClaims = new List<Claim>
        {
            new Claim { ClaimId = 1, Month = "September 2025", TotalAmount = 4500.00m, Status = "Submitted", SubmittedAt = DateTime.Now.AddDays(-3) },
            new Claim { ClaimId = 2, Month = "August 2025", TotalAmount = 5000.00m, Status = "Approved", SubmittedAt = DateTime.Now.AddMonths(-1) }
        };

        //GET: /Claim/Index
        public IActionResult Index()
        {
            return View(DummyClaims);
        }


        //GET: /Claim/Create
        public IActionResult Create()
        {
            return View();
        }


        //POST: /Claim/Submit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit()
        {
            //simulation only: No DB insert, just show a message
            ViewBag.Message = "Claim submitted successfully!";
            return View("Create");
        }


        //GET: /Claim/Details/
        public IActionResult Details(int id)
        {
            var claim = DummyClaims.Find(c => c.ClaimId == id);
            if (claim == null)
            {
                claim = new Claim
                {
                    ClaimId = id,
                    Month = "N/A",
                    TotalAmount = 0.00m,
                    Status = "N/A",
                    SubmittedAt = DateTime.Now
                };
            }
            return View(claim);
        }


        //POST: /Claim/Approve
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Approve(int id)
        {
            //simulation only: No DB update, just show a message(no real update)
            TempData["Message"] = $"Claim #{id} approved successfully!";
            return RedirectToAction("Details", new { id  });
        }
    }
}
