using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soenneker.Extensions.Generic
{
    public static class GenericExtension
    {
        public static Stream ToStream<T>(this T input)
        {
            MemoryStream streamPayload = _memoryStreamUtil.GetSync();
            _systemTextJsonSerializer.Serialize(streamPayload, input, typeof(T), default);
            streamPayload.ToStart(); // Seek because Position set directly will lose the buffer: https://stackoverflow.com/a/71596118
            return streamPayload;
        }
    }
}
