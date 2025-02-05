using System.Diagnostics.Contracts;
using System.IO;
using System.Threading;
using Soenneker.Extensions.Stream;
using Soenneker.Extensions.Task;
using Soenneker.Utils.Json;

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
}