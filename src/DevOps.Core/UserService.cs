using DevOps.Core.Contracts;
using DevOps.Core.Interfaces;
using DevOps.Core.Models;

namespace DevOps.Core;

internal class UserService : IUserService
{
    private readonly IUserProfileClient _profileClient;
    private readonly IOragnisationClient _oragnisationClient;

    public UserService(IUserProfileClient profileClient, IOragnisationClient oragnisationClient)
    {
        _profileClient = profileClient;
        _oragnisationClient = oragnisationClient;
    }

    public async Task<AppUser> GetCurrentUser()
    {
        UserProfile profile = await _profileClient.GetUserProfile();

        await _oragnisationClient.GetAccounts(profile);

        return null;
    }
}
