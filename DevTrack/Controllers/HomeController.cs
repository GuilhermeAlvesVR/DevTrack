using Microsoft.AspNetCore.Mvc;
using DevTrack.Models;
using DevTrack.Services;

namespace DevTrack.Controllers
{
    public class HomeController : Controller
    {
        private readonly GitHubService _gitHubService;

        public HomeController(GitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return View(new DevTrackViewModel
                {
                    ErrorMessage = "Digite um username válido."
                });
            }

            var user = await _gitHubService.GetUserAsync(username);

            if (user == null)
            {
                return View(new DevTrackViewModel
                {
                    ErrorMessage = "Usuário não encontrado no GitHub."
                });
            }

            var repos = await _gitHubService.GetReposAsync(username);
            var eventsList = await _gitHubService.GetEventsAsync(username);
            var languages = _gitHubService.GetLanguageStats(repos);

            var viewModel = new DevTrackViewModel
            {
                User = user,
                Repositories = repos.OrderByDescending(r => r.UpdatedAt).ToList(),
                Events = eventsList,
                Languages = languages
            };

            // Developer Score
            var score = 0;

            // Quantidade de repositórios
            score += Math.Min(viewModel.Repositories.Count * 2, 20);

            // Total de stars
            var totalStars = viewModel.Repositories.Sum(r => r.Stars);
            score += Math.Min(totalStars * 3, 15);

            // Diversidade de linguagens
            var totalLanguages = viewModel.Repositories
                .Where(r => !string.IsNullOrWhiteSpace(r.Language))
                .Select(r => r.Language)
                .Distinct()
                .Count();

            score += Math.Min(totalLanguages * 3, 15);

            // Repositórios com descrição
            var reposWithDescription = viewModel.Repositories
                .Count(r => !string.IsNullOrWhiteSpace(r.Description));

            score += Math.Min(reposWithDescription * 2, 10);

            // Repositórios atualizados nos últimos 30 dias
            var recentReposCount = viewModel.Repositories
                .Count(r => (DateTime.UtcNow - r.UpdatedAt).TotalDays <= 30);

            score += Math.Min(recentReposCount * 2, 10);

            // Eventos recentes
            score += Math.Min(viewModel.Events.Count * 2, 20);

            // Seguidores
            score += Math.Min(viewModel.User.Followers * 2, 10);

            // Limite máximo
            viewModel.DeveloperScore = Math.Min(score, 100);

            // Nível
            if (viewModel.DeveloperScore >= 70)
                viewModel.DeveloperLevel = "Avançado";
            else if (viewModel.DeveloperScore >= 40)
                viewModel.DeveloperLevel = "Intermediário";
            else
                viewModel.DeveloperLevel = "Iniciante";

            var insights = new List<string>();

            // Atividade recente
            if (viewModel.Events.Count >= 8)
                insights.Add("Alta atividade recente no GitHub.");
            else if (viewModel.Events.Count >= 3)
                insights.Add("Atividade recente moderada no GitHub.");
            else
                insights.Add("Baixa atividade recente no GitHub.");

            // Diversidade de linguagens
            if (totalLanguages >= 4)
                insights.Add("Boa diversidade de linguagens nos projetos.");
            else if (totalLanguages >= 2)
                insights.Add("Diversidade de linguagens razoável.");
            else
                insights.Add("Baixa diversidade de linguagens.");

            // Descrição dos repositórios
            if (viewModel.Repositories.Count > 0)
            {
                var descriptionRate = (double)reposWithDescription / viewModel.Repositories.Count;

                if (descriptionRate >= 0.7)
                    insights.Add("Os repositórios estão bem documentados com descrições.");
                else if (descriptionRate >= 0.4)
                    insights.Add("Parte dos repositórios possui descrição.");
                else
                    insights.Add("Muitos repositórios ainda estão sem descrição.");
            }

            // Atualizações recentes
            if (recentReposCount >= 4)
                insights.Add("Boa frequência de atualização recente dos projetos.");
            else if (recentReposCount >= 1)
                insights.Add("Há alguns projetos atualizados recentemente.");
            else
                insights.Add("Nenhum projeto foi atualizado recentemente.");

            // Popularidade
            if (viewModel.TotalStars >= 20)
                insights.Add("O perfil já demonstra alguma tração em stars.");
            else if (viewModel.TotalStars > 0)
                insights.Add("O perfil já recebeu stars, mas ainda tem espaço para crescer.");
            else
                insights.Add("O perfil ainda não possui tração visível em stars.");

            viewModel.Insights = insights;

            return View(viewModel);
        }
    }
}