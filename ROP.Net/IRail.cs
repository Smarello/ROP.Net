using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROP.Net
{
    public interface IRail<TSuccess, TError>
    {
        bool IsSuccess { get; }
        ITrack<TSuccess> Success { get; }
        ITrack<TError> Error { get; }
    }
    
}
