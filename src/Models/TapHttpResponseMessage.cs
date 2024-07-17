using System.Text.Json;
using System.Net.Http.Headers;

namespace Tap.NetCore.Service.Models;

/// <summary>
/// Represents a strongly-typed HTTP response message with additional metadata.
/// </summary>
/// <typeparam name="T">The type of the content contained in the HTTP response.</typeparam>
public class TapHttpResponseMessage<T> : HttpResponseMessage
{
    /// <summary>
    /// Gets the content of the HTTP response as a strongly-typed object.
    /// </summary>
    public new T Content { get; init; }

    /// <summary>
    /// Gets the headers of the HTTP response.
    /// </summary>
    public new HttpResponseHeaders Headers { get; init; }

    /// <summary>
    /// Gets the trailing headers of the HTTP response.
    /// </summary>
    public new HttpResponseHeaders TrailingHeaders { get; init; }

    /// <summary>
    /// Gets a value that indicates if the HTTP response was successful.
    /// </summary>
    public new bool IsSuccessStatusCode { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TapHttpResponseMessage{T}"/> class using an existing <see cref="HttpResponseMessage"/>.
    /// </summary>
    /// <param name="message">The original HTTP response message.</param>
    public TapHttpResponseMessage(HttpResponseMessage message)
    {
        T content;

        if (message.IsSuccessStatusCode)
        {
            content = JsonSerializer.Deserialize<T>(
                message.Content.ReadAsStringAsync().GetAwaiter().GetResult()
            );
        }
        else
        {
            content = default;
        }

        ReasonPhrase = message.ReasonPhrase;
        Content = content;
        Headers = message.Headers;
        RequestMessage = message.RequestMessage;
        StatusCode = message.StatusCode;
        IsSuccessStatusCode = message.IsSuccessStatusCode;
        TrailingHeaders = message.TrailingHeaders;
        Version = message.Version;
    }
}
