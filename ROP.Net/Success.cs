using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROP.Net
{
    public record Success<T> : ISuccessTrack<T>
    {
      
        private readonly T result;

        public Success(T result)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            this.result = result;
        }

        public T GetResult() => result!;
        
    }
}
