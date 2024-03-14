using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROP.Net
{
    internal record Rail<TSuccess, TFailure> : IRail<TSuccess, TFailure>
    {
        public bool IsSuccess { get; init; }

        public TSuccess? Result { get; private set; }
        public TFailure? Error { get; private set; }

        private Rail()
        { }

        public static IRail<TSuccess, TFailure> FromSuccessfulTrack(TSuccess result) =>
            new Rail<TSuccess, TFailure>
            {
                IsSuccess = true,
                Result = result,
                Error = default
            };
        

        public static IRail<TSuccess, TFailure> FromErrorTrack(TFailure error) => 
            new Rail<TSuccess, TFailure>
            {
                IsSuccess = false,
                Error = error,
                Result = default
            };
    }
}
