using System.Text.Json;
using DevTrack.Models;

namespace DevTrack.Services
{
    public class GitHubService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public GitHubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.github.com/");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "DevTrackApp");

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<GitHubUser?> GetUserAsync(string username)
        {
            var response = await _httpClient.GetAsync($"users/{username}");

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GitHubUser>(json, _jsonOptions);
        }

        public async Task<List<GitHubRepo>> GetReposAsync(string username)
        {
            var response = await _httpClient.GetAsync($"users/{username}/repos?sort=updated&per_page=100");

            if (!response.IsSuccessStatusCode)
                return new List<GitHubRepo>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GitHubRepo>>(json, _jsonOptions) ?? new List<GitHubRepo>();
        }

        public async Task<List<GitHubEvent>> GetEventsAsync(string username)
        {
            var response = await _httpClient.GetAsync($"users/{username}/events/public?per_page=10");

            if (!response.IsSuccessStatusCode)
                return new List<GitHubEvent>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GitHubEvent>>(json, _jsonOptions) ?? new List<GitHubEvent>();
        }

        public Dictionary<string, int> GetLanguageStats(List<GitHubRepo> repos)
        {
            return repos
                .Where(r => !string.IsNullOrWhiteSpace(r.Language))
                .GroupBy(r => r.Language)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}