using DevOps.Core.Models;

namespace DevOps.Core.Interfaces;

public interface IUserService
{

  public Task<AppUser> GetCurrentUser();
}
