namespace DevOps.Client.Session;

internal interface ISessionDataStore
{
  Task Delete(string key);
  Task<T> Get<T>(string key);
  Task Set<T>(string key, T value);
}
