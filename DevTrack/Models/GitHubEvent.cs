using System.Text.Json.Serialization;

namespace DevTrack.Models
{
    public class GitHubEvent
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("repo")]
        public GitHubEventRepo Repo { get; set; }

        public string TypeDisplay =>
            Type switch
            {
                "PushEvent" => "Push realizado",
                "CreateEvent" => "Repositório ou branch criado",
                "WatchEvent" => "Repositório estrelado",
                "ForkEvent" => "Repositório forkado",
                "PublicEvent" => "Repositório tornou-se público",
                _ => "Atividade no GitHub"
            };
    }

    public class GitHubEventRepo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}