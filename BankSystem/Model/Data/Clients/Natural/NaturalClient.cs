using System;

namespace BankSystem
{
    public abstract class NaturalClient : Client
    {
        /// <summary>
        /// Имя
        /// </summary>
        public abstract string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public abstract string LastName { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public abstract DateTime DateOfBirth { get; set; }
    }
}