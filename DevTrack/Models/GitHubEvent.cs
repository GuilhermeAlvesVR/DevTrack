using System.Text.Json.Serialization;

namespace DevTrack.Models
{
    public class GitHubEvent
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("repo")]
        public GitHubEventRepo? Repo { get; set; }

        public string TypeDisplay
        {
            get
            {
                return Type switch
                {
                    "PushEvent" => "Push realizado",
                    "CreateEvent" => "Repositório ou branch criado",
                    "PublicEvent" => "Repositório tornou-se público",
                    "WatchEvent" => "Repositório favoritado",
                    "ForkEvent" => "Fork realizado",
                    _ => Type
                };
            }
        }
    }

    public class GitHubEventRepo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}