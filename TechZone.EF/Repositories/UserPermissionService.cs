using TechZone.Core.Data;
using TechZone.Core.Entities;
using TechZone.Core.Interfaces;
using TechZone.Core.Service.Interfaces;
using TechZone.EF.Application;

public class UserPermissionService : IUserPermissionService
{
    private readonly ApplicationDbContext _dbContext;

    public UserPermissionService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool HasPermission(string userId, Permission permission)
    {
        return _dbContext.Set<UserPermission>()
            .Any(up => up.UserId == userId && up.PermissionId == permission);
    }
}
