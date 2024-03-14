using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROP.Net
{
    public interface IRail<TSuccess, TFailure>
    {
        bool IsSuccess { get; init; }
        TSuccess? Result{ get; }        
        TFailure? Error { get; }
    }
    
}
