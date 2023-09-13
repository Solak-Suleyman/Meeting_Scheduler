using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication4.GenericRepository;
using WebApplication4.Models.Context;
using WebApplication4.Models.DTO;
using WebApplication4.Models.Entity;
using WebApplication4.UnitOfWork;

namespace WebApplication4.NonGenericRepository
{
    public class AuthorizationRepository : GenericRepository<User>, IAuthorizationRepository
    {
        public IConfiguration _configuration;
        public AuthorizationRepository(IUnitOfWork<MeetingSchedulerContext> unitOfWork, IConfiguration configuration) : base(unitOfWork)
        {
            _configuration = configuration;
        }

        public AuthorizationResponse GenerateAuthorizationToken(string userId, string userName)
        {
            var now = DateTime.UtcNow;
            var secret = _configuration.GetValue<string>("Secret");
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

            var userClaims = new List<Claim>
    {
        new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
        new Claim(ClaimTypes.NameIdentifier, userId),
    };

            //userClaims.AddRange(roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));

            var expires = now.Add(TimeSpan.FromMinutes(60));

            var jwt = new JwtSecurityToken(
                    notBefore: now,
                    claims: userClaims,
                    expires: expires,
                    audience: "https://localhost:7000/",
                    issuer: "https://localhost:7000/",
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            //we don't know about thread safety of token handler

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var resp = new AuthorizationResponse
            {
                UserId = userId,
                AuthorizationToken = encodedJwt,
                RefreshToken = string.Empty
            };

            return resp;
        }
    }
}
