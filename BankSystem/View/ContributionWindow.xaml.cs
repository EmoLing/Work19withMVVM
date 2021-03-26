using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BankSystem.ViewModel;

namespace BankSystem.View
{
    /// <summary>
    /// Логика взаимодействия для ContributionWindow.xaml
    /// </summary>
    public partial class ContributionWindow : Window
    {
        public ContributionWindow(ViewModelContibution viewModelContibution)
        {
            InitializeComponent();
            DataContext = viewModelContibution;
        }
        /// <summary>
        /// Ограничение на ввод только цифр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoxSum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
