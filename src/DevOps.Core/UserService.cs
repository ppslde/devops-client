using DevOps.Core.Contracts;
using DevOps.Core.Interfaces;
using DevOps.Core.Models;

namespace DevOps.Core;

internal class UserService : IUserService
{
  private readonly IUserProfileClient _profileClient;
  private readonly IAvatarClient _avatarClient;

  public UserService(IUserProfileClient profileClient, IAvatarClient avatarClient)
  {
    _profileClient = profileClient;
    _avatarClient = avatarClient;
  }

  public async Task<AppUser?> GetCurrentUser()
  {
    UserProfile profile = await _profileClient.GetUserProfile();
    //var avatar = await _avatarClient.GetAvatar(profile);

    return new(profile, null);
  }
}
