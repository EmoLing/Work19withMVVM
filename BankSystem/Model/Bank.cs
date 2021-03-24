using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using InterfacesLib;
using Microsoft.EntityFrameworkCore;
using Prism.Mvvm;

namespace BankSystem.Model
{
    public class Bank : BindableBase, IAdd, IRemove, ICredit, ITransact, IContribution
    {
        public ClientsDBContext clientsDbContext = new ClientsDBContext();

        private readonly ObservableCollection<AllLegalClient> allLegalClients = new ObservableCollection<AllLegalClient>();
        public ReadOnlyObservableCollection<AllLegalClient> AllLegalClients;

        private  readonly ObservableCollection<AllNaturalClient> allNaturalClients = new ObservableCollection<AllNaturalClient>();
        public ReadOnlyObservableCollection<AllNaturalClient> AllNaturalClients;


        private readonly ObservableCollection<AllVipLegalClient> allVipLegal = new ObservableCollection<AllVipLegalClient>();
        public ReadOnlyObservableCollection<AllVipLegalClient> AllVipLegalClients;

        private readonly ObservableCollection<AllVipNaturalClient> allVipNatural = new ObservableCollection<AllVipNaturalClient>();
        public ReadOnlyObservableCollection<AllVipNaturalClient> AllVipNaturalClients;

        public delegate void AccountHandler(object sender, AccountEventArgs e);
        public event AccountHandler Notify;

        public Bank()
        {
            clientsDbContext.AllNaturalClients.Load();
            allNaturalClients = clientsDbContext.AllNaturalClients.Local.ToObservableCollection();

            clientsDbContext.AllLegalClients.Load();
            allLegalClients = clientsDbContext.AllLegalClients.Local.ToObservableCollection();

            clientsDbContext.AllVipNaturalClients.Load();
            allVipNatural = clientsDbContext.AllVipNaturalClients.Local.ToObservableCollection();

            clientsDbContext.AllVipLegalClients.Load();
            allVipLegal = clientsDbContext.AllVipLegalClients.Local.ToObservableCollection();

            AllLegalClients = new ReadOnlyObservableCollection<AllLegalClient>(allLegalClients);
            AllNaturalClients = new ReadOnlyObservableCollection<AllNaturalClient>(allNaturalClients);
            AllVipLegalClients = new ReadOnlyObservableCollection<AllVipLegalClient>(allVipLegal);
            AllVipNaturalClients = new ReadOnlyObservableCollection<AllVipNaturalClient>(allVipNatural);

        }

        public void RemoveValue<T>(T item)
        {
            if (item is AllNaturalClient)
            {
                if (allNaturalClients.Contains(item as AllNaturalClient))
                {
                    allNaturalClients.Remove(item as AllNaturalClient);
                    clientsDbContext.SaveChanges();
                }
            }
            else if (item is AllLegalClient)
            {
                if (allLegalClients.Contains(item as AllLegalClient))
                {
                    allLegalClients.Remove(item as AllLegalClient);
                    clientsDbContext.SaveChanges();
                }
            }
            else if (item is AllVipNaturalClient)
            {
                if (allVipNatural.Contains(item as AllVipNaturalClient))
                {
                    allVipNatural.Remove(item as AllVipNaturalClient);
                    clientsDbContext.SaveChanges();
                }
            }
            else if (item is AllVipLegalClient)
            {
                if (allVipLegal.Contains(item as AllVipLegalClient))
                {
                    allVipLegal.Remove(item as AllVipLegalClient);
                    clientsDbContext.SaveChanges();
                }
            }
        }

