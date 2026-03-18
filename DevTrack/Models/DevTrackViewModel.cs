namespace DevTrack.Models
{
    public class DevTrackViewModel
    {
        public GitHubUser User { get; set; }
        public List<GitHubRepo> Repositories { get; set; } = new();
        public List<GitHubEvent> Events { get; set; } = new();
        public Dictionary<string, int> Languages { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public int DeveloperScore { get; set; }
        public string DeveloperLevel { get; set; } = "Iniciante";

        public List<string> Insights { get; set; } = new();

        public int TotalStars =>
            Repositories.Sum(r => r.Stars);

        public string MainLanguage =>
            Languages.Any()
                ? Languages.OrderByDescending(x => x.Value).First().Key
                : "N/A";

        public GitHubRepo? MostStarredRepo =>
            Repositories.OrderByDescending(r => r.Stars).FirstOrDefault();

        public GitHubRepo? MostRecentlyUpdatedRepo =>
            Repositories.OrderByDescending(r => r.UpdatedAt).FirstOrDefault();

        public GitHubEvent? LatestEvent =>
            Events.OrderByDescending(e => e.CreatedAt).FirstOrDefault();

        public int RecentEventsCount =>
            Events.Count(e => e.CreatedAt >= DateTime.UtcNow.AddDays(-7));

        public string DeveloperStatus
        {
            get
            {
                if (LatestEvent == null)
                    return "Inativo";

                var daysSinceLastEvent = (DateTime.UtcNow - LatestEvent.CreatedAt).TotalDays;

                if (daysSinceLastEvent <= 3)
                    return "Ativo";

                if (daysSinceLastEvent <= 10)
                    return "Moderado";

                return "Inativo";
            }
        }

        public string DeveloperStatusClass
        {
            get
            {
                return DeveloperStatus switch
                {
                    "Ativo" => "status-active",
                    "Moderado" => "status-moderate",
                    _ => "status-inactive"
                };
            }
        }

        public string LastActivityRelative
        {
            get
            {
                if (LatestEvent == null)
                    return "Sem atividade recente";

                var span = DateTime.UtcNow - LatestEvent.CreatedAt;

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