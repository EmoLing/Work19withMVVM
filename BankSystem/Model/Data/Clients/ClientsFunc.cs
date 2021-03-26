using System;
using System.Linq;

namespace BankSystem
{
    public static class ClientsFunc
    {
        /// <summary>
        /// Получение репутации
        /// </summary>
        /// <returns></returns>
        public static string GetReputatuion()
        {
            Random r = new Random();
            string reputation = "Положительная";

            if (r.Next(0, 100) < 25)
                reputation = "Отрицательная";
            return reputation;
        }

        /// <summary>
        /// Получение Id
        /// </summary>
        /// <param name="typeClient"></param>
        /// <returns></returns>
        public static int GetId(string typeClient)
        {
            ClientsDBContext context = new ClientsDBContext();
            int iD = 0;
            try
            {
                if ((typeClient == "AllNaturalClient") && (context.AllNaturalClients.Count() != 0))
                {
                    iD = context.AllNaturalClients.OrderByDescending(id => id).FirstOrDefault().Id;
                }
                else if ((typeClient == "AllLegalClient") && (context.AllLegalClients.Count() != 0))
                {
                    iD = context.AllLegalClients.OrderByDescending(id => id).FirstOrDefault().Id;
                }
                else if ((typeClient == "AllVipNaturalClient") && (context.AllVipNaturalClients.Count() != 0))
                {
                    iD = context.AllVipNaturalClients.OrderByDescending(id => id).FirstOrDefault().Id;
                }
                else if ((typeClient == "AllVipLegalClients") && (context.AllVipLegalClients.Count() != 0))
                {
                    iD = context.AllVipLegalClients.OrderByDescending(id => id).FirstOrDefault().Id;
                }
            }
            catch (NullReferenceException ex)
            {
                iD = 0;
            }
            return ++iD;
        }

        /// <summary>
        /// Получение Номера счета
        /// </summary>
        /// <param name="typeClient"></param>
        /// <returns></returns>
        public static int GetAccountNumber(string typeClient)
        {
            ClientsDBContext context = new ClientsDBContext();
            int accountNumber = 1000;
            try
            {
                if ((typeClient == "AllNaturalClient") && (context.AllNaturalClients.Count() != 0))
                {
                    accountNumber = context.AllNaturalClients.OrderByDescending(accountNumber => accountNumber).FirstOrDefault().AccountNumber;
                }
                else if ((typeClient == "AllLegalClient") && (context.AllLegalClients.Count() != 0))
                {
                    accountNumber = context.AllLegalClients.OrderByDescending(accountNumber => accountNumber).FirstOrDefault().AccountNumber;
                }
                else if ((typeClient == "AllVipNaturalClient") && (context.AllVipNaturalClients.Count() != 0))
                {
                    accountNumber = context.AllVipNaturalClients.OrderByDescending(accountNumber => accountNumber).FirstOrDefault().AccountNumber;
                }
                else if ((typeClient == "AllVipLegalClients") && (context.AllVipLegalClients.Count() != 0))
                {
                    accountNumber = context.AllVipLegalClients.OrderByDescending(accountNumber => accountNumber).FirstOrDefault().AccountNumber;
                }
            }
            catch (NullReferenceException)
            {
                accountNumber = 1000;
            }
            return accountNumber + 1;
        }
    }
}