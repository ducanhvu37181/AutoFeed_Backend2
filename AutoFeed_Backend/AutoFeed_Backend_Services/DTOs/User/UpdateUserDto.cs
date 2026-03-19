using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFeed_Backend_Services.DTOs.User
{
    public class UpdateUserDto
    {
        public int UserId { get; set; }
        public int? RoleId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
    }
}
