using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Competitor.Common;
using Competitor.Model.Identity;
using Competitor.Service.Identity;

namespace Competitor.Web.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class ChangePasswordController : Controller
    {
        private readonly IUsersService _usersService;
        public ChangePasswordController(IUsersService usersService)
        {
            _usersService = usersService;
            _usersService.CheckArgumentIsNull(nameof(usersService));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody]ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _usersService.GetCurrentUserAsync();
            if (user == null)
            {
                return BadRequest("NotFound");
            }

            var (Succeeded, Error) = await _usersService.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (Succeeded)
            {
                return Ok();
            }

            return BadRequest(Error);
        }
    }
}