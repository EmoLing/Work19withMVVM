using System;

namespace BankSystem
{
    public class AgeExceptions : ArgumentException
    {
        public AgeExceptions(string message)
            : base(message)
        { }
    }
}