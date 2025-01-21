using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PTTS.Core.Domain.UserAggregate;

namespace PTTS.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController : ControllerBase
    {
    }

    public class UpdateUserProfileDto
    {
        public string? FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}