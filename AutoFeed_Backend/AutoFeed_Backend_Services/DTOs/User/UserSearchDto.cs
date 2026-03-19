using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFeed_Backend_Services.DTOs.User
{
    public class UserSearchDto
    {
        public string? Keyword { get; set; }  // tìm theo fullName, email, username
        public int? RoleId { get; set; }
        public bool IncludeInactive { get; set; } = false;
    }
}
