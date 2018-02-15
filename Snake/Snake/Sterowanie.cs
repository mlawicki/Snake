using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace Snake
{
    class Sterowanie
    {
        private static Hashtable tabelaKlawiszy = new Hashtable();

        public static bool KlawiszNacisniety(Keys klawisz)
        {
            if (tabelaKlawiszy[klawisz] == null)
            {
                return false;
            }

            return (bool) tabelaKlawiszy[klawisz];
        }

        public static void zmianaStanu(Keys klawisz, bool stan)
        {
            tabelaKlawiszy[klawisz] = stan;
        }






    }
}
