using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
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
using Microsoft.Win32;

namespace DemoUser.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddOrChangePage.xaml
    /// </summary>
    public partial class AddOrChangePage : Page
    {
        Client sel;
        DemoUserEntities DB = new DemoUserEntities();
        public AddOrChangePage(Client client)
        {
            InitializeComponent();
            GenderTB.ItemsSource = DB.GenderOfClient.ToList();
            sel = client;
            this.DataContext = sel;
            if (sel.Id==0)
            {
                IdL.Visibility = Visibility.Collapsed;
                IdTB.Visibility = Visibility.Collapsed;
            }
        }

        private void PhotoB_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter="*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg"
            };
            if(dialog.ShowDialog().GetValueOrDefault())
            {
                sel.Photo = File.ReadAllBytes(dialog.FileName);
                Image.Source = new BitmapImage(new Uri(dialog.FileName));
            }
        }

        private void SaveB_Click(object sender, RoutedEventArgs e)
        {           

            if (sel.Id == 0)
            DB.Client.Add(sel);
            DB.SaveChanges();
            NavigationService.GoBack();
        }

        private void BackB_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
