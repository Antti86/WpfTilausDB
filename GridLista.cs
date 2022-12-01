using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTilausDB
{
    public class GridLista
    {
        public GridLista(int tuoteId, string tuoteNimi, int maara, decimal ahinta,
            decimal summa, int tilausId)
        {
            TuoteId = tuoteId;
            TuoteNimi = tuoteNimi;
            Maara = maara;
            Ahinta = ahinta;
            Summa = summa;
            TilausId = tilausId;
        }

        public int TuoteId { get; private set; }
        public string TuoteNimi { get; private set; }
        public int Maara { get; private set; }
        public decimal Ahinta { get; private set; }
        public decimal Summa { get; private set; }
        public int TilausId  { get; private set; }




    }
}
