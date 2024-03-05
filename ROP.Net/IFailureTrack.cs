using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROP.Net
{
    public interface IFailureTrack<T> : ITrack<T>
    {
        T GetError();
    }
}
