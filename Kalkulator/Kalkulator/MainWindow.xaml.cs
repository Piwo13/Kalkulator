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
using System.Globalization;

namespace Kalkulator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool blad = false;
        private bool sczyt = false;
        private string ost;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Numer_click(object sender, RoutedEventArgs e)
        {
            Blad();
            Button button = (Button)sender;
            string num = button.Content.ToString();

            if (sczyt == false)
            {
                if(pamiec.Text.Length !=0 && pamiec.Text.Last() == '=')
                {
                    pamiec.Text = "";
                }
                wejscie.Text = num;
                sczyt = true;
            }
            else
            {
                if (wejscie.Text.Equals("0"))
                {
                    wejscie.Text = num;
                }
                else
                {
                    if (wejscie.Text.Length < 21)
                    {
                        wejscie.Text += num;
                    }
                }
            }
            Logika.Rozmiar(wejscie);
        }

        private void Akcja_click(object sender, RoutedEventArgs e)
        {
            Blad();
            Button but = (Button)sender;
            Logika.Dzialania(wejscie, pamiec, but.Content.ToString().First(), ref blad, ref sczyt, ref ost);
            Logika.Rozmiar(wejscie);
        }

        private void  Zero_click(object sender, RoutedEventArgs e)
        {
            Blad();
            if (sczyt == false)
            {
                wejscie.Text = "0";
                sczyt = true;
            }
            else if (!wejscie.Text.Equals("0") && wejscie.Text.Length < 21)
            {
                wejscie.Text += '0';
                Logika.Rozmiar(wejscie);
            }
        }

        private void Backspace_click(object sender, RoutedEventArgs e)
        {
            Blad();
            if(sczyt != false)
            {
                if (!wejscie.Text.Equals("0"))
                {
                    if((wejscie.Text.Length==2 && Double.Parse(wejscie.Text.Replace(',','.'), CultureInfo.InvariantCulture) <0) || wejscie.Text.Length == 1)
                    {
                        wejscie.Text = "0";

                    }
                    else
                    {
                        wejscie.Text = wejscie.Text.Remove(wejscie.Text.Length - 1, 1);
                    }
                }
            }
            else
            {
                if(pamiec.Text.Length != 0)
                {
                    pamiec.Text = "";
                }
            }
            Logika.Rozmiar(wejscie);
        }

        private void Clear_click(object sender, RoutedEventArgs e)
        {
            Blad();
            wejscie.Text = "0";
            sczyt = true;
            Logika.Rozmiar(wejscie);
        }

        private void Clearall_click(object sender, RoutedEventArgs e)
        {
            wejscie.Text = "0";
            pamiec.Text = "";
            sczyt = false;
            Logika.Rozmiar(wejscie);
        }

        private void Negacja_click(object sender, RoutedEventArgs e)
        {
            Blad();
            if(Double.Parse(wejscie.Text.Replace(',','.'), CultureInfo.InvariantCulture) != 0)
            {
                if (Double.Parse(wejscie.Text.Replace(',', '.'), CultureInfo.InvariantCulture) < 0)
                {
                    wejscie.Text = wejscie.Text.Remove(0, 1);
                }
                else
                {
                    if (wejscie.Text.Length < 21)
                    {
                        wejscie.Text = '-' + wejscie.Text;
                    }
                }
            }
            sczyt = true;
            Logika.Rozmiar(wejscie);
        }

        private void Kropka_click(object sender, RoutedEventArgs e)
        {
            Blad();
            if (!wejscie.Text.Contains(',') && wejscie.Text.Length < 21)
            {
                wejscie.Text += ',';
                Logika.Rozmiar(wejscie);
                sczyt = true;
            }
        }

        private void Enter_click(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                Logika.PerformClick(rownanie);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(!(Keyboard.IsKeyDown(Key.RightShift) || Keyboard.IsKeyDown(Key.LeftShift)))
            {
                if (e.Key == Key.D1 || e.Key == Key.NumPad1)
                {
                    Logika.PerformClick(jeden);
                }
                else if (e.Key == Key.D2 || e.Key == Key.NumPad2)
                {
                    Logika.PerformClick(dwa);
                }
                else if (e.Key == Key.D3 || e.Key == Key.NumPad3)
                {
                    Logika.PerformClick(trzy);
                }
                else if (e.Key == Key.D4 || e.Key == Key.NumPad4)
                {
                    Logika.PerformClick(cztery);
                }
                else if (e.Key == Key.D5 || e.Key == Key.NumPad5)
                {
                    Logika.PerformClick(piec);
                }
                else if (e.Key == Key.D6 || e.Key == Key.NumPad6)
                {
                    Logika.PerformClick(szesc);
                }
                else if (e.Key == Key.D7 || e.Key == Key.NumPad7)
                {
                    Logika.PerformClick(siedem);
                }
                else if (e.Key == Key.D8 || e.Key == Key.NumPad8)
                {
                    Logika.PerformClick(osiem);
                }
                else if (e.Key == Key.D9 || e.Key == Key.NumPad9)
                {
                    Logika.PerformClick(dziewiec);
                }
                else if (e.Key == Key.D0 || e.Key == Key.NumPad0)
                {
                    Logika.PerformClick(zero);
                }
                else if (e.Key == Key.Back)
                {
                    Logika.PerformClick(backspace);
                }
                else if (e.Key == Key.OemComma || e.Key == Key.OemPeriod)
                {
                    Logika.PerformClick(kropka);
                }
                else if (e.Key == Key.OemMinus)
                {
                    Logika.PerformClick(minus);
                }
                else if (e.Key == Key.Enter || e.Key == Key.OemPlus)
                {
                    Logika.PerformClick(rownanie);
                }
                else if (e.Key == Key.OemQuestion)
                {
                    Logika.PerformClick(dzielenie);
                }
                else if (e.Key == Key.Multiply)
                {
                    Logika.PerformClick(mnozenie);
                }
            }
            else
            {
                if (e.Key == Key.OemPlus)
                {
                    Logika.PerformClick(dodawanie);
                }
                else if (e.Key == Key.D8)
                {
                    Logika.PerformClick(mnozenie);
                }
            }
        }

        private void Blad()
        {
            if (blad == true)
            {
                Logika.PerformClick(clearall);
                blad = false;
                ost = "";
            }
        }
    } 
}
