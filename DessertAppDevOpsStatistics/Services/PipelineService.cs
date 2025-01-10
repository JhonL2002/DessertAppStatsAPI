using DessertAppDevOpsStatistics.Models;
using DessertAppDevOpsStatistics.Responses;
using System.Net.Http.Headers;
using System.Text;

namespace DessertAppDevOpsStatistics.Services
{
    public class PipelineService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<PipelineService> _logger;
        private readonly SecretService _secretService;

        public PipelineService(
            IHttpClientFactory httpClientFactory,
            ILogger<PipelineService> logger,
            SecretService secretService)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _secretService = secretService;
        }

        public async Task<List<Pipeline>> GetSuccessfulPipelines()
        {
            var organization = "JhonL2002";
            var project = "dessertapp";
            var pipelineId = 5;
            var personalAccessToken = await _secretService.GetSecretFromVaultAsync();

            //Define the URL API to get stats from Azure DevOps
            var url = $"https://dev.azure.com/{organization}/{project}/_apis/build/builds?definitions={pipelineId}&statusFilter=completed&resultFilter=succeeded&$top=10&api-version=6.0";

            //Create a client using IHttpClientFactory to send a GET request
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($":{personalAccessToken}")));

            //Fetch data
            try
            {
                var response = await client.GetFromJsonAsync<PipelineResponse>(url);
                return response?.Value ?? new List<Pipeline>();
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Unable to get pipelines, see more details: {ex}");
            }
            return null!;

        }
    }
}
