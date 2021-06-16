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
using DemoUser.Data;

namespace DemoUser.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        DemoUserEntities DB = new DemoUserEntities();
        int actualPage;
        public ClientPage()
        {
            InitializeComponent();           
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
            GenderSerch.ItemsSource = Tools.Tools.CreateComboBoxList(DB.GenderOfClient.ToList(), new Data.GenderOfClient() { GenderName = "Все" });
            GenderSerch.SelectedIndex = 0;
            CBItemsOnList.SelectedIndex = 0;
            CBItemsOnList.ItemsSource = new List<string> { "All", "10", "50", "200" };
            Refresh();
        }


        private void Refresh()
        {
            var filteredClient = (IEnumerable<Data.Client>)DB.Client;
            if (GenderSerch.SelectedIndex > 0)
            {
                var selectedGender = GenderSerch.SelectedItem as Data.GenderOfClient;
                filteredClient = filteredClient.Where(i => i.GenderOfClient.GenderName == selectedGender.GenderName);
            }

            if (!string.IsNullOrWhiteSpace(FirstnameSerch.Text))
            {
                var search = FirstnameSerch.Text.ToLower();
                filteredClient = filteredClient.Where(i => i.Firstname.ToLower().Contains(search)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(LastnameSerch.Text))
            {
                var search = LastnameSerch.Text.ToLower();
                filteredClient = filteredClient.Where(i => i.Lastname.ToLower().Contains(search)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(PatronymicSerch.Text))
            {
                var search = PatronymicSerch.Text.ToLower();
                filteredClient = filteredClient.Where(i => i.Patronymic.ToLower().Contains(search)).ToList();
            }

            if (CBItemsOnList.SelectedIndex > 0 && filteredClient.Count() > 0)
            {
                int itemsOnPage = Int32.Parse((string)CBItemsOnList.SelectedItem);
                filteredClient = filteredClient.Skip(itemsOnPage * actualPage).Take(itemsOnPage);
                if (filteredClient.Count() == 0)
                {
                    actualPage--;
                    Refresh();
                    return;
                }
            }

            ClientDG.ItemsSource = filteredClient.ToList();
            TCount.Text = $"{filteredClient.Count()}/{DB.Client.Count()}";
        }

        private void AddB_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddOrChangePage(
            new Data.Client()
            { BirthDate = DateTime.Today }
            ));
        }

        private void DeleteB_Click(object sender, RoutedEventArgs e)
        {
            var selectedClient = ClientDG.SelectedItem as Data.Client;
            if (selectedClient == null)
                return;
            if (selectedClient.LastVizit != null) MessageBox.Show("Удаление запрещено");
            else
            {
                DB.Client.Remove(selectedClient);
                DB.SaveChanges();
            }          
            ClientDG.ItemsSource = DB.Client.ToList();
        }

        private void ChangeB_Click(object sender, RoutedEventArgs e)
        {
            var selectedClient = ClientDG.SelectedItem as Client;
            if (selectedClient == null)
                return;
            NavigationService.Navigate(new AddOrChangePage(selectedClient));

        }

        private void GenderSerch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void FirstnameSerch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void LastnameSerch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void PatronymicSerch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void EmailSerch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void NomberSerch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void BNext_Click(object sender, RoutedEventArgs e)
        {
            actualPage++;
            Refresh();
        }

        private void BPrev_Click(object sender, RoutedEventArgs e)
        {
            actualPage--;
            if (actualPage < 0)
                actualPage = 0;
            Refresh();
        }

        private void CBItemsOnList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            actualPage = 0;
            Refresh();
        }
    }
}
