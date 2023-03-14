using DevOps.Core.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace DevOps.Client.Authentication;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
  public CustomAuthenticationStateProvider()
  {
    CurrentUser = GetAnonymous();
  }

  private ClaimsPrincipal CurrentUser { get; set; }

  private ClaimsPrincipal GetUser(AppUser user, params string[] roles)
  {

    var identity = new ClaimsIdentity(new[]
    {
            new Claim(ClaimTypes.Email, user.Profile.EmailAddress),
            new Claim(ClaimTypes.Name, user.Profile.DisplayName),
            //new Claim("Avatar", string.Format("data:image/png;base64,{0}", Convert.ToBase64String(user.Avatar.Value.)))
        }, "Authentication type");
    identity.AddClaims(roles.Select(r => new Claim(ClaimTypes.Role, r)));

    return new ClaimsPrincipal(identity);
  }

  private ClaimsPrincipal GetAnonymous()
  {
    var identity = new ClaimsIdentity(new[]
   {
         new Claim(ClaimTypes.Sid, "0"),
         new Claim(ClaimTypes.Name, "Anonymous"),
         new Claim(ClaimTypes.Role, "Anonymous")
     }, null);

    return new ClaimsPrincipal(identity);
  }

  public override Task<AuthenticationState> GetAuthenticationStateAsync() => Task.FromResult(new AuthenticationState(CurrentUser));

  public Task<AuthenticationState> ChangeUser(AppUser user, string role)
  {
    CurrentUser = GetUser(user, role);
    var task = GetAuthenticationStateAsync();
    NotifyAuthenticationStateChanged(task);
    return task;
  }

  public Task<AuthenticationState> Logout()
  {
    CurrentUser = GetAnonymous();
    var task = GetAuthenticationStateAsync();
    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    return task;
  }
}
