using System.Text.Json.Serialization.Metadata;
using Soenneker.Extensions.Stream;
using Soenneker.Extensions.Task;
using Soenneker.Utils.Json;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text.Json;
using System.Threading;
using Soenneker.Extensions.Arrays.Bytes;

namespace Soenneker.Extensions.Generic;

/// <summary>
/// A collection of useful T (generic) extension methods
/// </summary>
public static class GenericExtension
{
    /// <summary>
    /// Allows for feeding a stream into this (recommended via IMemoryStreamUtil, which gets serialized (JSON), and then the stream is returned
    /// </summary>
    /// <returns>Allows for feeding a stream into this (recommended via IMemoryStreamUtil, which gets serialized (JSON), and then the stream is returned.</returns>
    /// <param name="input">The value to serialize.</param>
    /// <param name="stream">The destination stream, reset before writing.</param>
    /// <param name="typeInfo">Source-generated JSON metadata and serialization options for the value.</param>
    /// <param name="cancellationToken">Cancels serialization.</param>
    [Pure]
    public static async System.Threading.Tasks.ValueTask<System.IO.Stream> ToStream<T>(this T input, System.IO.Stream stream, JsonTypeInfo<T> typeInfo, CancellationToken cancellationToken = default)
    {
        stream.SetLength(0);
        stream.Position = 0;

        await JsonUtil.SerializeToStream(stream, input, typeInfo, cancellationToken: cancellationToken)
                      .NoSync();
        stream.ToStart();
        return stream;
    }

    /// <summary>
    /// Not recommended if you have access to IMemoryStreamUtil, builds a new <see cref="MemoryStream"/> and returns that after seeking to start.
    /// </summary>
    /// <returns>Not recommended if you have access to IMemoryStreamUtil, builds a new <see cref="MemoryStream"/> and returns that after seeking to start.</returns>
    /// <param name="input">The value to serialize.</param>
    /// <param name="typeInfo">Source-generated JSON metadata and serialization options for the value.</param>
    /// <param name="cancellationToken">Cancels serialization.</param>
    [Pure]
    public static async System.Threading.Tasks.ValueTask<MemoryStream> ToStream<T>(this T input, JsonTypeInfo<T> typeInfo, CancellationToken cancellationToken = default)
    {
        var stream = new MemoryStream();

        try
        {
            await JsonUtil.SerializeToStream(stream, input, typeInfo, cancellationToken: cancellationToken)
                          .NoSync();
        }
        catch
        {
            await stream.DisposeAsync().ConfigureAwait(false);
            throw;
        }

        stream.ToStart();
        return stream;
    }

    /// <summary>
    /// Serializes an object to JSON and encodes it as a Base64 string.
    /// </summary>
    /// <typeparam name="T">
    /// The type of object to serialize.
    /// </typeparam>
    /// <param name="obj">
    /// The object to serialize and encode. Cannot be null.
    /// </param>
    /// <returns>
    /// A Base64-encoded string representing the serialized JSON form of the object.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="obj"/> is null.
    /// </exception>
    /// <exception cref="JsonException">
    /// Thrown if the object cannot be serialized to JSON.
    /// </exception>
    /// <param name="typeInfo">Source-generated JSON metadata and serialization options for the value.</param>
    public static string ToBase64Json<T>(this T obj, JsonTypeInfo<T> typeInfo)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        byte[] bytes = JsonUtil.SerializeToUtf8Bytes(obj, typeInfo);

        return bytes.ToBase64String();
    }
}
