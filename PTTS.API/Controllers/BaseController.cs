using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TodoAppWithAuth.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedAccessException("User ID not found in claims.");
        }
    }
}
