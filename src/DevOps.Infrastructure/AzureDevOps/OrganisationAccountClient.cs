using DevOps.Core.Contracts;
using DevOps.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DevOps.Infrastructure.AzureDevOps;

internal class OrganisationAccountClient : IOragnisationClient
{
    private readonly ITokenProvider _tokenProvider;

    private string url = "https://app.vssps.visualstudio.com/_apis/accounts?api-version=7.0";

    public OrganisationAccountClient(ITokenProvider tokenProvider)
    {

        ArgumentNullException.ThrowIfNull(tokenProvider, nameof(ITokenProvider));
        _tokenProvider = tokenProvider;
    }

    public async Task<IEnumerable<Oragnisation>> GetAccounts(UserProfile profile)
    {
        var token = await _tokenProvider.GetToken();

        url += $"&memberId={profile.PublicAlias}";

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
        var result = JsonSerializer.Deserialize<Response>(json, options);

        return result.Value;

    }


    private class Response
    {
        public int Count { get; set; }
        public Oragnisation[] Value { get; set; }
    }
}
