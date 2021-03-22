using System;
using System.Linq;

namespace BankSystem
{
    public static class ClientsFunc
    {
        public static string GetReputatuion()
        {
            Random r = new Random();
            string reputation = "Положительная";

            if (r.Next(0, 100) < 25)
                reputation = "Отрицательная";
            return reputation;
        }

        public static int GetId(string typeClient)
        {
            ClientsDBContext context = new ClientsDBContext();
            int iD = 0;
            if (typeClient == "AllNaturalClient")
            {
                iD=context.AllNaturalClients.OrderByDescending(id => id).FirstOrDefault().Id;
            }
            else if(typeClient == "AllLegalClient")
            {
                iD = context.AllLegalClients.OrderByDescending(id => id).FirstOrDefault().Id;
            }
            else if (typeClient == "AllVipNaturalClient")
            {
                iD = context.AllVipNaturalClients.OrderByDescending(id => id).FirstOrDefault().Id;
            }
            else if (typeClient == "AllVipLegalClients")
            {
                iD = context.AllVipLegalClients.OrderByDescending(id => id).FirstOrDefault().Id;
            }
            return iD+1;
        }

        public static int GetAccountNumber(string typeClient)
        {
            ClientsDBContext context = new ClientsDBContext();
            int accountNumber = 0;
            if (typeClient == "AllNaturalClient")
            {
                accountNumber = context.AllNaturalClients.OrderByDescending(s => s).FirstOrDefault().AccountNumber;
            }
            else if (typeClient == "AllLegalClient")
            {
                accountNumber = context.AllLegalClients.OrderByDescending(s => s).FirstOrDefault().AccountNumber;
            }
            else if (typeClient == "AllVipNaturalClient")
            {
                accountNumber = context.AllVipNaturalClients.OrderByDescending(s => s).FirstOrDefault().AccountNumber;
            }
            else if (typeClient == "AllVipLegalClients")
            {
                accountNumber = context.AllVipLegalClients.OrderByDescending(s => s).FirstOrDefault().AccountNumber;
            }
            return accountNumber + 1;
        }
    }
}