namespace Tvdb.Sdk
{
    /// <summary>
    /// The api client settings.
    /// </summary>
    public class SdkClientSettings
    {
        /// <summary>
        /// Gets or sets the api base url.
        /// </summary>
        /// <remarks>
        /// Default value: "https://api4.thetvdb.com/v4/".
        /// </remarks>
        public string BaseUrl { get; set; } = "https://api4.thetvdb.com/v4/";

        /// <summary>
        /// Gets or sets the user's access token.
        /// </summary>
        public string AccessToken { get; set; }
    }
}
