using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Kalkulator
{
    static class Logika
    {
        public static void Rozmiar(TextBox sender)
        {
            if (sender.Text.Length >= 22)
            {
                sender.FontSize = 15;
            }
            else if (sender.Text.Length > 14 && sender.Text.Length < 22)
            {
                sender.FontSize = 25;
            }
            else
            {
                sender.FontSize = 38;
            }
        }
        public static void PerformClick(Button button)
        {
            button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        public static void Dzialania(TextBox wejscie, TextBox wyjscie, char akcja, ref bool blad, ref bool szczyt, ref string ost)
        {
            string wej = wejscie.Text;
            string wyj = wyjscie.Text;
            if (wej.EndsWith(","))
            {
                wej = wej.Replace(",", "");
                wejscie.Text = wej;
            }
            if (akcja != '=')
            {
                if (wyj.Length == 0)
                {
                    wyjscie.Text = wej + ' ' + akcja;
                    szczyt = false;
                }
                else
                {
                    if ((wyj.EndsWith("+") || wyj.EndsWith("/") || wyj.EndsWith("-") || wyj.EndsWith("*")) && szczyt == false)
                    {
                        wyjscie.Text = wyj.Remove(wyj.Length - 1, 1) + akcja;
                    }
                    else if (wyj.EndsWith("="))
                    {
                        wyjscie.Text = wej + ' ' + akcja;
                    }
                    else
                    {
                        wyjscie.Text = wyj + ' ' + wej + ' ' + akcja;
                        wejscie.Text = Logika.Oblicz(wyj + ' ' + wej, ref blad);
                        szczyt = false;
                    }
                }
            }
            else
            {
                if (wyj.Length == 0)
                {
                    wyjscie.Text = wej + " =";
                }
                else
                {
                    if (wyj.Last() == '=')
                    {
                        wyjscie.Text = wej + ost + " =";
                        wejscie.Text = Logika.Oblicz(wej + ost, ref blad);
                    }
                    else
                    {
                        ost = ' ' + wyj.Last().ToString() + ' ' + wej;
                        wejscie.Text = Logika.Oblicz(wyj + ' ' + wej, ref blad);
                        wyjscie.Text = wyj + ' ' + wej + " =";
                    }
                }
                szczyt = false;
            }
            wyjscie.Select(wyjscie.Text.Length, 0);
            wyjscie.Focus();
        }

        private static string Oblicz(string s, ref bool blad)
        {
            try
            {
                List<string> elements = s.Split(' ').ToList();
                int index;
                double tmp;
                while (elements.Count() > 1)
                {
                    if (elements.Contains("/") || elements.Contains("*"))
                    {
                        index = elements.FindIndex(x => x == "/" || x == "*");
                    }
                    else
                    {
                        index = elements.FindIndex(x => x == "+" || x == "-");
                    }
                    if (elements[index] == "/")
                    {
                        tmp = Double.Parse(elements[index - 1].Replace(',', '.'), CultureInfo.InvariantCulture) / Double.Parse(elements[index + 1].Replace(',', '.'), CultureInfo.InvariantCulture);
                    }
                    else if (elements[index] == "*")
                    {
                        tmp = Double.Parse(elements[index - 1].Replace(',', '.'), CultureInfo.InvariantCulture) * Double.Parse(elements[index + 1].Replace(',', '.'), CultureInfo.InvariantCulture);
                    }
                    else if (elements[index] == "+")
                    {
                        tmp = Double.Parse(elements[index - 1].Replace(',', '.'), CultureInfo.InvariantCulture) + Double.Parse(elements[index + 1].Replace(',', '.'), CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        tmp = Double.Parse(elements[index - 1].Replace(',', '.'), CultureInfo.InvariantCulture) - Double.Parse(elements[index + 1].Replace(',', '.'), CultureInfo.InvariantCulture);
                    }
                    elements[index] = Math.Round(tmp, 12).ToString().Replace('.', ',');
                    elements.RemoveAt(index + 1);
                    elements.RemoveAt(index - 1);
                }

                return elements[0];
            }
            catch
            {
                blad = true;
                return "Error";
            }
        }
    }
}
