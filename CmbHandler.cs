using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using static WpfTilausDB.ItemList;

namespace WpfTilausDB
{
    public class ItemList //Handles ComboBox items, check the right char when calling ctr..
                          //Generic version of this class is working progress
                          //Trouble getting linq querys to match..
    {
        internal class Item
        {
            public Item(string Name_in, int Id_in)
            {
                Name = Name_in;
                Id = Id_in;
            }

            public string Name { get;  set; }

            public int Id { get;  set; }

        }

        public ItemList(ComboBox cmb_in, char c)
        {
            cmb = cmb_in;
            TilausDBEntities entity = new TilausDBEntities();

            if (c == 'a')
            {
                var items = from i in entity.Asiakkaat
                            select i;
               
                foreach (var a in items)
                {
                    
                    itemlist.Add(new Item(a.Nimi, a.AsiakasID));
                }
            }
            else if (c == 't')
            {
                var items = from i in entity.Tuotteet
                            select i;
                foreach (var a in items)
                {

                    itemlist.Add(new Item(a.Nimi, a.TuoteID));
                }
            }
            cmb.DisplayMemberPath = "Name";
            cmb.SelectedValuePath = "Id";
            cmb.ItemsSource = itemlist;
        }
        public void FlushList()
        {
            itemlist.Clear();
        }

        //variables
        List<Item> itemlist { get; set; } = new List<Item>();
        ComboBox cmb;
    }
}
