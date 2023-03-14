using DevOps.Core.Contracts;
using DevOps.Core.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace DevOps.Infrastructure.AzureDevOps;

internal class AvatarClient : IAvatarClient
{
  private readonly ITokenProvider _tokenProvider;

  private string url = "https://vssps.dev.azure.com/{0}/_apis/graph/Subjects/{1}/avatars?api-version=7.0";
  //private const string url = "https://vssps.dev.azure.com/{0}/_apis/graph/users/{1}/avatar";

  public AvatarClient(ITokenProvider tokenProvider)
  {
    ArgumentNullException.ThrowIfNull(tokenProvider, nameof(ITokenProvider));
    _tokenProvider = tokenProvider;
  }

  public async Task<Avatar> GetAvatar(UserProfile profile)
  {
    var token = await _tokenProvider.GetToken();
    var organisation = await _tokenProvider.GetOrganisation();

    return await LoadAvatar(token, organisation, profile.Id);
  }

  public async Task<Avatar> GetAvatar(Organisation org, UserProfile profile)
  {
    var token = await _tokenProvider.GetToken();
    return await LoadAvatar(token, org.AccountName, profile.Id);
  }

  private async Task<Avatar> LoadAvatar(string token, string organisation, Guid userId)
  {
    using var client = new HttpClient();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($":{token}")));

    using var response = await client.GetAsync(string.Format(url, organisation, userId));
    response.EnsureSuccessStatusCode();
    var json = await response.Content.ReadAsStringAsync();

    var options = new JsonSerializerOptions
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
    var result = JsonSerializer.Deserialize<Avatar>(json, options);

    return result;
  }
}
