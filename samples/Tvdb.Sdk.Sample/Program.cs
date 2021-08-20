using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Tvdb.Sdk.Sample
{
    internal static class Program
    {
        private static async Task Main()
        {
            string? apiKey, pin;
            do
            {
                Console.Write("v4 Api Key: ");
                apiKey = Console.ReadLine()?.Trim();
            }
            while (string.IsNullOrEmpty(apiKey));

            do
            {
                Console.Write("Subscriber Pin: ");
                pin = Console.ReadLine()?.Trim();
            }
            while (string.IsNullOrEmpty(pin));

            var serviceProvider = ConfigureServices();

            // Authenticate.
            var loginClient = serviceProvider.GetRequiredService<ILoginClient>();
            var sdkClientSettings = serviceProvider.GetRequiredService<SdkClientSettings>();
            try
            {
                var loginResponse = await loginClient.LoginAsync(new Body
                {
                    Apikey = apiKey,
                    Pin = pin
                }).ConfigureAwait(false);

                // Store access token.
                sdkClientSettings.AccessToken = loginResponse.Data.Token;
            }
            catch (LoginException ex)
            {
                await Console.Error.WriteLineAsync(ex.Message)
                    .ConfigureAwait(false);
                return;
            }

            // Begin searching.
            var searchClient = serviceProvider.GetRequiredService<ISearchClient>();
            while (true)
            {
                string? searchTerm;
                do
                {
                    Console.Write("Search Term: ");
                    searchTerm = Console.ReadLine()?.Trim();
                }
                while (string.IsNullOrEmpty(searchTerm));

                try
                {
                    var searchResult = await searchClient.GetSearchResultsAsync(query: searchTerm)
                        .ConfigureAwait(false);
                    if (searchResult is null)
                    {
                        Console.WriteLine("No results found");
                    }
                    else
                    {
                        Console.WriteLine("Results:");
                        foreach (var result in searchResult.Data)
                        {
                            Console.Write("\t" + $"{result.Name}");
                            if (!string.IsNullOrEmpty(result.Year))
                            {
                                Console.Write($" ({result.Year})");
                            }

                            Console.WriteLine();
                        }
                    }
                }
                catch (SeriesException ex)
                {
                    await Console.Error.WriteLineAsync(ex.Message)
                        .ConfigureAwait(false);
                }
            }
        }

        private static ServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            // Configured handler to allow compression and utf8 in headers.
            static HttpMessageHandler DefaultHttpClientHandlerDelegate(IServiceProvider service)
                => new SocketsHttpHandler
                {
                    AutomaticDecompression = DecompressionMethods.All,
                    RequestHeaderEncodingSelector = (_, _) => Encoding.UTF8
                };

            // Register SDK settings as singleton.
            serviceCollection.AddSingleton<SdkClientSettings>();

            // Register LoginClient.
            serviceCollection.AddHttpClient<ILoginClient, LoginClient>()
                .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate);

            // Register SearchClient.
            serviceCollection.AddHttpClient<ISearchClient, SearchClient>()
                .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate);

            return serviceCollection.BuildServiceProvider();
        }
    }
}
