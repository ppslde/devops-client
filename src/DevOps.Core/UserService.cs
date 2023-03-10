using DevOps.Core.Contracts;
using DevOps.Core.Interfaces;
using DevOps.Core.Models;

namespace DevOps.Core {
    internal class UserService : IUserService {
        private readonly IUserProfileClient _profileClient;

        public UserService(IUserProfileClient profileClient) {
            _profileClient = profileClient;
        }

        public async Task<AppUser> GetCurrentUser() {

            await _profileClient.GetUserProfile();

            return null;
        }
    }
}
