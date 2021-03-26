using System;

namespace BankSystem.Legal
{
    public abstract class LegalClient : Client
    {
        /// <summary>
        /// Название
        /// </summary>
        public abstract string Name { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        public abstract DateTime DateOfBirth { get; set; }
    }
}