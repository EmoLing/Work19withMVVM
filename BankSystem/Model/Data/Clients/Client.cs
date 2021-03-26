using InterfasesLib;
using Prism.Mvvm;

namespace BankSystem
{
    public abstract class Client : BindableBase,IAccount
    {
        /// <summary>
        /// Id
        /// </summary>
        public abstract int Id { get; set; }
        /// <summary>
        /// Отдел
        /// </summary>
        public abstract string Department { get; set; }
        /// <summary>
        /// Номер счета
        /// </summary>
        public abstract int AccountNumber { get; set; }
        /// <summary>
        /// Количество денег на счету
        /// </summary>
        public abstract decimal AmountOfMoney { get; set; }
        /// <summary>
        /// Вклад
        /// </summary>
        public abstract decimal CheckContribution { get; set; }
        /// <summary>
        /// Кредит
        /// </summary>
        public abstract decimal CheckDebt { get; set; }
    }
}