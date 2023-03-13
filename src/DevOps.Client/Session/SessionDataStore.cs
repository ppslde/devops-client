using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace DevOps.Client.Session;

internal class SessionDataStore : ISessionDataStore
{

  private readonly ProtectedSessionStorage _protectedSessionStore;

  public SessionDataStore(ProtectedSessionStorage protectedSessionStore)
  {
    _protectedSessionStore = protectedSessionStore;
  }

  public async Task<T?> Get<T>(string key)
  {

    ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

    var result = await _protectedSessionStore.GetAsync<T>(key);
    if (!result.Success)
      return default;

    ArgumentNullException.ThrowIfNull(result.Value, nameof(ProtectedBrowserStorageResult<T>.Value));
    return result.Value;
  }

  public async Task Set<T>(string key, T value)
  {

    ArgumentNullException.ThrowIfNull(value, nameof(value));
    ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

    await _protectedSessionStore.SetAsync(key, value);
  }

  public async Task Delete(string key)
  {

    ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

    await _protectedSessionStore.DeleteAsync(key);
  }
}
