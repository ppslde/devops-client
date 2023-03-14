using DevOps.Core.Models;

namespace DevOps.Core.Contracts;

public interface IAvatarClient
{
  Task<Avatar> GetAvatar(UserProfile profile);
  Task<Avatar> GetAvatar(Organisation org, UserProfile profile);
}
