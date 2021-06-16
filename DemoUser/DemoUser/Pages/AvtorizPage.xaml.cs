using DemoUser.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoUser.Pages
{
    /// <summary>
    /// Логика взаимодействия для AvtorizPage.xaml
    /// </summary>
    public partial class AvtorizPage : Page
    {
        DemoUserEntities DB = new DemoUserEntities();

        public AvtorizPage()
        {
            InitializeComponent();
            if (Properties.Settings.Default.Login != null)
            {
                LoginTB.Text = Properties.Settings.Default.Login;
            }
        }

        private void AvtorizB_Click(object sender, RoutedEventArgs e)
        {
            var user = DB.User.FirstOrDefault(i => i.Login == LoginTB.Text && i.Password == PasswordTB.Password);
            if (REmemberCB.IsChecked.GetValueOrDefault())
            {
                Properties.Settings.Default.Login = user.Login;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Login = null;
                Properties.Settings.Default.Save();
            }
            if (user != null) this.NavigationService.Navigate(new ClientPage());
            else
            {
                MessageBox.Show("Введите корректные данные");
                LoginTB.Clear();
                PasswordTB.Clear();
            }
        }
    }
}
