using System.Text.Json.Serialization;

namespace DevTrack.Models
{
    public class GitHubRepo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("stargazers_count")]
        public int Stars { get; set; }

        [JsonPropertyName("forks_count")]
        public int Forks { get; set; }

        [JsonPropertyName("html_url")]
        public string HtmlUrl { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public string UpdatedRelative
        {
            get
            {
                var span = DateTime.UtcNow - UpdatedAt;

                if (span.TotalDays >= 1)
                    return $"há {(int)span.TotalDays} dias";

                if (span.TotalHours >= 1)
                    return $"há {(int)span.TotalHours} horas";

                if (span.TotalMinutes >= 1)
                    return $"há {(int)span.TotalMinutes} minutos";

                return "agora";
            }
        }
    }
}