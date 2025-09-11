using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechZone.Core.Data;

namespace TechZone.Core.Entities
{
    public class UserPermission
    {
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }
        public Permission PermissionId { get; set; }
    }
}
