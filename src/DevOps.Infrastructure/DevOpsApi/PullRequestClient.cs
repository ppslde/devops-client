using DevOps.Core.Model;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace DevOps.Infrastructure.DevOpsApi
{
    internal class PullRequestClient
    {
        private readonly string _baseUrl;
        private readonly string _personalAccessToken;

        public PullRequestClient(string baseUrl, string personalAccessToken)
        {
            _baseUrl = baseUrl;
            _personalAccessToken = personalAccessToken;
        }

        public async Task<IEnumerable<PullRequest>> GetPullRequestsAsync(string repositoryId)
        {
            var url = $"{_baseUrl}/{repositoryId}/pullrequests?api-version=6.1-preview.1";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($":{_personalAccessToken}")));

            using var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var result = JsonSerializer.Deserialize<PullRequest[]>(json, options);

            return result;
        }
    }
}
