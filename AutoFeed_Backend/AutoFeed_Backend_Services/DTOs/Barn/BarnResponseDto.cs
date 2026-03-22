namespace AutoFeed_Backend_Services.DTOs.Barn
{
    public class BarnResponseDto
    {
        public int BarnId { get; set; }
        public decimal? Temperature { get; set; }
        public decimal? Humidity { get; set; }
        public string Type { get; set; }
        public decimal? Area { get; set; }
        public bool Status { get; set; } 
        public DateTime? CreateDate { get; set; }
    }
}
