using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using BankSystem.Model;
using BankSystem.View;
using InterfasesLib;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace BankSystem.ViewModel
{
    public class ViewModelBank : BindableBase
    {
        /// <summary>
        /// Банк
        /// </summary>
        private Model.Bank bank = new Bank();
        /// <summary>
        /// Выбранный клиент
        /// </summary>
        private object selectedBank;
        /// <summary>
        /// Выбранный клиент
        /// </summary>
        public object SelectedBank
        {
            get { return selectedBank; }
            set
            {
                selectedBank = value;
                RaisePropertyChanged("SelectedBank");
            }
        }
        public ViewModelBank()
        {
            bank.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };

            RemoveCommand = new DelegateCommand<object>(i => RemoveValue(i));
            EditCommand = new DelegateCommand(()=> SaveChages());
            AddCommand = new DelegateCommand(() => AddClient());
            TransactCommand = new DelegateCommand<object>(i => Transact(i));
            OpenContibutionCommand = new DelegateCommand<object>(s => OpenContribution(s));
            CloseContibutionCommand = new DelegateCommand<object>(s => CloseContribution(s));
            OpenCreditCommand = new DelegateCommand<object>((s) => OpenCredit(s));
            CloseCreditCommand = new DelegateCommand<object>(s => CloseCredit(s));
        }

        #region Методы сохранения
        /// <summary>
        /// Сохранение операций по вкладу в ЛОГ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToLogContrubutionMessage(object sender, AccountEventArgs e)
        {
            string path = $"logs/{DateTime.Now.ToShortDateString()}_log_Contribution.txt";
            DirectoryInfo directoryInfo = new DirectoryInfo("logs");
            if (directoryInfo.Exists == false)
                Directory.CreateDirectory("logs");
            using (StreamWriter streamWriter = new StreamWriter(path, true))
            {
                streamWriter.AutoFlush = true;
                streamWriter.WriteLine(e.Message);
            }
            FileInfo fileInfo = new FileInfo(path);
            MessageBox.Show($"Запись была записана в log расположенный по пути: \n {fileInfo.FullName}",
                "Complete!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Сохранение операций по кредиту в ЛОГ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToLogCreditMessage(object sender, AccountEventArgs e)
        {
            string path = $"logs/{DateTime.Now.ToShortDateString()}_log_Credit.txt";
            DirectoryInfo directoryInfo = new DirectoryInfo("logs");
            if (directoryInfo.Exists == false)
                Directory.CreateDirectory("logs");
            using (StreamWriter streamWriter = new StreamWriter(path, true))
            {
                streamWriter.AutoFlush = true;
                streamWriter.WriteLine(e.Message);
            }
            FileInfo fileInfo = new FileInfo(path);
            MessageBox.Show($"Запись была записана в log расположенный по пути: \n {fileInfo.FullName}",
                "Complete!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Сохранение в бд
        /// </summary>
        public void SaveChages()
        {
            bank.clientsDbContext.SaveChanges();
        }
        #endregion

        #region Команды
        /// <summary>
        /// Команда добавления клиента
        /// </summary>
        public DelegateCommand AddCommand { get; }
        /// <summary>
        /// Команда Удаления
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; }
        /// <summary>
        /// Команда редактирования
        /// </summary>
        public DelegateCommand EditCommand { get; }
        /// <summary>
        /// Команда транзакции
        /// </summary>
        public DelegateCommand<object> TransactCommand { get; }
        /// <summary>
        /// Команда открытия вклада
        /// </summary>
        public DelegateCommand<object> OpenContibutionCommand { get; }
        /// <summary>
        /// Команда закрытия вклада
        /// </summary>
        public DelegateCommand<object> CloseContibutionCommand { get; }
        /// <summary>
        /// Команда открытия кредита
        /// </summary>
        public DelegateCommand<object> OpenCreditCommand { get; }
        /// <summary>
        /// Команда закрытия кредита
        /// </summary>
        public DelegateCommand<object> CloseCreditCommand { get; }
        #endregion

        #region Коллекции отображения на View
        /// <summary>
        /// Коллекция Юр лиц
        /// </summary>
        public ReadOnlyObservableCollection<AllLegalClient> AllLegalClients => bank.AllLegalClients;
        /// <summary>
        /// Коллекция физ.лиц
        /// </summary>
        public ReadOnlyObservableCollection<AllNaturalClient> AllNaturalClients => bank.AllNaturalClients;
        /// <summary>
        /// Коллекция Вип юр лиц
        /// </summary>
        public ReadOnlyObservableCollection<AllVipLegalClient> AllVipLegalClients => bank.AllVipLegalClients;
        /// <summary>
        /// Коллекция Вип физ. лиц
        /// </summary>
        public ReadOnlyObservableCollection<AllVipNaturalClient> AllVipNaturalClients => bank.AllVipNaturalClients;
        #endregion

        #region Методы для команд

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="i">Выбранный клиент</param>
        private void RemoveValue(object i)
        {
            if (i != null)
                bank.RemoveValue(i);
        }
        /// <summary>
        /// Добавление
        /// </summary>
        private void AddClient()
        {
            AddClientViewModel addClientViewModel = new AddClientViewModel(bank);
            AddClientWindow addClientWindow = new AddClientWindow(addClientViewModel);
            addClientWindow.Show();
        }
        /// <summary>
        /// Транзакция
        /// </summary>
        /// <param name="i">Выбранный клиент (отправитель)</param>
        private void Transact(object i)
        {
            if (i == null)
            {
                MessageBox.Show("ВЫБЕРИТЕ КЛИЕНТА!!!!");
            }
            else
            {
                TransactViewModel viewModel = new TransactViewModel(ref bank, i);
                TransactWindow transactWindow = new TransactWindow(viewModel);

                if (transactWindow.ShowDialog() == true)
                {
                    transactWindow.Show();
                    transactWindow.Activate();
                }
                SaveChages();
            }
        }
        /// <summary>
        /// Открытие вклада
        /// </summary>
        /// <param name="s">Выбранный клиент</param>
        private void OpenContribution(object s)
        {
            if (s == null)
            {
                MessageBox.Show("ВЫБЕРИТЕ КЛИЕНТА!!!!");
            }
            else
            {
                ViewModelContibution viewModel = new ViewModelContibution(ref bank, s);
                ContributionWindow contibutionWindow = new ContributionWindow(viewModel);

                if (contibutionWindow.ShowDialog() == true)
                {
                    contibutionWindow.Show();
                    contibutionWindow.Activate();
                }
                SaveChages();
            }
        }
        /// <summary>
        /// Закрытие вклада
        /// </summary>
        /// <param name="s">Выбранный клиент</param>
        private void CloseContribution(object s)
        {
            if (bank.CheckTransact(s))
            {
                bank.Notify += SaveToLogContrubutionMessage;
                bank.CloseContribution(s as IAccount);
                bank.Notify -= SaveToLogContrubutionMessage;
            }
            else
            {
                MessageBox.Show("НЕТУ ОТКРЫТОГО ВКЛАДА");
            }
        }
        /// <summary>
        /// Открытие кредита
        /// </summary>
        /// <param name="s">Выбранный клиент</param>
        private void OpenCredit(object s)
        {
            if (s == null)
            {
                MessageBox.Show("ВЫБЕРИТЕ КЛИЕНТА!!!!");
            }
            else
            {
                ViewModelCredit viewModel = new ViewModelCredit(ref bank, s);
                CreditView creditView = new CreditView(viewModel);

                if (creditView.ShowDialog() == true)
                {
                    creditView.Show();
                    creditView.Activate();
                }

                SaveChages();
            }
        }
        /// <summary>
        /// Закрытие кредита
        /// </summary>
        /// <param name="s">Выбранный клиент</param>
        private void CloseCredit(object s)
        {
            if (bank.CheckCredit(s))
            {
                bank.Notify += SaveToLogContrubutionMessage;
                bank.CloseCredit(s as IAccount);
                bank.Notify -= SaveToLogContrubutionMessage;
            }
            else
            {
                MessageBox.Show("НЕТУ ОТКРЫТОГО ВКЛАДА");
            }
        }
        #endregion
    }
}