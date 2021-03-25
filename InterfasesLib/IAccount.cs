﻿namespace InterfasesLib
{
    public interface IAccount
    {
        public decimal CheckContribution { get; set; }
        public decimal CheckDebt { get; set; }

        /// <summary>
        /// Номер счета
        /// </summary>
        public int AccountNumber { get; }

        /// <summary>
        /// Сумма денег
        /// </summary>
        public decimal AmountOfMoney { get; set; }

        public string Department { get; }

        public static decimal operator +(IAccount xAccount, decimal sum)
        {
            return xAccount.AmountOfMoney + sum;
        }

        public static decimal operator -(IAccount xAccount, decimal sum)
        {
            xAccount.AmountOfMoney = xAccount.AmountOfMoney - sum;
            return xAccount.AmountOfMoney;
        }
    }
}