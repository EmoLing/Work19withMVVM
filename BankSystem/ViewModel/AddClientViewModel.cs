using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BankSystem.Model;
using Prism.Commands;
using Prism.Mvvm;

namespace BankSystem.ViewModel
{
    public class AddClientViewModel : BindableBase
    {
        #region Поля
        private Bank bank;
        private string date;
        private string createDate;
        private bool stateButton;
        #endregion

        #region Свойства
        /// <summary>
        /// дата рождения
        /// </summary>
        public string Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                RaisePropertyChanged("Date");
            }
        }
        /// <summary>
        /// Дата создания
        /// </summary>
        public string DateCreate
        {
            get
            {
                return createDate;
            }
            set
            {
                createDate = value;
                RaisePropertyChanged("DateCreate");
            }
        }
        /// <summary>
        /// Состояние кнопки
        /// </summary>
        public bool StateButton
        {
            get
            {
                return stateButton;
            }
            set
            {
                stateButton = value;
                RaisePropertyChanged("StateButton");
            }
        }
        #endregion

        #region Конструктор
        public AddClientViewModel(Bank bank)
        {
            this.bank = bank;
            AddCommand = new DelegateCommand<object[]>((i) => AddClientCommand(i));
            TextIsDateCommand = new DelegateCommand<object>(box => TestDate(box));
            BlockChechCommand = new DelegateCommand<Object[]>(s => BlockCheck(s));
            UnBlockChechCommand = new DelegateCommand<Object[]>(s => UnblockCheck(s));
        }
        #endregion

        #region Команды
        /// <summary>
        /// Команда добавления
        /// </summary>
        public DelegateCommand<object[]> AddCommand { get; }
        /// <summary>
        /// Команда проверки на дату
        /// </summary>
        public DelegateCommand<object> TextIsDateCommand { get; set; }
        /// <summary>
        /// Команда переключения на Check
        /// </summary>
        public DelegateCommand<Object[]> BlockChechCommand { get; set; }
        /// <summary>
        /// Команда переключения на UnCheck
        /// </summary>
        public DelegateCommand<Object[]> UnBlockChechCommand { get; set; }
        #endregion

        #region Методы
        /// <summary>
        /// Добавление клиента
        /// </summary>
        /// <param name="temp">Массив параметров</param>
        private void AddClientCommand(object[] temp)
        {
            if (temp == null)
                return;

            string naturOrLegal = string.Empty;
            string simpleOrVip = string.Empty;


            foreach (var item in temp)
            {
                if (item is RadioButton && (item as RadioButton).IsChecked == true)
                    naturOrLegal = (item as RadioButton).Content.ToString();
                if (item.ToString() == "Обычный" || item.ToString() == "VIP")
                    simpleOrVip = item.ToString();
            }
            List<object> list = new List<object>();
            try
            {
                foreach (var item in temp)
                {
                    if (!string.IsNullOrEmpty(item.ToString()) && !(item is RadioButton))
                    {
                        list.Add(item);
                    }
                }

                if (!string.IsNullOrEmpty(naturOrLegal) && !string.IsNullOrEmpty(simpleOrVip))
                {
                    bank.AddClient(list, naturOrLegal, simpleOrVip);
                    MessageBox.Show("Добавлено!");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// Проверка даты 
        /// </summary>
        /// <param name="box">TextBox date</param>
        private void TestDate(object box)
        {
            if ((box as TextBox).Name == "boxBirthday")
                Date = (box as TextBox).Text;
            else
                DateCreate = (box as TextBox).Text;

            if (!TextIsDate((box as TextBox).Text.ToString()))
            {
                MessageBox.Show("ОШИБКА");
                Date = string.Empty;
                if ((box as TextBox).Name == "boxBirthday")
                    Date = string.Empty;
                else
                    DateCreate = string.Empty;

                StateButton = false;
                (box as TextBox).Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }
            else
            {
                StateButton = true;
                (box as TextBox).Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
        }

        /// <summary>
        /// RadioBut Check
        /// </summary>
        /// <param name="s">Массив элементов</param>
        private void BlockCheck(object[] s)
        {
            foreach (var item in s)
            {
                if (item is TextBox)
                {
                    (item as TextBox).IsEnabled = true;
                    (item as TextBox).Text = string.Empty;
                }
                else if (item is ComboBox)
                {
                    (item as ComboBox).IsEnabled = true;
                    (item as ComboBox).Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// RadioBut Uncheck
        /// </summary>
        /// <param name="s"></param>
        private void UnblockCheck(object[] s)
        {
            foreach (var item in s)
            {
                if (item is TextBox)
                {
                    (item as TextBox).IsEnabled = false;
                    (item as TextBox).Text = string.Empty;
                }
                else if (item is ComboBox)
                {
                    (item as ComboBox).IsEnabled = false;
                    (item as ComboBox).Text = string.Empty;
                }
            }
        }
        /// <summary>
        /// Проверка на то, чтобы дата была по формату dd.mm.yyyy
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        static bool TextIsDate(string text)
        {
            var dateFormat = "dd.MM.yyyy";
            var dateFormat2 = "dd,MM,yyyy";
            DateTime scheduleDate;
            if (DateTime.TryParseExact(text, dateFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out scheduleDate)
                || DateTime.TryParseExact(text, dateFormat2, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out scheduleDate))
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}