using System;

namespace ContractMonthlyClaimSystem.Models
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public string Month { get; set; }    //e.g "September 2025"
        public decimal TotalAmount { get; set; }  //e.g: 4500.00
        public string Status{ get; set; }   //submitted/approved
        public DateTime SubmittedAt { get; set; }
       
    }
}
