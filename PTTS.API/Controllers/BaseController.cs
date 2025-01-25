using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PTTS.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected string GetUserId()
        {
            var user = HttpContext.User;
            return user.FindFirstValue(ClaimTypes.NameIdentifier)
                            ?? throw new UnauthorizedAccessException("User ID not found in claims.");
        }
    }
}
