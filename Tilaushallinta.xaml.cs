using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Interaction logic for Tilaushallinta.xaml
    /// </summary>
    public partial class Tilaushallinta : Page
    {
        TilausDBEntities entity = new TilausDBEntities();

        List<Asiakkaat> asiakaslista = new List<Asiakkaat>();
        List<Tuotteet> tuotelista = new List<Tuotteet>();

        List<Tilausrivit> rivilista = new List<Tilausrivit>();

        List<GridLista> GridinTuotteet = new List<GridLista>();

        int tilausriviID;
        public Tilaushallinta()
        {
            InitializeComponent();


            tuotelista = Utility.HaeTuotteet(entity);   //Tallenetaan kaikki tuotteet listaan tietokannasta
            Utility.SetComboBox(tuotelista, CmbTuotteet);   //Asetetaan tuotteet Combo Boxiin

            asiakaslista = Utility.HaeAsiakkaat(entity);    //Tallenetaan kaikki asiakaat listaan tietokannasta
            Utility.SetComboBox(asiakaslista, CmbAsiakkaat);    //Asetetaan asiakkaat Combo Boxiin

            tilausriviID = Utility.HaeTilausRivinumero(entity); //Haetaan ja tallenetaan seuraava tilausrivi


            TxtTuoMaara.Text = "1";

            DatTilaus.SelectedDate = DateTime.Now;              //Asetetaan oletus tilaus ja toimitus päivät
            DatToimitus.SelectedDate = DateTime.Now.AddDays(3);

            DgRiviLista.ItemsSource = GridinTuotteet;           //Asetetaan Datagridin tietolähde
        }
        private void CmbAsiakkaat_DropDownClosed(object sender, EventArgs e)
        {

            foreach (var i in asiakaslista)
            {
                if (i.Nimi == CmbAsiakkaat.Text)
                {
                    TxtAsiakasNmr.Text = i.AsiakasID.ToString();
                    TxtAsiaOsoite.Text = i.Osoite;
                    TxtAsiaPostiNmr.Text = i.Postinumero.ToString();
                    TxtAsiaPostToimi.Text = i.Postitoimipaikat.Postitoimipaikka.ToString();
                    break;
                }
            }

            TxtTilausNumero.Text = Utility.HaeTilausnumero(entity).ToString();
        }

        private void CmbTuotteet_DropDownClosed(object sender, EventArgs e)
        {
            foreach ( var i in tuotelista)
            {
                if (i.Nimi == CmbTuotteet.Text)
                {
                    TxtTuoteNmr.Text = i.TuoteID.ToString();
                    TxtTuoteHinta.Text = i.Ahinta.ToString();
                    break;
                }
            }
        }

        private void TuotePlusMinusBtn(object sender, RoutedEventArgs e)
        {
            int maara = int.Parse(TxtTuoMaara.Text);
            if (sender == BtnTuoteMiinus && maara > 1)
            {
                maara--;
            }
            if (sender == BtnTuotePlus)
            {
                maara++;
            }
            TxtTuoMaara.Text = maara.ToString();
        }


        private void TxtDataContextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtTuoteHinta.Text == "" || TxtTuoMaara.Text == "")
            {
                TxtTuoteSumma.Text = "0";
            }
            else
            {
                decimal maara = decimal.Parse(TxtTuoMaara.Text);
                decimal hinta = decimal.Parse(TxtTuoteHinta.Text);

                decimal summa = maara * hinta;

                TxtTuoteSumma.Text = summa.ToString();
            }
        }

        private void SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatTilaus.SelectedDate == null ||DatToimitus.SelectedDate == null)
            {
                return;
            }
            else
            {
                DateTime tilaus = DatTilaus.SelectedDate.Value;
                DateTime toimitus = DatToimitus.SelectedDate.Value;
                TimeSpan Erotus = toimitus - tilaus;
                TxtToimitusAika.Text = Erotus.Days.ToString();
            }
        }

        private void BtnLisaaRivi_Click(object sender, RoutedEventArgs e)
        {
            if (CmbAsiakkaat.Text != "" && CmbTuotteet.Text != "")
            {
                int index = tuotelista.FindIndex(x => x.Nimi == CmbTuotteet.Text);

                GridLista tiedot = new GridLista(tuotelista[index].TuoteID, tuotelista[index].Nimi,
                    int.Parse(TxtTuoMaara.Text), (decimal)tuotelista[index].Ahinta,
                    decimal.Parse(TxtTuoteSumma.Text),
                    int.Parse(TxtTilausNumero.Text));

                Tilausrivit rivi = new Tilausrivit()
                {
                    TilausriviID = tilausriviID,
                    TilausID = Utility.HaeTilausnumero(entity),
                    TuoteID = tuotelista[index].TuoteID,
                    Maara = int.Parse(TxtTuoMaara.Text),
                    Ahinta = (decimal)tuotelista[index].Ahinta
                };
                rivilista.Add(rivi);

                GridinTuotteet.Add(tiedot);
                DgRiviLista.Items.Refresh();
                tilausriviID++;

                decimal summa = 0;

                foreach (var i in GridinTuotteet)
                {
                    summa += i.Summa;
                }
                TxtTilKokonSumma.Text = summa.ToString();
            }
            else
            {
                MessageBox.Show("Ei tarvittavia tietoja tilausrivin luomiseen!");
            }

        }
        private void BtnLuoTilaus_Click(object sender, RoutedEventArgs e)
        {
            if (rivilista.Count != 0)
            {
                int index = asiakaslista.FindIndex(x => x.Nimi == CmbAsiakkaat.Text);

                Tilaukset uusitilaus = new Tilaukset()
                {
                    TilausID = int.Parse(TxtTilausNumero.Text),
                    AsiakasID = asiakaslista[index].AsiakasID,
                    Toimitusosoite = asiakaslista[index].Osoite,
                    Postinumero = asiakaslista[index].Postinumero,
                    Tilauspvm = DatTilaus.SelectedDate.Value,
                    Toimituspvm = DatToimitus.SelectedDate.Value

                };
                entity.Tilaukset.Add(uusitilaus);

                foreach (var i in rivilista)
                {
                    entity.Tilausrivit.Add(i);
                }
                entity.SaveChanges();

                MessageBox.Show("Tilaus luotu ja tallenettu!");
                ClearAll();
            }
            else
            {
                MessageBox.Show("Tilauksen luonti epäonnistui!");
            }

        }


        private void ClearAll()
        {
            foreach (var i in GridPohja.Children)
            {
                if (i.GetType() == typeof(TextBox))
                {
                    ((TextBox)i).Text = string.Empty;
                }
                if (i.GetType() == typeof(ComboBox))
                {
                    ((ComboBox)i).Text = string.Empty;
                }
            }
            
            rivilista.Clear();
            GridinTuotteet.Clear();
            DgRiviLista.Items.Refresh();
            TxtTuoMaara.Text = "1";
            DatTilaus.SelectedDate = DateTime.Now;
            DatToimitus.SelectedDate = DateTime.Now.AddDays(3);
            
        }

        private void BtnPeruutaTilaus_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void BtnPoistaRivi_Click(object sender, RoutedEventArgs e)
        {
            if (GridinTuotteet.Count != 0)
            {
                GridinTuotteet.Remove(GridinTuotteet.Last());
                DgRiviLista.Items.Refresh();
            }

        }
    }
}
