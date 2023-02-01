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
    /// Interaction logic for AsiakasSivu.xaml
    /// </summary>
    public partial class AsiakasSivu : Page
    {
        TilausDBEntities entity = new TilausDBEntities();

        List<Asiakkaat> asiakaslista = new List<Asiakkaat>();
        public AsiakasSivu()
        {
            InitializeComponent();

            asiakaslista = Utility.HaeAsiakkaat(entity);

            DgAsiakaslista.ItemsSource = asiakaslista;

            

            
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

            //if (DgAsiakaslista.SelectedItem != null)
            //{
            //    Asiakaat test = DgAsiakaslista.SelectedItem;

            //    Asiakkaat t = new Asiakkaat()
            //    {
            //        DgAsiakaslista.SelectedItem.
            //    };
            //    //https://stackoverflow.com/questions/19225568/wpf-datagrid-get-selected-cell-value

            //    int test2 = 0;
                
            //    //int index = asiakaslista.FindIndex
            //    //    (x => x.Nimi == DgAsiakaslista.SelectedItem.ToString());

            //    //asiakaslista.Remove(asiakaslista[index]);
            //}



        }
    }
}
