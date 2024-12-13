using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.RolesDTOs
{
    public class RoleAssignDTO
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public bool RoleExist { get; set; }
    }
}
