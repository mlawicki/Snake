using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public enum Kierunki
    {
        Lewo,
        Prawo,
        Gora,
        Dol
    };

    class Ustawienia
    {
        public static int Szerokosc { get; set; }
        public static int Wysokosc { get; set; }
        public static int Szybkosc { get; set; }
        public static int Punktacja { get; set; }
        public static int Punkty { get; set; }
        public static bool KoniecGry { get; set; }
        public static Kierunki kierunek { get; set; }

        public Ustawienia()
        { 
            Szerokosc = 15;
            Wysokosc = 15;
            Szybkosc = 5;
            Punktacja = 10;
            Punkty = 10;
            KoniecGry = false;
            kierunek = Kierunki.Dol;
        }
    }
}
