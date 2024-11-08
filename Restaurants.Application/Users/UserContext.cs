using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Restaurants.Application.Users
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }

    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public CurrentUser? GetCurrentUser()
        {
            var user = httpContextAccessor?.HttpContext?.User;
            if (user == null)
            {
                throw new InvalidOperationException("User context is not present");
            }

            if (user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            var userId = user.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(claim => claim.Type == ClaimTypes.Email)!.Value;
            var roles = user.Claims.Where(claim => claim.Type == ClaimTypes.Role)!.Select(claim => claim.Value);
            var nationality = user.FindFirst(claim => claim.Type == "Nationality")?.Value;
            var dateOfBirthString = user.FindFirst(claim => claim.Type == "DateOfBirth")?.Value;
            var dateOfBirth = dateOfBirthString == null 
                ? (DateOnly?) null
                :DateOnly.ParseExact(dateOfBirthString,"yyyy-MM-dd");

            return new CurrentUser(userId, email, roles, nationality, dateOfBirth);
        }
    }
}
