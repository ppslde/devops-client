using DevOps.Core.Contracts;
using DevOps.Core.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace DevOps.Infrastructure.AzureDevOps;

internal class UserProfileClient : IUserProfileClient
{

  private readonly ITokenProvider _tokenProvider;

  private const string url = "https://vssps.dev.azure.com/_apis/profile/profiles/me?api-version=7.0";

  public UserProfileClient(ITokenProvider tokenProvider)
  {

    ArgumentNullException.ThrowIfNull(tokenProvider, nameof(ITokenProvider));
    _tokenProvider = tokenProvider;
  }

  public async Task<UserProfile> GetUserProfile()
  {

    var token = await _tokenProvider.GetToken();

    using var client = new HttpClient();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($":{token}")));

    using var response = await client.GetAsync(url);
    response.EnsureSuccessStatusCode();
    var json = await response.Content.ReadAsStringAsync();

    var options = new JsonSerializerOptions
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
    var result = JsonSerializer.Deserialize<UserProfile>(json, options);

    return result;

  }
}
