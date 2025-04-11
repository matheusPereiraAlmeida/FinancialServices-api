using Newtonsoft.Json;
using Questao2.Entidades;
using Questao2.Interfaces;

namespace Questao2.Service
{
    public class APIConnector : IAPI
    {
        private HttpClient HttpClient { get; }
        private string Url { get; }
        public APIConnector(HttpClient client, string url) 
        {
            HttpClient = client;
            Url = url;
        }

        public async Task<int> GetGoalsAsync(int year, string team, bool isTeam1)
        {
            var goals = 0;
            var teamParam = isTeam1 ? $"team1={team}" : $"team2={team}";

            // Primeira chamada só para descobrir o total de páginas
            var firstUrl = $"{Url}year={year}&{teamParam}&page=1";
            var firstResponse = await HttpClient.GetStringAsync(firstUrl);
            
            var firstResult = JsonConvert.DeserializeObject<MatchResponse>(firstResponse);
            if (firstResult == null)
                return 0;

            var totalPages = firstResult.Total_Pages;
            var tasks = Enumerable.Range(1, totalPages).Select(async page =>
            {
                string url = $"{Url}year={year}&{teamParam}&page={page}";
                string response = await HttpClient.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<MatchResponse>(response);

                return result.Data
                    .Select(match => int.TryParse(isTeam1 ? match.Team1Goals : match.Team2Goals, out int g) ? g : 0)
                    .Sum();
            });

            // Aguarda todas as tarefas e soma os resultados
            var goalsPerPage = await Task.WhenAll(tasks);
            goals = goalsPerPage.Sum();

            return goals;
        }

        public int GetGoals(int year, string team, bool isTeam1)
        {
            throw new NotImplementedException();
        }
    }
}
