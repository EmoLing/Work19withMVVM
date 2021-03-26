using System;
using System.Diagnostics.Eventing.Reader;
using System.Windows;
using BankSystem.Model;
using InterfasesLib;
using Prism.Commands;
using Prism.Mvvm;

namespace BankSystem.ViewModel
{
    public class TransactViewModel : BindableBase
    {
        private Bank bank;
        private string client;
        private object selectedClient;

        /// <summary>
        /// Получатель
        /// </summary>
        public string Client
        {
            get { return client; }
            set
            {
                client = value;
                RaisePropertyChanged("Client");
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="bank">банк</param>
        /// <param name="SelectedClient">Выбранный клиент</param>
        public TransactViewModel(ref Bank bank, object SelectedClient)
        {
            this.bank = bank;

            FindClientCommand = new DelegateCommand<object[]>(s => FildClient(s));
            TransactCommand = new DelegateCommand<string>(s => MessageBox.Show(this.bank.Transact(SelectedClient as IAccount, selectedClient as IAccount, Convert.ToDecimal(s))));
            CloseCommand = new DelegateCommand<Window>(window => window.Close());
            LoadedCommand = new DelegateCommand(() => this.bank.Notify += Bank.SaveToLogMessage);
            UnLoadedCommand = new DelegateCommand(() => this.bank.Notify -= Bank.SaveToLogMessage);
        }

        #region Команды
        /// <summary>
        /// Команда транзакции
        /// </summary>
        public DelegateCommand<string> TransactCommand { get; set; }
        /// <summary>
        /// Поиск клиента
        /// </summary>
        public DelegateCommand<object[]> FindClientCommand { get; set; }
        /// <summary>
        /// Закрытие окна
        /// </summary>
        public DelegateCommand<Window> CloseCommand { get; }
        /// <summary>
        /// Загрузка окна
        /// </summary>
        public DelegateCommand LoadedCommand { get; }
        /// <summary>
        /// Выгрузка окна
        /// </summary>
        public DelegateCommand UnLoadedCommand { get; }
        #endregion

        #region Методы
        /// <summary>
        /// Поиск клиента
        /// </summary>
        /// <param name="s"></param>
        private void FildClient(object[] s)
        {
            string accountNumber = s[1].ToString();
            string department = s[0].ToString();

            if (!string.IsNullOrEmpty(accountNumber) && !string.IsNullOrEmpty(department))
            {
                if (department == "Физический")
                {
                    selectedClient = bank.Find<AllNaturalClient>(int.Parse(accountNumber), department);
                    try
                    {
                        Client = (selectedClient == null) ? "НЕ НАЙДЕН" :
                            ((selectedClient as AllNaturalClient).FirstName + " " + (selectedClient as AllNaturalClient).LastName);
                    }
                    catch (NullReferenceException)
                    {
                        Client = "НЕ НАЙДЕН";
                    }
                }
                else if (department == "Юридический")
                {
                    selectedClient = bank.Find<AllLegalClient>(int.Parse(accountNumber), department);
                    try
                    {
                        Client = (selectedClient == null) ? "НЕ НАЙДЕН" :
                            ((selectedClient as AllLegalClient).Name);
                    }
                    catch (NullReferenceException)
                    {
                        Client = "НЕ НАЙДЕН";
                    }
                }
                if (department == "VIP физ")
                {
                    selectedClient = bank.Find<AllVipNaturalClient>(int.Parse(accountNumber), department);
                    try
                    {
                        Client = (selectedClient == null) ? "НЕ НАЙДЕН" :
                            ((selectedClient as AllVipNaturalClient).FirstName + " " + (selectedClient as AllVipNaturalClient).LastName);
                    }
                    catch (NullReferenceException)
                    {
                        Client = "НЕ НАЙДЕН";
                    }
                }
                else if (department == "VIP юр")
                {
                    selectedClient = bank.Find<AllVipLegalClient>(int.Parse(accountNumber), department);
                    try
                    {
                        Client = (selectedClient == null) ? "НЕ НАЙДЕН" :
                            ((selectedClient as AllVipLegalClient).Name);
                    }
                    catch (NullReferenceException)
                    {
                        Client = "НЕ НАЙДЕН";
                    }
                }
            }
        }
        #endregion
    }
}