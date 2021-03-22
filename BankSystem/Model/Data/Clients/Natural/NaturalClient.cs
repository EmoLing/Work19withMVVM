using System;

namespace BankSystem
{
    public abstract class NaturalClient : Client
    {
        public abstract string FirstName { get; set; }
        public abstract string LastName { get; set; }
        public abstract DateTime DateOfBirth { get; set; }
    }
}