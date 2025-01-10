using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
namespace DessertAppDevOpsStatistics.Services
{
    public class SecretService
    {
        private readonly ILogger<SecretService> _logger;

        public SecretService(ILogger<SecretService> logger)
        {
            _logger = logger;
        }

        public async Task<string> GetSecretFromVaultAsync()
        {
            try
            {
                //To authenticate resource, you need a managed identity in Azure Key Vault
                var keyVaultName = "dessertkeyvault";
                var kvUri = $"https://{keyVaultName}.vault.azure.net";
                var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
                var secret = await client.GetSecretAsync("PERSONALACCESSTOKEN");
                return secret.Value.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to get secret, see more details: {ex.Message}");
                return $"An unexpected error has occurred, see details: {ex.Message}";
            }
        }
    }
}
