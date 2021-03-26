using System;

namespace BankSystem
{
    /// <summary>
    /// Exception по возрасту
    /// </summary>
    public class AgeExceptions : ArgumentException
    {
        public AgeExceptions(string message)
            : base(message)
        { }
    }
}