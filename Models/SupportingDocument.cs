using System;

namespace ContractMonthlyClaimSystem.Models
{
    public class SupportingDocument
    {
        public int DocumentId { get; set; }
        public int ClaimId { get; set; }  //FK to Claim
        public string FileName { get; set; }  //e.g: "invoice_september.pdf"
        public string FilePath { get; set; }  //placeholder - not used for storage in prototype
        public DateTime UploadedAt { get; set; }
    }
}
