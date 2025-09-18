using System;

namespace ContractMonthlyClaimSystem.Models
{
    public class ClaimLine
    {
        public int ClaimLineId { get; set; }
        public int ClaimId { get; set; }  //FK to Claim
        public string Description { get; set; }  //e.g: "Lecture for CS101"
        public decimal HoursWorked { get; set; }  //e.g: 10.5
        public decimal RatePerHour { get; set; }  //e.g: 315.00 (calculated as Hours * Lecturer.HourlyRate)
        public decimal Subtotal { get; set; }  //e.g: 3307.50 (calculated as HoursWorked * RatePerHour)
        
    }
}
