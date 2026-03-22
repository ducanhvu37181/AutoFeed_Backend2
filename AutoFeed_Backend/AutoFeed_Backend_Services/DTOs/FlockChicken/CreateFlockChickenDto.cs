namespace AutoFeed_Backend_Services.DTOs.FlockChicken
{
    public class CreateFlockChickenDto
    {
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public decimal? Weight { get; set; }
        public int? Age { get; set; }
        public string HealthStatus { get; set; }
        public string Note { get; set; }
    }
}