        public void AddClient<T>(List<T> item, string depart, string type)
        {
            List<string> itemTemp = new List<string>();

            foreach (var temp in item)
            {
                itemTemp.Add(temp.ToString());
            }

            if (type == "Обычный" && depart == "Физ лицо")
            {
                allNaturalClients.Add(new AllNaturalClient(itemTemp[0],itemTemp[1],DateTime.Parse(itemTemp[2]), "Физический"));
                
            }
            else if (type == "Обычный" && depart == "Юр лицо")
            {
                allLegalClients.Add(new AllLegalClient(itemTemp[0], DateTime.Parse(itemTemp[2]), "Юридический"));
                
            }
            else if (type == "VIP" && depart == "Физ лицо")
            {
                allVipNatural.Add(new AllVipNaturalClient(itemTemp[0], itemTemp[1], DateTime.Parse(itemTemp[2]), "VIP"));
                
            }
            else if (type == "VIP" && depart == "Юр лицо")
            {
                allVipLegal.Add(new AllVipLegalClient(itemTemp[0], DateTime.Parse(itemTemp[1]), "VIP"));
                
            }
            clientsDbContext.SaveChanges();
        }

        /// <summary>
        /// Транзакция
        /// </summary>
        /// <typeparam name="TClient1">Отправитель</typeparam>
        /// <typeparam name="TClient2">Получатель</typeparam>
        /// <param name="client1">Отправитель</param>
        /// <param name="client2">Получатель</param>
        /// <param name="sum">сумма</param>
        public void Transact<TClient1>(TClient1 client1, TClient1 client2, decimal sum)
            where TClient1 : IAccount

        {
            if (client1 == null || client2 == null)
                return;
            if (client1.Equals(client2))
            {
                return;
            }

            if (!LockTransact(client1, client2, sum)) return;
            GiveMoney(client1, client2, sum);
            GetMoney(client2, client1, sum);
        }

