using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication4.GenericRepository;
using WebApplication4.Models.Context;
using WebApplication4.Models.DTO;
using WebApplication4.NonGenericRepository;
using WebApplication4.UnitOfWork;

namespace WebApplication4.Controllers
{
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private IUnitOfWork<MeetingSchedulerContext> unitOfWork = new UnitOfWork<MeetingSchedulerContext>();
        private readonly AuthContext _authContext;
        private readonly IUserRepository _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private AuthorizationRepository _authorizationRepository;
        private readonly IConfiguration _configuration;

        public AuthorizationController(
            AuthContext authContext,
            IConfiguration configuration,
            SignInManager<IdentityUser> signInManager,
            IUserStore<IdentityUser> userStore)
        {
            _authorizationRepository=new AuthorizationRepository(unitOfWork,configuration);
            _userManager = new UserRepository(unitOfWork);
            _configuration = configuration;
            _signInManager = signInManager;
            _authContext = authContext;

            _emailStore = (IUserEmailStore<IdentityUser>)userStore;
            _userStore = userStore;
        }
        [HttpPost("authorization/token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] GetTokenRequest request)
        {
            var user = _userManager.GetByUserName(request.UserName);

            if (user == null)
            {
                //401 or 404
                return Unauthorized();
            }
            if (!user.password.Equals(request.Password))
            {
                return Unauthorized();
            }



            var resp = _authorizationRepository.GenerateAuthorizationToken(user.id.ToString(), user.user_name);

            return Ok(resp);
        }
    }
}
