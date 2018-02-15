using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {

        private List<Czesci> Snake = new List<Czesci>();
        private Czesci obiekt = new Czesci();

        public Form1()
        {
            InitializeComponent();
            new Ustawienia();
            czasGry.Interval = 750 / Ustawienia.Szybkosc;
            czasGry.Tick += Rysuj;
            czasGry.Start();

            zacznijGre();


        }

        private void Rysuj(object sender, EventArgs e)
        {

            if (Ustawienia.KoniecGry == true)
            {
                if (Sterowanie.KlawiszNacisniety(Keys.Enter))
                {
                    zacznijGre();
                }
            }
            else
            {
                if (Sterowanie.KlawiszNacisniety(Keys.Right) && Ustawienia.kierunek != Kierunki.Lewo)
                {
                    Ustawienia.kierunek = Kierunki.Prawo;
                }
                else if (Sterowanie.KlawiszNacisniety(Keys.Left) && Ustawienia.kierunek != Kierunki.Prawo)
                {
                    Ustawienia.kierunek = Kierunki.Lewo;
                }
                else if (Sterowanie.KlawiszNacisniety(Keys.Up) && Ustawienia.kierunek != Kierunki.Dol)
                {
                    Ustawienia.kierunek = Kierunki.Gora;
                }
                else if (Sterowanie.KlawiszNacisniety(Keys.Down) && Ustawienia.kierunek != Kierunki.Gora)
                {
                    Ustawienia.kierunek = Kierunki.Dol;
                }

                ruszajWeza();
            }
            tloGry.Invalidate();
        }

        private void ruszajWeza()
        {
            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (Ustawienia.kierunek)
                    {
                        case Kierunki.Prawo:
                            Snake[i].X++;
                            break;
                        case Kierunki.Lewo:
                            Snake[i].X--;
                            break;
                        case Kierunki.Gora:
                            Snake[i].Y--;
                            break;
                        case Kierunki.Dol:
                            Snake[i].Y++;
                            break;
                    }

                    int Xmaksymalne = tloGry.Size.Width / Ustawienia.Szerokosc;
                    int Ymaksymalne = tloGry.Size.Height / Ustawienia.Wysokosc;

                    if (Snake[i].X < 0 || Snake[i].Y < 0 || Snake[i].X > Xmaksymalne || Snake[i].Y > Ymaksymalne)
                    {
                        smierc();
                    }

                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            smierc();
                        }
                    }

                    if (Snake[0].X == obiekt.X && Snake[0].Y == obiekt.Y)
                    {
                        polknijObiekt();
                    }

                }
                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
            
        }

        private void zacznijGre()
        {
            label3.Visible = false;
            new Ustawienia();
            Snake.Clear();
            Czesci poczatekWeza = new Czesci {X = 5, Y = 15};
            Snake.Add(poczatekWeza);
            label2.Text = Ustawienia.Punkty.ToString();

            generujObiektDoZjedzenia();
        }

        private void smierc()
        {
            Ustawienia.KoniecGry = true;
        }

        private void polknijObiekt()
        {
            Czesci obecnaDlugoscWeza = new Czesci()
                {X = Snake[Snake.Count - 1].X, Y = Snake[Snake.Count - 1].Y};
            Snake.Add(obecnaDlugoscWeza);
            Ustawienia.Punkty += Ustawienia.Punktacja;
            label2.Text = Ustawienia.Punkty.ToString();
            generujObiektDoZjedzenia();
            Ustawienia.Punktacja += 1;
            Ustawienia.Szybkosc += 1;

        }

        private void generujObiektDoZjedzenia()
        {
            int Xmaksymalne = tloGry.Size.Width/Ustawienia.Szerokosc;
            int Ymaksymalne = tloGry.Size.Height/Ustawienia.Szerokosc;
            Random rnd = new Random();
            obiekt = new Czesci {X = rnd.Next(0, Xmaksymalne), Y = rnd.Next(0, Ymaksymalne)};
        }

        private void NacisniecieKlawisza(object sender, KeyEventArgs e)
        {
            Sterowanie.zmianaStanu(e.KeyCode, true);
        }

        private void PuszczenieKlawisza(object sender, KeyEventArgs e)
        {
            Sterowanie.zmianaStanu(e.KeyCode, false);
        }

        private void Rysuj(object sender, PaintEventArgs e)
        {
            Graphics tlo = e.Graphics;

            if (Ustawienia.KoniecGry == false)
            {
                Brush kolorWeza;

                for (int i = 0; i < Snake.Count; i++)
                {
                    if (i == 0)
                    { kolorWeza = Brushes.LightGreen;}
                    else
                    {
                        kolorWeza = Brushes.DarkGreen;
                    }
                    //poczatek i dalsze czesci weza
                    tlo.FillEllipse(kolorWeza,new Rectangle(Snake[i].X*Ustawienia.Szerokosc,Snake[i].Y*Ustawienia.Wysokosc,Ustawienia.Szerokosc,Ustawienia.Wysokosc));
                    //powstawanie obiektow do zjedzenia przez weza
                    tlo.FillEllipse(Brushes.White,new Rectangle(obiekt.X*Ustawienia.Szerokosc,obiekt.Y*Ustawienia.Wysokosc,Ustawienia.Szerokosc,Ustawienia.Wysokosc));
                }
            }

            else
            {
                string tekstNaKoniec = "GAME OVER\nLiczba punktów: " + Ustawienia.Punkty + "\nNaciśnij Enter";
                label3.Text = tekstNaKoniec;
                label3.Visible = true;
            }

        }
    }
}
