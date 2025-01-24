using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PTTS.Core.Domain.UserAggregate;
using PTTS.Infrastructure.DatabaseContext;
using TodoAppWithAuth.Controllers;

namespace PTTS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet(Name = "GetMyProfile")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMyProfile()
        {
            // Access the ClaimsPrincipal via HttpContext.User
            string userId = GetUserId();
            var user = await _context.Users.FindAsync(userId);

            if (user == null) return NotFound("User not found.");
            return Ok(user);
        }

        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetUserProfile(string id)
        // {
        //     var user = await _userManager.FindByIdAsync(id);
        //     if (user == null) return NotFound();

        //     return Ok(new
        //     {
        //         user.UserName,
        //         user.Email,
        //         user.FullName,
        //         user.DateOfBirth
        //     });
        // }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateUserProfile(string id, [FromBody] UpdateUserProfileDto model)
        // {
        //     var user = await _userManager.FindByIdAsync(id);
        //     if (user == null) return NotFound();

        //     if (model.FullName != null) user.FullName = model.FullName;
        //     if (model.FirstName != null) user.FirstName = model.FirstName;
        //     if (model.LastName != null) user.LastName = model.LastName;

        //     var result = await _userManager.UpdateAsync(user);
        //     if (!result.Succeeded) return BadRequest(result.Errors);

        //     return NoContent();
        // }

    }
}