using System;
using System.Collections.Generic;

namespace ContractMonthlyClaimSystem.Models
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public int LecturerId { get; set; }  //FK to Lecturer
        public string Month { get; set; }    //e.g "September 2025"
        public decimal TotalHours { get; set; }  //e.g: 150.5
        public decimal TotalAmount { get; set; }  //e.g: 4500.00
        public string Status{ get; set; }   //submitted/approved/rejected/pending/draft
        public DateTime SubmittedAt { get; set; }
        public int? ApprovedBy { get; set; }  //FK to User
        public DateTime? ApprovedAt { get; set; }


        // Navigation properties (filled from dummy data)
        public List<ClaimLine> ClaimLines { get; set; } = new List<ClaimLine>();
        public List<SupportingDocument> Documents { get; set; } = new List<SupportingDocument>();

    }
}
