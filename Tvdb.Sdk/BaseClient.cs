using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tvdb.Sdk
{
    /// <summary>
    /// The base client.
    /// </summary>
    public class BaseClient
    {
        private readonly SdkClientSettings _clientState;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseClient"/> class.
        /// </summary>
        /// <param name="clientState">The sdk client state.</param>
        public BaseClient(SdkClientSettings clientState)
        {
            _clientState = clientState;
        }

        /// <summary>
        /// Prepare the request.
        /// </summary>
        /// <remarks>
        /// Required for generated clients.
        /// </remarks>
        /// <param name="client">The http client.</param>
        /// <param name="request">The http request message.</param>
        /// <param name="url">The request url.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The Task.</returns>
        [SuppressMessage("Usage", "CA1801", Justification = "Required for generated code", Scope = "client")]
        [SuppressMessage("Usage", "CA1801", Justification = "Required for generated code", Scope = "cancellationToken")]
        protected Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, StringBuilder url, CancellationToken cancellationToken)
        {
            // Insert baseurl into full url.
            if (!string.IsNullOrEmpty(_clientState.BaseUrl))
            {
                url.Insert(0, _clientState.BaseUrl.TrimEnd('/') + '/');
            }

            if (!string.IsNullOrEmpty(_clientState.AccessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _clientState.AccessToken);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Prepare the request.
        /// </summary>
        /// <remarks>
        /// Required for generated clients.
        /// </remarks>
        /// <param name="client">The http client.</param>
        /// <param name="request">The http request message.</param>
        /// <param name="url">The request url.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The Task.</returns>
        [SuppressMessage("Usage", "CA1801", Justification = "Required for generated code", Scope = "client")]
        [SuppressMessage("Usage", "CA1801", Justification = "Required for generated code", Scope = "request")]
        [SuppressMessage("Usage", "CA1801", Justification = "Required for generated code", Scope = "url")]
        [SuppressMessage("Usage", "CA1801", Justification = "Required for generated code", Scope = "cancellationToken")]
        protected Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, string url, CancellationToken cancellationToken)
        {
            // Required for generated api.
            return Task.CompletedTask;
        }

        /// <summary>
        /// Process response message.
        /// </summary>
        /// <remarks>
        /// Required for generated clients.
        /// </remarks>
        /// <param name="client">The http client.</param>
        /// <param name="response">The http response message.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The Task.</returns>
        [SuppressMessage("Usage", "CA1801", Justification = "Required for generated code", Scope = "client")]
        [SuppressMessage("Usage", "CA1801", Justification = "Required for generated code", Scope = "response")]
        [SuppressMessage("Usage", "CA1801", Justification = "Required for generated code", Scope = "cancellationToken")]
        protected Task ProcessResponseAsync(HttpClient client, HttpResponseMessage response, CancellationToken cancellationToken)
        {
            // Required for generated api.
            return Task.CompletedTask;
        }
    }
}
