using System;

namespace Snail.Core.Lang.Validation.Exceptions
{
    internal class ArgumentValidationException : Exception
    {
        internal ArgumentValidationException(string message)
            : base(message)
        {
        }
    }
}
