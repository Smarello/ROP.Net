using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROP.Net
{
    public record Success<T>(T result) : ISuccessTrack<T>
    {
        public T GetResult() => result;
        
    }
}
