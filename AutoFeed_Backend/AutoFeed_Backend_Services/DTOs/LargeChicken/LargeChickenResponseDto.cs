using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFeed_Backend_Services.DTOs.LargeChicken
{
    public class LargeChickenResponseDto
    {
        public int ChickenLid { get; set; }
        public int? FlockId { get; set; }
        public string Name { get; set; }
        public decimal? Weight { get; set; }
        public int? Age { get; set; }
        public string HealthStatus { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
    }
}
