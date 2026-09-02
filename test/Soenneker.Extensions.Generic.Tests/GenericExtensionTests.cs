using System.IO;
using System.Text;
using System.Threading;
using AwesomeAssertions;
using Soenneker.Tests.Unit;

namespace Soenneker.Extensions.Generic.Tests;

public class GenericExtensionTests : UnitTest
{
    [Test]
    public void Default()
    {

    }

    [Test]
    public async System.Threading.Tasks.Task ToStream_replaces_existing_stream_content(CancellationToken cancellationToken)
    {
        var stream = new MemoryStream(Encoding.UTF8.GetBytes("this is stale content that must not remain"));

        System.IO.Stream result = await new { Id = 1 }.ToStream(stream, cancellationToken);

        result.Should().BeSameAs(stream);
        result.Position.Should().Be(0);

        using var reader = new StreamReader(result, Encoding.UTF8, leaveOpen: true);
        (await reader.ReadToEndAsync()).Should().Be("{\"id\":1}");
    }
}
