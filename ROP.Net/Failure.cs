using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROP.Net
{
    public record Failure<T>(T failure) : IFailureTrack<T>
    {
        public T GetError() => failure;

    }
}
