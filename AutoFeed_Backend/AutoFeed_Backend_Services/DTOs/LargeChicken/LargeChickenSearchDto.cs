using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFeed_Backend_Services.DTOs.LargeChicken
{
    public class LargeChickenSearchDto
    {
        public string? Name { get; set; }
        public string? HealthStatus { get; set; }
        public int? FlockId { get; set; }
        public bool IncludeInactive { get; set; } = false;
    }
}
