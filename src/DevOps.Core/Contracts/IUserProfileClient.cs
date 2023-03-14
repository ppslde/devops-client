using DevOps.Core.Models;

namespace DevOps.Core.Contracts;

public interface IUserProfileClient
{
  Task<UserProfile> GetUserProfile();
}
