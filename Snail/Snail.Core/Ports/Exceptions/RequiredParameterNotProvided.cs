using System;

namespace Snail.Core.Ports.Exceptions
{
    public class RequiredParameterNotProvided : Exception
    {
        public RequiredParameterNotProvided(string message)
            : base(message)
        {}
    }
}
