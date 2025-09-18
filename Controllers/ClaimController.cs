using Microsoft.AspNetCore.Mvc;
using ContractMonthlyClaimSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ContractMonthlyClaimSystem.Controllers
{
    public class ClaimController : Controller
    {
        // Dummy data for Part 1 prototype (no DB connection yet)
        private static readonly List<Lecturer> DummyLecturers = new List<Lecturer>
        { 
            new Lecturer {LecturerId = 101, FullName = "Dr. Tumi N.", Email = "tumi@gmail.com", HourlyRate = 500 },
            new Lecturer {LecturerId = 102, FullName = "Mrs. Kgosi S.", Email = "kgosi@yahoo.com", HourlyRate = 400 }
        };


        private static readonly List<User> DummyUsers = new List<User>
        { 
            new User {UserId = 1, FullName = "Kholo Nkosi", Role = "Programme Coordinator" },
            new User {UserId = 2, FullName = "Siya Sepuru", Role = "Academic Manager" }
        };


        private static readonly List<Claim> DummyClaims;
        static ClaimController()
        {
            //create dummy claims with lines and documents
            DummyClaims = new List<Claim>
            {
                new Claim
                {
                    ClaimId = 1,
                    LecturerId = 101,
                    Month = "September 2025",
                    TotalHours = 16,
                    TotalAmount = 16 * 500,
                    Status = "Submitted",
                    SubmittedAt = DateTime.Now.AddDays(-3),
                    ClaimLines = new List<ClaimLine>
                    {
                        new ClaimLine { ClaimLineId=1, ClaimId=1, Description="Lecture: Module C108", HoursWorked=8, RatePerHour=500, Subtotal=4000},
                        new ClaimLine { ClaimLineId=2, ClaimId=2, Description="Preparation", HoursWorked=4, RatePerHour=500, Subtotal=2000 }
                    },
                    Documents = new List<SupportingDocument>
                    {
                        new SupportingDocument { DocumentId=1, ClaimId=1, FileName="attendance_Sept2025.pdf", FilePath="", UploadedAt=DateTime.Now.AddDays(-3) }
                    }
                },
                new Claim
                {
                    ClaimId = 2,
                    LecturerId = 102,
                    Month = "July 2025",
                    TotalHours = 6,
                    TotalAmount = 6 * 400,
                    Status = "Approved",
                    SubmittedAt = DateTime.Now.AddMonths(-1),
                    ApprovedBy = 2,
                    ApprovedAt = DateTime.Now.AddDays(-20),
                    ClaimLines = new List<ClaimLine>
                    {
                        new ClaimLine { ClaimLineId=3, ClaimId=2, Description="Workshop", HoursWorked=10, RatePerHour=400, Subtotal=4000 }
                    },
                    Documents = new List<SupportingDocument>()
                }  
            };
        }

        //lecturer view
        //GET: /Claim/LectuerDashboard?lecturerId101
        public IActionResult LecturerDashboard(int lectrurerId = 101)
        {
            var lecturer = DummyLecturers.FirstOrDefault(l => l.LecturerId == lectrurerId);
            if (lecturer == null) return NotFound();

            var claims = DummyClaims.Where(c => c.LecturerId == lectrurerId).OrderByDescending(c => c.SubmittedAt).ToList();
            ViewBag.Lecturer = lecturer;
            return View(claims);
        }


        //GET: /Claim/Create (shows UI only)
        public IActionResult Create(int lecturerId = 101)
        {
            ViewBag.LecturerId = lecturerId;
            ViewBag.Message = TempData["Message"];
            return View();
        }


        //POST: /Claim/Submit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitSimulation(int lecturerId)
        {
            //simulation only: No DB insert, just show a message
            TempData["Message"] = "Claim submitted successfully!";
            return RedirectToAction("Create", new { lecturerId });
        }


        //GET: /Claim/Details/
        public IActionResult Details(int id)
        {
            var claim = DummyClaims.FirstOrDefault(c => c.ClaimId == id);
            if (claim == null) return NotFound();
            ViewBag.Users = DummyUsers;
            return View(claim);
        }

        //Coordinator/Manager view
        //GET: /Claim/ApproveerDashboard?role=Programme Coordinator
        public IActionResult ApproverDashboard(string role = "Programme Coordinator")
        {
            //show pending/submitted claims for approver
            var claims = DummyClaims.Where(c => c.Status == "Submitted" || c.Status == "Pending").OrderBy(c => c.SubmittedAt).ToList();
            ViewBag.Role = role;
            return View(claims);
        }


        //POST: /Claim/ApproveSimulation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ApproveSimulation(int id, int approverId)
        {
            //simulation only: No DB update, just show a message(no real update)
            TempData["Message"] = $"Claim #{id} approved by user #{approverId} .";
            return RedirectToAction("Details", new { id  });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RejectSimulation(int id, int approverId, string comment)
        {
            //simulation only: No DB update, just show a message(no real update)
            TempData["Message"] = $"Claim #{id} rejected by user #{approverId}. Comment: {comment}";
            return RedirectToAction("Details", new { id });
        }

        //helper: list of lecturers for UI (read-only)
        public IActionResult Lecturers()
        {
            return View(DummyLecturers);
        }
    }
}
