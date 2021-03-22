using System;

namespace BankSystem.Legal
{
    public abstract class LegalClient : Client
    {
        public abstract string Name { get; set; }
        public abstract DateTime DateOfBirth { get; set; }
    }
}