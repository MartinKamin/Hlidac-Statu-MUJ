namespace HlidacStatu.JobsWeb.Models
{
    public class JobStatistics
    {
        public string Name { get; set; }
        public decimal DolniKvartil { get; set; }
        public decimal HorniKvartil { get; set; }
        public decimal Minimum { get; set; }
        public decimal Maximum { get; set; }
        public decimal Average { get; set; }
        public decimal Median { get; set; }
        
        public decimal LeftWhisk { get; set; }
        public decimal RightWhisk { get; set; }
        public decimal[] LowOutliers { get; set; }
        public decimal[] HighOutliers { get; set; }
        public int ContractCount { get; set; }
        public int SupplierCount { get; set; }
    }

    public class JobPrecalculated
    {
        public string SmlouvaId { get; set; }
        public string IcoOdberatele { get; set; }
        public string[] IcoDodavatele { get; set; }
    
        public int Year { get; set; }
    
        public long JobPk { get; set; }
        public string JobGrouped { get; set; }
        public decimal SalaryMd { get; set; }
        public decimal SalaryMdVat { get; set; }
        public string Subject { get; set; }
        public string[] Tags { get; set; }
        
        
    }
}