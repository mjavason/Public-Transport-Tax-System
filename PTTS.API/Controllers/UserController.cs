using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PTTS.Application.Queries.User;
using PTTS.Core.Domain.UserAggregate;
using PTTS.Core.Domain.UserAggregate.DTOs;
using PTTS.Infrastructure.DatabaseContext;

namespace PTTS.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ApiBaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserController(ApplicationDbContext context, UserManager<User> userManager, IMediator mediator) : base(mediator)
        {
            _userManager = userManager;
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMyProfile()
        {
            // Access the ClaimsPrincipal via HttpContext.User
            Guid userId = Guid.Parse(GetUserId());
            var query = new GetUserByIdQuery { UserId = userId };
            var result = await _mediator.Send(query);

            return GetActionResult(result, "User profile retrieved successfully");
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserProfile(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            return Ok(new
            {
            user.UserName,
            user.Email,
            user.FullName,
            user.DateOfBirth
            });
        }

        // [HttpPut()]
        // public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserProfileDto model)
        // {
        //     string id = GetUserId();
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