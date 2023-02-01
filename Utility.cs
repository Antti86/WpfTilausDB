using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfTilausDB
{
    static public class Utility
    {

        static public void SetComboBox<T>(List<T> list, ComboBox combo)
        {
            combo.DisplayMemberPath = "Nimi";
            combo.SelectedValuePath = "Nimi";
            combo.ItemsSource = list;
        }

        static public List<Asiakkaat> HaeAsiakkaat(TilausDBEntities entity)
        {
            var items = from i in entity.Asiakkaat
                        select i;

            List<Asiakkaat> list = new List<Asiakkaat>();

            foreach (var k in items)
            {
                Asiakkaat t = new Asiakkaat()
                {
                    Nimi = k.Nimi,
                    AsiakasID = k.AsiakasID,
                    Osoite = k.Osoite,
                    Postinumero = k.Postinumero,
                    Postitoimipaikat = k.Postitoimipaikat

                };

                list.Add(t);
            }
            return list;
        }

        static public List<Tuotteet> HaeTuotteet(TilausDBEntities entity)
        {
            var items = from i in entity.Tuotteet
                        select i;
            List<Tuotteet> list = new List<Tuotteet>();


            foreach (var k in items)
            {
                Tuotteet t = new Tuotteet()
                {
                    Nimi = k.Nimi,
                    TuoteID = k.TuoteID,
                    Ahinta = k.Ahinta
                };

                list.Add(t);
            }
            return list;
        }
        static public List<Postitoimipaikat> HaePostitoimipaikat(TilausDBEntities entity)
        {
            var items = from i in entity.Postitoimipaikat
                        select i;
            List<Postitoimipaikat> list = new List<Postitoimipaikat>();


            foreach (var k in items)
            {
                Postitoimipaikat t = new Postitoimipaikat()
                {
                    Postinumero = k.Postinumero,
                    Postitoimipaikka = k.Postitoimipaikka
                };

                list.Add(t);
            }
            return list;
        }
        static public int HaeTilausnumero(TilausDBEntities entity)
        {
            if (entity.Tilaukset.Count() == 0) 
            {
                return 100000;
            }
            else
            {
                var tilauk = from i in entity.Tilaukset
                             select i.TilausID;

                return tilauk.Max() + 1;
            }
        }

        static public int HaeTilausRivinumero(TilausDBEntities entity)
        {
            if (entity.Tilausrivit.Count() == 0) 
            {
                return 100000;
            }
            else
            {
                var tilauk = from i in entity.Tilausrivit
                             select i.TilausriviID;

                return tilauk.Max() + 1;
            }
        }

    }
}
