using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using TechZone.Core.Data;
using TechZone.Core.Interfaces;
using TechZone.Core.Service.Interfaces;

namespace TechZone.Api.Authorization
{
    public class PermissionBasedAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IUserPermissionService _userPermissionService;

        public PermissionBasedAuthorizationFilter(IUserPermissionService userPermissionService)
        {
            _userPermissionService = userPermissionService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // نجيب الـ Attribute اللي محطوط على الـ Action أو الـ Controller
            var attribute = context.ActionDescriptor.EndpointMetadata
                .OfType<CheckPermissionAttribute>()
                .FirstOrDefault();

            if (attribute == null) return; // مفيش Permission مطلوب

            var claimIdentity = context.HttpContext.User.Identity as ClaimsIdentity;

            if (claimIdentity == null || !claimIdentity.IsAuthenticated)
            {
                context.Result = new ForbidResult();
                return;
            }

            var userId = claimIdentity.FindFirst("uid")?.Value;

            if (userId == null || !_userPermissionService.HasPermission(userId, attribute.Permission))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
