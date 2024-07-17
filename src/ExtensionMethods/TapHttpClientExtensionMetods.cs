using Tap.NetCore.HttpClient;
using Tap.NetCore.Service.Models;
using Tap.NetCore.HttpClient.ExtensionMethods;

namespace Tap.NetCore.Service.ExtensionMethods;

/// <summary>
/// Provides extension methods for the <see cref="TapHttpClient"/> class.
/// </summary>
public static class TapHttpClientExtensionMethods
{
    /// <summary>
    /// Sends a GET request to the specified endpoint and deserializes the content of the response into the specified type.
    /// </summary>
    /// <typeparam name="T">The type to which the content of the response is deserialized.</typeparam>
    /// <param name="client">The <see cref="TapHttpClient"/> instance.</param>
    /// <param name="endpoint">The endpoint to which the GET request is sent.</param>
    /// <param name="accessToken">The access token for authentication, if required.</param>
    /// <param name="args">The query parameters to include in the GET request.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="TapHttpResponseMessage{T}"/>.</returns>
    public static async Task<TapHttpResponseMessage<T>> GetContentAsync<T>(
        this TapHttpClient client,
        string endpoint,
        IDictionary<string, string> args = null)
    {
        var response = await client.GetAsync(endpoint, args, HttpCompletionOption.ResponseContentRead);

        return new TapHttpResponseMessage<T>(response);
    }
}