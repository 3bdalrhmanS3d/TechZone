using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechZone.Core.Data;

namespace TechZone.Core.Service.Interfaces
{
    public interface IUserPermissionService
    {
        bool HasPermission(string userId, Permission permission);
    }
}
