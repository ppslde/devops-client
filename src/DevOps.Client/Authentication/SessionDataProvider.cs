using DevOps.Client.Session;
using DevOps.Core.Contracts;

namespace DevOps.Client.Authentication;

internal interface ISessionData
{
  Task SetAuthData(AuthModel authData);
  Task Clear();
}

internal class SessionDataProvider : ITokenProvider, ISessionData
{
  const string TokenKey = "SessionToken";
  const string OrgKey = "AzBaseOrganisation";

  private readonly ISessionDataStore _sessionData;

  public SessionDataProvider(ISessionDataStore sessionData)
  {
    _sessionData = sessionData;
  }

  public async Task SetAuthData(AuthModel authData)
  {
    ArgumentNullException.ThrowIfNull(authData, nameof(AuthModel));
    ArgumentException.ThrowIfNullOrEmpty(authData.Token, nameof(AuthModel.Token));
    ArgumentException.ThrowIfNullOrEmpty(authData.Organisation, nameof(AuthModel.Organisation));

    await _sessionData.Set(TokenKey, authData.Token);
    await _sessionData.Set(OrgKey, authData.Organisation);
  }

  public async Task Clear()
  {
    await _sessionData.Delete(TokenKey);
    await _sessionData.Delete(OrgKey);
  }

  public async Task<string?> GetToken()
  {
    string result = await _sessionData.Get<string>(TokenKey);
    return result;
  }

  public async Task<string?> GetOrganisation()
  {
    string result = await _sessionData.Get<string>(OrgKey);
    return result;
  }
}
