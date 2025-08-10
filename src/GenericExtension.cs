using Soenneker.Extensions.Stream;
using Soenneker.Extensions.Task;
using Soenneker.Utils.Json;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text.Json;
using System.Threading;
using Soenneker.Extensions.Arrays.Bytes;
using Soenneker.Extensions.String;

namespace Soenneker.Extensions.Generic;

/// <summary>
/// A collection of useful T (generic) extension methods
/// </summary>
public static class GenericExtension
{
    /// <summary>
    /// Allows for feeding a stream into this (recommended via IMemoryStreamUtil, which gets serialized (JSON), and then the stream is returned
    /// </summary>
    [Pure]
    public static async System.Threading.Tasks.ValueTask<System.IO.Stream> ToStream<T>(this T input, System.IO.Stream stream, CancellationToken cancellationToken = default)
    {
        await JsonUtil.SerializeToStream(stream, input, cancellationToken: cancellationToken)
                      .NoSync();
        stream.ToStart();
        return stream;
    }

    /// <summary>
    /// Not recommended if you have access to IMemoryStreamUtil, builds a new <see cref="MemoryStream"/> and returns that after seeking to start.
    /// </summary>
    [Pure]
    public static async System.Threading.Tasks.ValueTask<MemoryStream> ToStream<T>(this T input, CancellationToken cancellationToken = default)
    {
        var stream = new MemoryStream();

        await JsonUtil.SerializeToStream(stream, input, cancellationToken: cancellationToken)
                      .NoSync();

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
    public static string ToBase64Json<T>(this T obj)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        byte[] bytes = JsonUtil.SerializeToUtf8Bytes(obj);

        return bytes.ToBase64String();
    }
}