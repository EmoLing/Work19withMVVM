using System;
using System.Collections.Generic;
using System.Text;

namespace InterfasesLib
{
    public interface ICredit
    {
        void Credit<T>(decimal sum_credit, int count_month, DateTime oldDate, DateTime currentDate, T client)
            where T : IAccount;

        string TestCredit<T>(decimal sum_credit, int count_month, DateTime oldDate, DateTime currentDate,
            out string OstatokPoCredit, out string firstSum, T client)
            where T : IAccount;

        void CloseCredit<T>(T client)
            where T : IAccount;
    }
}