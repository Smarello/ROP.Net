using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROP.Net
{
    internal interface IErrorTrack<T> : ITrack<T>
    {
        T GetError();
    }
}
