﻿using System.IO;
using Soenneker.Extensions.Stream;
using Soenneker.Utils.Json;
using Soenneker.Utils.MemoryStream.Abstract;

namespace Soenneker.Extensions.Generic;

public static class GenericExtension
{
    /// <summary>
    /// Allows for feeding a stream into this (recommended via <see cref="IMemoryStreamUtil"/>), which gets serialized (JSON), and then the stream is returned
    /// </summary>
    public static System.IO.Stream ToStream<T>(this T input, System.IO.Stream stream)
    {
        JsonUtil.SerializeIntoStream(stream, input);
        stream.ToStart();
        return stream;
    }

    /// <summary>
    /// Not recommended, builds a new <see cref="MemoryStream"/> and returns that after seeking to start.
    /// </summary>
    public static System.IO.Stream ToStream<T>(this T input)
    {
        var stream = new MemoryStream();
        JsonUtil.SerializeIntoStream(stream, input);
        stream.ToStart();
        return stream;
    }
}