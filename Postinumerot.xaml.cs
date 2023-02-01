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

namespace WpfTilausDB
{
    /// <summary>
    /// Interaction logic for Postinumerot.xaml
    /// </summary>
    public partial class Postinumerot : Page
    {
        TilausDBEntities entity = new TilausDBEntities();

        List<Postitoimipaikat> postitoimipaikatlista = new List<Postitoimipaikat>();
        public Postinumerot()
        {
            InitializeComponent();

            postitoimipaikatlista = Utility.HaePostitoimipaikat(entity);

            DgPostiToimi.ItemsSource = postitoimipaikatlista;
        }

        private void DgPostiToimi_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
