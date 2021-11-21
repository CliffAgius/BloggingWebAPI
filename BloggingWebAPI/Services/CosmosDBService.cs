using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Services.AppAuthentication;
using BloggingWebAPI.Models;
using System.Diagnostics;
using Microsoft.Azure.KeyVault;

namespace BloggingWebAPI.Services
{
    public static class CosmosDBService
    {
        private static CosmosClient CosmosClient { get; set; }

        private static async Task InitDBConnection()
        {
            var secretUri = "https://cas-blog.vault.azure.net/secrets/CosmosDBConnectionString/78f5632ae6dc4e989375db59b0af12f6";
            var keyVaultTokenProvider = new AzureServiceTokenProvider().KeyVaultTokenCallback;
            var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(keyVaultTokenProvider));

            var mySecret = await keyVaultClient.GetSecretAsync(secretUri);
            CosmosClient = new CosmosClient(mySecret.Value);
        }

        public static async Task<List<T>> Get<T>(string Query = "")
        {
            try
            {
                var allItems = new List<T>();

                if (CosmosClient is null)
                {
                    await InitDBConnection();
                }

                QueryRequestOptions options = new QueryRequestOptions() { MaxBufferedItemCount = 100 };
                var database = CosmosClient.GetDatabase("ToDoList");
                var container = database.GetContainer("Items");
                var queryText = $"SELECT * FROM c";
                if (!string.IsNullOrEmpty(Query))
                {
                    queryText += Query;
                }

                using (FeedIterator<EmptyDocument<T>> query = container.GetItemQueryIterator<EmptyDocument<T>>(
                    queryText,
                    requestOptions: options))
                {
                    while (query.HasMoreResults)
                    {
                        foreach (var item in await query.ReadNextAsync())
                        {
                            allItems.Add(item.Document);
                        }
                    }
                }

                return allItems;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR - {ex.Message}");
                return null;
            }
        }
    }
}
