using System;
using System.Collections.Generic;
using BankSystem.Legal;

#nullable disable

namespace BankSystem
{
    public partial class AllLegalClient : LegalClient
    {
        #region Поля

        /// <summary>
        /// Id
        /// </summary>
        private int id;
        /// <summary>
        /// Название
        /// </summary>
        private string name;
        /// <summary>
        /// Дата создания
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
        /// Количество денег на счету
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
        /// Id
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
        /// Название
        /// </summary>
        public override string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }
        /// <summary>
        /// Дата создания
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
        /// Количество денег на счете
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

        public AllLegalClient()
        {
        }
        /// <summary>
        /// Конструктор добавление клиента
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="dateOfBirth">Дата создания</param>
        /// <param name="department">Отдел</param>
        public AllLegalClient(string name, DateTime dateOfBirth, string department)
        {
            Id = ClientsFunc.GetId(nameof(AllLegalClient));
            Name = name;
            DateOfBirth = dateOfBirth;
            Reputation = ClientsFunc.GetReputatuion();
            Department = department;
            AccountNumber = ClientsFunc.GetAccountNumber(nameof(AllLegalClient));
            AmountOfMoney = 0;
            CheckContribution = 0;
            CheckDebt = 0;
        }
    }
}