        /// <summary>
        /// Блокировка транзакции
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client1"></param>
        /// <param name="client2"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        private bool LockTransact<T>(T client1, T client2, decimal sum)
            where T : IAccount
        {

            if ((client1.Department != client2.Department) && (sum >= 50000))
            {
                Notify?.Invoke(this, new AccountEventArgs(
                    $"{DateTime.Now} Заблокированная транзакция Транзакция: Клиент с номером счета {client1.AccountNumber} из {client1.Department} отдела  перевел " +
                    $"Клиенту с номером счета {client2.AccountNumber} из {client2.Department} отдела", sum));
                //MessageBox.Show("Перевод в другой отдел возможен только при суммах меньше 50000", "WARNING",
                //    MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }
            else return true;
        }
        /// <summary>
        /// Отправка денег
        /// </summary>
        private void GiveMoney<T>(T client1, T client2, decimal sum)
            where T : IAccount
        {
            client1.AmountOfMoney = client1.AmountOfMoney - sum;
            Notify?.Invoke(this, new AccountEventArgs($"{DateTime.Now}  Транзакция: Клиент с номером счета {client1.AccountNumber} из {client1.Department} отдела перевел" +
                                                      $" клиенту с номером счета {client2.AccountNumber} из {client2.Department} отдела", sum));
        }

        /// <summary>
        /// Получение денег
        /// </summary>
        private void GetMoney<T>(T client2, T client1, decimal sum)
            where T : IAccount
        {
            client2.AmountOfMoney = client2.AmountOfMoney + sum;
            Notify?.Invoke(this, new AccountEventArgs($"{DateTime.Now}  Транзакция: Клиент с номером счета {client2.AccountNumber} из {client2.Department} отдела получил " +
                                                      $"от клиента с номером счета {client1.AccountNumber} из отдела {client2.Department}", sum));
        }

        /// <summary>
        /// Проверка на уже созданный вклад
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool CheckTransact<T>(T item)
        {
            if (item is AllNaturalClient)
            {
                if ((item as AllNaturalClient).CheckContribution != 0)
                    return true;
                else return false;
            }
            else if (item is AllLegalClient)
            {
                if ((item as AllLegalClient).CheckContribution != 0)
                    return true;
                else return false;
            }
            else if (item is AllVipNaturalClient)
            {
                if ((item as AllVipNaturalClient).CheckContribution != 0)
                    return true;
                else return false;
            }
            else if (item is AllVipLegalClient)
            {
                if ((item as AllVipLegalClient).CheckContribution != 0)
                    return true;
                else return false;
            }
            else return true;
        }

        /// <summary>
        /// Проверка на уже созданный вклад
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool CheckCredit<T>(T item)
        {

            if (item is AllNaturalClient)
            {
                if ((item as AllNaturalClient).CheckDebt != 0)
                    return true;
                else return false;
            }
            else if (item is AllLegalClient)
            {
                if ((item as AllLegalClient).CheckDebt != 0)
                    return true;
                else return false;
            }
            else if (item is AllVipNaturalClient)
            {
                if ((item as AllVipNaturalClient).CheckDebt != 0)
                    return true;
                else return false;
            }
            else if (item is AllVipLegalClient)
            {
                if ((item as AllVipLegalClient).CheckDebt != 0)
                    return true;
                else return false;
            }
            else return true;
        }

        /// <summary>
        /// Вклад
        /// </summary>
        /// <param name="Capitalization">С капитализацией или без</param>
        /// <param name="currentDateTime">Текущая дата</param>
        /// <param name="oldDateTime">Дата создания вклада</param>
        /// <param name="sum">Сумма</param>
        /// <returns></returns>
        public void Contribution(bool Capitalization, DateTime currentDateTime, DateTime oldDateTime, decimal sum)
        {
            var a = currentDateTime.Subtract(oldDateTime).Days / (365.25 / 12);
            int month = Convert.ToInt32(a);

            Check_Contribution = sum;
            AmountOfMoney -= sum;

            if (AmountOfMoney < 0)
            {
                AmountOfMoney += sum;
                Check_Contribution = 0;
                return;
            }

            int stavka = 10;
            stavka = (reputation == "Положительная") ? 20 : 10;

            if (Capitalization)
            {
                decimal percent = stavka / 12;
                decimal percent_stavka = percent;
                for (int i = 0; i < month; i++)
                {
                    Check_Contribution += percent_stavka;
                    percent_stavka = (Check_Contribution * ((decimal)stavka / 100)) / 12;
                }
            }
            else
            {
                if (month == 12)
                    Check_Contribution += stavka;
            }

            Notify?.Invoke(this, new AccountEventArgs($"{DateTime.Now}  Открытие вклада: Клиент {this.Name} из отдела {this.Department} открыл вклад и положил на него сумму {sum} рублей"));
        }

        /// <summary>
        /// Вклад, котоырй будет показан, как предварительный
        /// </summary>
        /// <param name="Capitalization">С капитализацией или без</param>
        /// <param name="currentDateTime">Текущая дата</param>
        /// <param name="oldDateTime">Дата создания вклада</param>
        /// <param name="sum">Сумма</param>
        /// <returns></returns>
        public string Test_Contribution(bool Capitalization, DateTime currentDateTime, DateTime oldDateTime, decimal sum)
        {
            var a = currentDateTime.Subtract(oldDateTime).Days / (365.25 / 12);
            int month = Convert.ToInt32(a);

            int stavka = 12;
            stavka = (reputation == "Положительная") ? 15 : 12;

            decimal testCheck_Contribution = sum;
            if (Capitalization)
            {
                decimal percent = stavka / 12;
                decimal percent_stavka = percent;
                for (int i = 0; i < month; i++)
                {
                    testCheck_Contribution += percent_stavka;
                    percent_stavka = (testCheck_Contribution * ((decimal)stavka / 100)) / 12;
                }
            }
            else
            {
                if (month == 12)
                    testCheck_Contribution += stavka;
            }
            return $"{testCheck_Contribution,0:0.##}";
        }

        /// <summary>
        /// Закрытие вклада
        /// </summary>
        public void CloseContribution()
        {
            AmountOfMoney += Check_Contribution;
            Check_Contribution = 0;
            Notify?.Invoke(this, new AccountEventArgs($"{DateTime.Now}  Закрытие вклада: Клиент {this.Name} из отдела {this.Department} закрыл вклад"));
        }

        /// <summary>
        /// Кредит
        /// </summary>
        /// <param name="sum_credit"></param>
        /// <param name="count_month"></param>
        /// <param name="current_moth"></param>
        public void Credit(decimal sum_credit, int count_month, DateTime oldDate, DateTime currentDate)
        {

            var current_moth = currentDate.Subtract(oldDate).Days / (365.25 / 12);
            decimal credit_stavka = (reputation == "Положительная") ? (decimal)0.03 : (decimal)0.1;
            decimal first_sum = (reputation == "Положительная") ? (sum_credit / 100 * 5) : (sum_credit / 100 * 10); //Первоначальный взнос
            sum_credit -= first_sum; //Сумма кредита
            AmountOfMoney += sum_credit;

            decimal every_month_debt = sum_credit / count_month; //ежемесячный долг

            decimal Nachis_Percents = sum_credit * credit_stavka / 12; //начисленные проценты

            decimal Ostatok_Po_Credit = sum_credit - every_month_debt; //Остаток по кредиту

            Check_Debt = Ostatok_Po_Credit;
            for (int i = 2; i <= current_moth; i++)
            {
                Nachis_Percents = Ostatok_Po_Credit * credit_stavka / 12; //на второй месяц

                decimal Sum_Platezha = every_month_debt + Nachis_Percents; //Сумма платежа

                AmountOfMoney -= Sum_Platezha;

                Ostatok_Po_Credit -= every_month_debt;

                Check_Debt = Ostatok_Po_Credit;
            }
            Notify?.Invoke(this, new AccountEventArgs($"{DateTime.Now}  Получение кредита: Клиент {this.Name} из отдела {this.Department} взял кредит на сумму {sum_credit} рублей на {count_month} месяцев"));
        }

        public string TestCredit(decimal sum_credit, int count_month, DateTime oldDate, DateTime currentDate, out string OstatokPoCredit, out string firstSum)
        {
            var current_moth = currentDate.Subtract(oldDate).Days / (365.25 / 12);
            string NextSummPlatezha = string.Empty;

            decimal credit_stavka = (reputation == "Положительная") ? (decimal)0.03 : (decimal)0.08;
            decimal first_sum = (reputation == "Положительная") ? (sum_credit / 100 * 5) : (sum_credit / 100 * 10); //Первоначальный взнос
            firstSum = $"{first_sum,0:0.##}";

            sum_credit -= first_sum; //Сумма кредита

            decimal sum_ostatkov = 0;

            decimal every_month_debt = sum_credit / count_month; //ежемесячный долг

            decimal Nachis_Percents = sum_credit * credit_stavka / 12; //начисленные проценты

            decimal Ostatok_Po_Credit = sum_credit - every_month_debt; //Остаток по кредиту

            sum_ostatkov = Ostatok_Po_Credit;

            OstatokPoCredit = $"{Ostatok_Po_Credit,0:0.##}";
            for (int i = 2; i <= current_moth; i++)
            {
                Nachis_Percents = Ostatok_Po_Credit * credit_stavka / 12; //на второй месяц

                decimal Sum_Platezha = every_month_debt + Nachis_Percents; //Сумма платежа
                NextSummPlatezha = $"{Sum_Platezha,0:0.##}";

                Ostatok_Po_Credit -= every_month_debt;

                sum_ostatkov += Ostatok_Po_Credit;
                OstatokPoCredit = $"{sum_ostatkov,0:0.##}";
            }
            return NextSummPlatezha;
        }

        public void CloseCredit()
        {
            AmountOfMoney -= Check_Debt;
            Check_Debt = 0;
            Notify?.Invoke(this, new AccountEventArgs($"{DateTime.Now}  Закрытие кредита: Клиент {this.Name} из отдела {this.Department} закрыл кредит"));
        }
    }
}