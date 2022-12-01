using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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

namespace WpfTilausDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Tilaushallinta tilaushallinta = new Tilaushallinta();
        AsiakasSivu asiakasSivu = new AsiakasSivu();
        public MainWindow()
        {
            InitializeComponent();
            Page.NavigationService.Navigate(tilaushallinta);



        }

        private void BtmTilaus_Click(object sender, RoutedEventArgs e)
        {
            Page.NavigationService.Navigate(tilaushallinta);
        }

        private void BtnAsiakkaat_Click(object sender, RoutedEventArgs e)
        {
            Page.NavigationService.Navigate(asiakasSivu);
        }

        private void BtnTuotteet_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
