using DevOps.Client.Session;
using DevOps.Core.Contracts;

namespace DevOps.Client.Authentication;

internal interface ISessionData {
    Task SetToken(string? token);
}

internal class SessionDataProvider : ITokenProvider, ISessionData {
    const string TokenKey = "SessionToken";
    private readonly ISessionDataStore _sessionData;

    public SessionDataProvider(ISessionDataStore sessionData) {
        _sessionData = sessionData;
    }

    public async Task<string?> GetToken() {
        string result = await _sessionData.Get<string>(TokenKey);
        return result;
    }

    public async Task SetToken(string? token) {
        if (token == null) {
            await _sessionData.Delete(TokenKey);
            return;
        }

        await _sessionData.Set(TokenKey, token);
    }
}
