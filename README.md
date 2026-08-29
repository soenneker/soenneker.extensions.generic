[![](https://img.shields.io/nuget/v/Soenneker.Extensions.Generic.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Extensions.Generic/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.generic/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.generic/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/Soenneker.Extensions.Generic.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Extensions.Generic/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.generic/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.generic/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.Generic

Generic extensions for serializing any value to a stream or a Base64-encoded JSON string.

## Installation

```bash
dotnet add package Soenneker.Extensions.Generic
```

## Quick start

```csharp
using Soenneker.Extensions.Generic;

var order = new { Id = 42, Status = "Ready" };

string encoded = order.ToBase64Json();
MemoryStream jsonStream = await order.ToStream();
```

`ToStream(Stream)` is also available when you want to supply and reuse the destination stream yourself.

## Available methods

- `ToStream(Stream, CancellationToken)` - Serializes the value as JSON into the supplied stream, rewinds it to position zero, and returns that same stream for reuse.
- `ToStream(CancellationToken)` - Creates a new `MemoryStream`, writes the value as JSON, rewinds it, and returns it ready to read. Dispose the returned stream when finished.
- `ToBase64Json()` - Serializes the value to UTF-8 JSON bytes and returns their Base64 representation; a null value throws `ArgumentNullException`.
