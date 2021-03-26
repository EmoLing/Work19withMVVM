using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using BankSystem.Model;
using InterfasesLib;
using Prism.Commands;
using Prism.Mvvm;

namespace BankSystem.ViewModel
{
    public class ViewModelContibution : BindableBase
    {
        #region Поля

        /// <summary>
        /// Банк
        /// </summary>
        private Bank bank;
        /// <summary>
        /// Выбранные клиент
        /// </summary>
        private object selectedClient;
        /// <summary>
        /// Тестовый вклад
        /// </summary>
        private string testContribution;

        #endregion

        #region Свойства
        /// <summary>
        /// Тестовый вклад
        /// </summary>
        public string TestContribution
        {
            get
            {
                return testContribution;
            }
            set
            {
                testContribution = value;
                RaisePropertyChanged("TestContribution");
            }
        }
        #endregion

        #region Конструктор
        /// <summary>
        /// Конструктор ViewModel вклада
        /// </summary>
        /// <param name="bank">Банк</param>
        /// <param name="SelectedClient">Выбранный клиент</param>
        public ViewModelContibution(ref Bank bank, object SelectedClient)
        {
            this.bank = bank;
            selectedClient = SelectedClient;

            TestContributionCommand = new DelegateCommand<object[]>(s => TestContibution(s));
            OpenContributionCommand = new DelegateCommand<object[]>(s => OpenContribution(s));
        }
        #endregion

        #region Команды
        /// <summary>
        /// Команда тестового клада
        /// </summary>
        public DelegateCommand<object[]> TestContributionCommand { get; }
        /// <summary>
        /// Команда Открытия вклада
        /// </summary>
        public DelegateCommand<object[]> OpenContributionCommand { get; }
        #endregion

        #region Методы
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
        /// Функция тестового вклада
        /// </summary>
        /// <param name="s"></param>
        private void TestContibution(object[] s)
        {
            RadioButton capitalism = null;
            foreach (var item in s)
            {
                if (item is RadioButton && ((item as RadioButton).IsChecked == true))
                {
                    capitalism = item as RadioButton;
                }
            }

            DateTime time = DateTime.Today;
            time = time.AddMonths(-int.Parse(s[1].ToString()));

            TestContribution = this.bank.Test_Contribution((capitalism.Content == "без капитализации") ? false : true,
                DateTime.Now, time, decimal.Parse(s[0].ToString()),
                selectedClient as IAccount);
        }

        /// <summary>
        /// Функция открытия вклада
        /// </summary>
        /// <param name="s"></param>
        private void OpenContribution(object[] s)
        {
            foreach (var item in s)
            {
                if (item == "")
                {
                    MessageBox.Show("Не все введено!");
                    return;
                }
            }

            RadioButton capitalism = null;
            foreach (var item in s)
            {
                if (item is RadioButton && ((item as RadioButton).IsChecked == true))
                {
                    capitalism = item as RadioButton;
                }
            }

            DateTime time = DateTime.Today;
            time = time.AddMonths(-int.Parse(s[1].ToString()));
            this.bank.Notify += SaveToLogContrubutionMessage;
            this.bank.Contribution((capitalism.Content == "без капитализации") ? false : true,
                DateTime.Now, time, decimal.Parse(s[0].ToString()),
                selectedClient as IAccount);
            this.bank.Notify -= SaveToLogContrubutionMessage;
        }
        #endregion
    }
}