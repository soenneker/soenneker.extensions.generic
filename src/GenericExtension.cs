using System.Diagnostics.CodeAnalysis;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.CompilerServices;
using Soenneker.Extensions.Stream;
using Soenneker.Utils.Json;
using Soenneker.Utils.MemoryStream.Abstract;

namespace Soenneker.Extensions.Generic;

/// <summary>
/// A collection of useful T (generic) extension methods
/// </summary>
public static class GenericExtension
{
    /// <summary>
    /// Allows for feeding a stream into this (recommended via <see cref="IMemoryStreamUtil"/>), which gets serialized (JSON), and then the stream is returned
    /// </summary>
    [Pure]
    public static System.IO.Stream ToStream<T>(this T input, System.IO.Stream stream)
    {
        JsonUtil.SerializeIntoStream(stream, input);
        stream.ToStart();
        return stream;
    }

    /// <summary>
    /// Not recommended, builds a new <see cref="MemoryStream"/> and returns that after seeking to start.
    /// </summary>
    [Pure]
    public static System.IO.Stream ToStream<T>(this T input)
    {
        var stream = new MemoryStream();
        JsonUtil.SerializeIntoStream(stream, input);
        stream.ToStart();
        return stream;
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if the input object is null.
    /// </summary>
    /// <param name="input">The input object.</param>
    /// <param name="name">The name of the calling member.</param>
    /// <exception cref="ArgumentNullException">Thrown when the input object is null.</exception>
    public static void ThrowIfNull<T>([NotNull] this T? input, [CallerMemberName] string? name = null)
    {
        if (input == null)
            throw new ArgumentNullException(name);
    }
}