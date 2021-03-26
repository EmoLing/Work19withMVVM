using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

#nullable disable

namespace BankSystem
{
    public partial class AllNaturalClient : NaturalClient
    {
        #region Поля
        /// <summary>
        /// Id
        /// </summary>
        private int id;
        /// <summary>
        /// Имя
        /// </summary>
        private string firstName;
        /// <summary>
        /// Фамилия
        /// </summary>
        private string lastName;
        /// <summary>
        /// Дата рождения
        /// </summary>
        private DateTime dateOfBirth;
        /// <summary>
        /// Репутация
        /// </summary>
        private string reputation;
        /// <summary>
        /// Отдел
        /// </summary>
        private string department;
        /// <summary>
        /// Номер счета
        /// </summary>
        private int accountNumber;
        /// <summary>
        /// Количество денег
        /// </summary>
        private decimal amountOfMoney;
        /// <summary>
        /// Вклад
        /// </summary>
        private decimal checkContribution;
        /// <summary>
        /// Кредит
        /// </summary>
        public decimal checkDebt;
        #endregion

        #region Свойства
        /// <summary>
        /// ID
        /// </summary>
        public override int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                RaisePropertyChanged("Id");
            }
        }

        /// <summary>
        /// Имя
        /// </summary>
        public override string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                RaisePropertyChanged("FirstName");
            }
        }

        /// <summary>
        /// Фамилия
        /// </summary>
        public override string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                RaisePropertyChanged("LastName");
            }
        }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public override DateTime DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }
            set
            {
                dateOfBirth = value;
                RaisePropertyChanged("DateOfBirth");
            }
        }

        /// <summary>
        /// Репутация
        /// </summary>
        public string Reputation
        {
            get
            {
                return reputation;
            }
            set
            {
                reputation = value;
                RaisePropertyChanged("Reputation");
            }
        }

        /// <summary>
        /// Отдел
        /// </summary>
        public override string Department
        {
            get
            {
                return department;
            }
            set
            {
                department = value;
                RaisePropertyChanged("Department");
            }
        }

        /// <summary>
        /// Номер счета
        /// </summary>
        public override int AccountNumber
        {
            get
            {
                return accountNumber;
            }
            set
            {
                accountNumber = value;
                RaisePropertyChanged("AccountNumber");
            }
        }

        /// <summary>
        /// Количество денег на счету
        /// </summary>
        public override decimal AmountOfMoney
        {
            get { return amountOfMoney; }
            set
            {
                amountOfMoney = value;
                RaisePropertyChanged("AmountOfMoney");
            }
        }

        /// <summary>
        /// Вклад
        /// </summary>
        public override decimal CheckContribution
        {
            get { return checkContribution; }
            set
            {
                checkContribution = value;
                RaisePropertyChanged("CheckContribution");
            }

        }

        /// <summary>
        /// Кредит
        /// </summary>
        public override decimal CheckDebt
        {
            get
            {
                return checkDebt;
            }
            set
            {
                checkDebt = value;
                RaisePropertyChanged("CheckDebt");
            }
        }
        #endregion

        public AllNaturalClient()
        {

        }

        /// <summary>
        /// Конструктор при добавлении клиента
        /// </summary>
        /// <param name="firstName">Имя</param>
        /// <param name="lastName">Фамилия</param>
        /// <param name="dateOfBirth">Дата рождения</param>
        /// <param name="department">Отдел</param>
        public AllNaturalClient(string firstName, string lastName, DateTime dateOfBirth, string department)
        {
            Id = ClientsFunc.GetId(nameof(AllNaturalClient));
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Reputation = ClientsFunc.GetReputatuion();
            Department = department;
            AccountNumber = ClientsFunc.GetAccountNumber(nameof(AllNaturalClient));
            AmountOfMoney = 0;
            CheckContribution = 0;
            CheckDebt = 0;
        }
    }
}

