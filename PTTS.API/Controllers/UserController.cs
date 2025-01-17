using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTTS.Infrastructure.DatabaseContext;
using TodoAppWithAuth.Controllers;

namespace PTTS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet(Name = "GetMyProfile")]
        [Authorize]
        public async Task<IActionResult> GetMyProfile()
        {
            // Access the ClaimsPrincipal via HttpContext.User
            string userId = GetUserId();
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

    }
}