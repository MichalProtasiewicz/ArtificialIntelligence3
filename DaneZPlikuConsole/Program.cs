using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaneZPlikuConsole
{
    class Program
    {
        static string TablicaDoString<T>(T[][] tab)
        {
            string wynik = "";
            for (int i = 0; i < tab.Length; i++)
            {
                for (int j = 0; j < tab[i].Length; j++)
                {
                    wynik += tab[i][j].ToString() + " ";
                }
                wynik = wynik.Trim() + Environment.NewLine;
            }

            return wynik;
        }

        static string[][] StringToTablica(string sciezkaDoPliku)
        {
            string trescPliku = System.IO.File.ReadAllText(sciezkaDoPliku); // wczytujemy treść pliku do zmiennej
            string[] wiersze = trescPliku.Trim().Split(new char[] { '\n' }); // treść pliku dzielimy wg znaku końca linii, dzięki czemu otrzymamy każdy wiersz w oddzielnej komórce tablicy
            string[][] wczytaneDane = new string[wiersze.Length][];   // Tworzymy zmienną, która będzie przechowywała wczytane dane. Tablica będzie miała tyle wierszy ile wierszy było z wczytanego poliku

            for (int i = 0; i < wiersze.Length; i++)
            {
                string wiersz = wiersze[i].Trim();     // przypisuję i-ty element tablicy do zmiennej wiersz
                string[] cyfry = wiersz.Split(new char[] { ' ' });   // dzielimy wiersz po znaku spacji, dzięki czemu otrzymamy tablicę cyfry, w której każda oddzielna komórka to czyfra z wiersza
                wczytaneDane[i] = new string[cyfry.Length];    // Do tablicy w której będą dane finalne dokładamy wiersz w postaci tablicy integerów tak długą jak długa jest tablica cyfry, czyli tyle ile było cyfr w jednym wierszu
                for (int j = 0; j < cyfry.Length; j++)
                {
                    string cyfra = cyfry[j].Trim(); // przypisuję j-tą cyfrę do zmiennej cyfra
                    wczytaneDane[i][j] = cyfra; 
                }
            }
            return wczytaneDane;
        }

        static void Main(string[] args)
        {
            string sciezkaDoParagonu = @"paragon-system.txt";
            

            string[][] paragon = StringToTablica(sciezkaDoParagonu);

            Console.WriteLine("Dane paragonu");
            string wynikParagon = TablicaDoString(paragon);
            Console.Write(wynikParagon);


            /****************** Miejsce na rozwiązanie *********************************/

            int cz = 2;

            List<string> d = new List<string>();
            List<string> f1 = new List<string>();
			List<string> ds = new List<string>();


            string tmpstr = "";
            int ilosc = 1;

            for (int i=0; i<paragon.GetLength(0); i++)
            {
                for (int j = 0; j < paragon[0].GetLength(0); j++)
                {
                    d.Add(paragon[i][j]);
                }
            }
			ds = new List<string>(d);

			ds.Sort();
            Console.WriteLine("Posortowane D= ");
            for (int i=0; i<ds.Count; i++ )
            {
                Console.WriteLine(ds[i]);
                if (tmpstr == "")
                {
                    tmpstr = ds[i];
                }
                else
                {
                    if (ds[i] == tmpstr)
                    {
                        ilosc++;

                        if (ds.Count - 1 != i)
                        {
                            if (ds[i + 1] != tmpstr)
                            {
                                if (ilosc >= cz)
                                {
                                    f1.Add(ds[i]);
                                    ilosc = 1;
                                    tmpstr = "";
                                }
                                else
                                {
                                    ilosc = 1;
                                    tmpstr = "";
                                }
                            }
                        }
                        
                        if (i == (ds.Count - 1))
                        {
                            if (ilosc >= cz)
                            {
                                f1.Add(ds[i]);
                                ilosc = 1;
                                tmpstr = "";
                            }
                            else
                            {
                                ilosc = 1;
                                tmpstr = "";
                            }
                        }
                    }
                    else
                    {
                        ilosc = 1;
                        tmpstr = "";
                    }                     
                }
            }
            Console.WriteLine("Zbior f1= ");

            foreach (string x in f1)
            {
                Console.WriteLine("{"+x+"}");
            }


            /////////////////////////////////////

            Console.WriteLine("Zbior C2= ");

            List<string> c2 = new List<string>();

            for(int i = 0; i < f1.Count; i++)
            {
                for(int j = i+1; j < f1.Count; j++)
                {
                    c2.Add(f1[i]);
                    c2.Add(f1[j]);
                }
            }

            for(int i = 0; i < c2.Count; i+=2) 
            {
                Console.WriteLine("{" + c2[i] + ", " + c2[i + 1] + "}");
            }

			//////////////////////////////////////////

			int iloscf2 = 0;
			List<string> f2 = new List<string>();


			for (int j = 0; j < c2.Count; j += 2)
			{
				for(int i = 0; i < d.Count; i+=4)
				{
					if(((d[i] == c2[j])||(d[i+1] == c2[j])||(d[i+2] == c2[j])||(d[i+3] == c2[j]))&&((d[i] == c2[j+1])||(d[i+1] == c2[j+1])||(d[i+2] == c2[j+1])||(d[i+3] == c2[j+1])))
					{
						iloscf2++;
					}
				}
				if (iloscf2 >= 2)
				{
					f2.Add(c2[j]);
					f2.Add(c2[j + 1]);
				}
				iloscf2 = 0;
			}

			Console.WriteLine("Zbior F2= ");
			
			for (int i = 0; i < f2.Count; i += 2) 
			{
				Console.WriteLine("{" + f2[i] + ", " + f2[i + 1] + "}");
			}

            ///////////////////////////////////////

            List<string> c3 = new List<string>();
            string tmp = "";

            for (int i = 0; i < f2.Count; i += 2)
            {
                tmp = f2[i];
                for (int j = i + 2; j < f2.Count; j +=2)
                {
                      if (tmp == f2[j])
                    {
                        c3.Add(tmp);
                        c3.Add(f2[i + 1]);
                        c3.Add(f2[j + 1]);
                    }
                }
            }

            Console.WriteLine("Zbior C3= ");

            for (int i = 0; i < c3.Count; i += 3)
            {
                Console.WriteLine("{" + c3[i] + ", " + c3[i + 1] + ", " + c3[i + 2] + "}");
            }

            //////////////////////////////////////

            List<string> c3spr = new List<string>();

            int tmp1, tmp2, tmp3;

            for (int i = 0; i < c3.Count; i += 3)
            {
                tmp1 = tmp2 = tmp3 = 0;

                for (int j = 0; j < f2.Count; j++)
                {
                    if(c3[i] == f2[j])
                    {
                        tmp1++;
                    }
                    if(c3[i+1] == f2[j])
                    {
                        tmp2++;
                    }
                    if(c3[i+2] == f2[j])
                    {
                        tmp3++;
                    }
                }
                if ((tmp1 >= cz) && (tmp2 >= cz) && (tmp3 >= cz))
                {
                    c3spr.Add(c3[i]);
                    c3spr.Add(c3[i + 1]);
                    c3spr.Add(c3[i + 2]);
                }
            }

            Console.WriteLine("Zbior C3 po sprawdzeniu= ");

            for (int i = 0; i < c3spr.Count; i += 3)
            {
                Console.WriteLine("{" + c3spr[i] + ", " + c3spr[i + 1] + ", " + c3spr[i + 2] + "}");
            }

            ////////////////////////////////////////////////

            List<string> f3 = new List<string>();

            for (int i = 0; i < c3spr.Count; i += 3)
            {
                tmp1 = 0;

                for (int j = 0; j < d.Count; j += 4)
                {
                    if ((c3spr[i] == d[j] || c3spr[i] == d[j+1] || c3spr[i] == d[j+2] || c3spr[i] == d[j+3]) 
                        && (c3spr[i+1] == d[j] || c3spr[i+1] == d[j + 1] || c3spr[i+1] == d[j + 2] || c3spr[i+1] == d[j + 3]) 
                        && (c3spr[i+2] == d[j] || c3spr[i+2] == d[j + 1] || c3spr[i+2] == d[j + 2] || c3spr[i+2] == d[j + 3]))
                    {
                        tmp1++;
                    }
                }
                if(tmp1>=cz)
                {
                    f3.Add(c3spr[i]);
                    f3.Add(c3spr[i + 1]);
                    f3.Add(c3spr[i + 2]);
                }
            }

            Console.WriteLine("Zbior F3= ");

            for (int i = 0; i < f3.Count; i += 3)
            {
                Console.WriteLine("{" + f3[i] + ", " + f3[i + 1] + ", " + f3[i + 2] + "}");
            }

            //////////////////////////////////

            List<string> c4 = new List<string>();

            string tmp1s = "";
            string tmp2s = "";

            for (int i = 0; i < f3.Count; i += 3)
            {
                tmp1s = f3[i];
                tmp2s = f3[i + 1];
                for (int j = i + 3; j < f3.Count; j += 3)
                {
                    if (tmp1s == f3[j] && tmp2s == f3[j+1])
                    {
                        c4.Add(tmp1s);
                        c4.Add(tmp2s);
                        c4.Add(f3[i + 2]);
                        c4.Add(f3[j + 2]);
                    }
                }
            }

            Console.WriteLine("Zbior C4= ");

            for (int i = 0; i < c4.Count; i += 4)
            {
                Console.WriteLine("{" + c4[i] + ", " + c4[i + 1] + ", " + c4[i + 2] + ", " + c4[i + 3] + "}");
            }

            /////////////////////////////////////

            List<string> c4spr = new List<string>();

            int tmp4;

            for (int i = 0; i < c4.Count; i += 4)
            {
                tmp1 = tmp2 = tmp3 = tmp4 = 0;

                for (int j = 0; j < f3.Count; j++)
                {
                    if (c4[i] == f3[j])
                    {
                        tmp1++;
                    }
                    if (c4[i + 1] == f3[j])
                    {
                        tmp2++;
                    }
                    if (c4[i + 2] == f3[j])
                    {
                        tmp3++;
                    }
                    if (c4[i + 3] == f3[j])
                    {
                        tmp4++;
                    }
                }
                if ((tmp1 >= cz) && (tmp2 >= cz) && (tmp3 >= cz) && (tmp4 >= cz))
                {
                    c4spr.Add(c4[i]);
                    c4spr.Add(c4[i + 1]);
                    c4spr.Add(c4[i + 2]);
                    c4spr.Add(c4[i + 3]);
                }
            }

            Console.WriteLine("Zbior C4 po sprawdzeniu= ");

            for (int i = 0; i < c4spr.Count; i += 4)
            {
                Console.WriteLine("{" + c4spr[i] + ", " + c4spr[i + 1] + ", " + c4spr[i + 2] + ", " + c4[i + 3] + "}");
            }

            /////////////////////////////////////////////////

            List<string> f4 = new List<string>();

            for (int i = 0; i < c4spr.Count; i += 4)
            {
                tmp1 = 0;

                for (int j = 0; j < d.Count; j += 4)
                {
                    if ((c4spr[i] == d[j] || c4spr[i] == d[j + 1] || c4spr[i] == d[j + 2] || c4spr[i] == d[j + 3])
                        && (c4spr[i + 1] == d[j] || c4spr[i + 1] == d[j + 1] || c4spr[i + 1] == d[j + 2] || c4spr[i + 1] == d[j + 3])
                        && (c4spr[i + 2] == d[j] || c4spr[i + 2] == d[j + 1] || c4spr[i + 2] == d[j + 2] || c4spr[i + 2] == d[j + 3])
                        && (c4spr[i + 3] == d[j] || c4spr[i + 3] == d[j + 1] || c4spr[i + 3] == d[j + 2] || c4spr[i + 3] == d[j + 3]))
                    {
                        tmp1++;
                    }
                }
                if (tmp1 >= cz)
                {
                    f4.Add(c4spr[i]);
                    f4.Add(c4spr[i + 1]);
                    f4.Add(c4spr[i + 2]);
                    f4.Add(c4spr[i + 3]);
                }
            }

            Console.WriteLine("Zbior F4= ");

            for (int i = 0; i < f4.Count; i += 4)
            {
                Console.WriteLine("{" + f4[i] + ", " + f4[i + 1] + ", " + f4[i + 2] + ", " + f4[i + 3] + "}");
            }

            if (f4.Count == 4)
            {
                Console.WriteLine("Zbiór F4 zawiera jeden element, warunek stopu został spełniony, kończymy algorytm Apriori");
            }
            else if (f4.Count == 0)
            {
                Console.WriteLine("Zbiór F4 nie zawiera elementu, warunek stopu nie został spełniony, kończymy algorytm Apriori");
            }
            else
            {
                Console.WriteLine("Zbiór F4 zawiera więcej niż jeden element, kontynujemy algorytm apriori");
            }

			////////////////////////////////////////////

			List<string> tmpf4 = new List<string>();
			
			float zgadzaf4 = 0;
			float ilef4 = 0;
			float wspf4 = 0;
			float ufnf4 = 0;

			List<string> a1f4 = new List<string>();
			List<string> a2f4 = new List<string>();
			List<string> a3f4 = new List<string>();
			List<string> a4f4 = new List<string>();

			for (int i = 0; i < f4.Count; i += 4)
			{
				tmpf4.Add(f4[i]);
				tmpf4.Add(f4[i+1]);
				tmpf4.Add(f4[i+2]);
				tmpf4.Add(f4[i+3]);

				tmpf4.Add(f4[i+1]);
				tmpf4.Add(f4[i+2]);
				tmpf4.Add(f4[i+3]);
				tmpf4.Add(f4[i]);

				tmpf4.Add(f4[i]);
				tmpf4.Add(f4[i+2]);
				tmpf4.Add(f4[i+3]);
				tmpf4.Add(f4[i+1]);

				tmpf4.Add(f4[i]);
				tmpf4.Add(f4[i+1]);
				tmpf4.Add(f4[i+3]);
				tmpf4.Add(f4[i+2]);
			}

			for (int i = 0; i < tmpf4.Count; i += 4)
			{
                zgadzaf4 = 0;
                ilef4 = 0;
                for (int j = 0; j < d.Count; j += 4)
				{
                    if ((tmpf4[i] == d[j] || tmpf4[i] == d[j+1] || tmpf4[i] == d[j+2] || tmpf4[i] == d[j+3]) &&
						(tmpf4[i+1] == d[j] || tmpf4[i+1] == d[j+1] || tmpf4[i+1] == d[j+2] || tmpf4[i+1] == d[j+3]) &&
						(tmpf4[i+2] == d[j] || tmpf4[i+2] == d[j+1] || tmpf4[i+2] == d[j+2] || tmpf4[i+2] == d[j+3]))
					{
						if ((tmpf4[i+3] == d[j] || tmpf4[i+3] == d[j+1] || tmpf4[i+3] == d[j+2] || tmpf4[i+3] == d[j+3]))
						{
							zgadzaf4++;
						}
						ilef4++;
					}
				}
                if(ilef4>0)
                {
                    wspf4 = ilef4 / 10;
                    ufnf4 = zgadzaf4 / ilef4;
                }
				
				if (wspf4 * ufnf4 >= 0.1)
				{
					a1f4.Add(tmpf4[i]);
					a1f4.Add(tmpf4[i+1]);
					a1f4.Add(tmpf4[i+2]);
					a1f4.Add(tmpf4[i+3]);
				}
				if (wspf4 * ufnf4 >= 0.2)
				{
					a2f4.Add(tmpf4[i]);
					a2f4.Add(tmpf4[i + 1]);
					a2f4.Add(tmpf4[i + 2]);
					a2f4.Add(tmpf4[i + 3]);
				}
				if (wspf4 * ufnf4 >= 0.3)
				{
					a3f4.Add(tmpf4[i]);
					a3f4.Add(tmpf4[i + 1]);
					a3f4.Add(tmpf4[i + 2]);
					a3f4.Add(tmpf4[i + 3]);
				}
				if (wspf4 * ufnf4 >= 0.4)
				{
					a4f4.Add(tmpf4[i]);
					a4f4.Add(tmpf4[i + 1]);
					a4f4.Add(tmpf4[i + 2]);
					a4f4.Add(tmpf4[i + 3]);
				}
			}

            /////////////////////////////

            List<string> tmpf3 = new List<string>();

            float zgadzaf3 = 0;
            float ilef3 = 0;
            float wspf3 = 0;
            float ufnf3 = 0;

            List<string> a1f3 = new List<string>();
            List<string> a2f3 = new List<string>();
            List<string> a3f3 = new List<string>();
            List<string> a4f3 = new List<string>();

            for (int i = 0; i < f3.Count; i += 3)
            {
                tmpf3.Add(f3[i]);
                tmpf3.Add(f3[i + 1]);
                tmpf3.Add(f3[i + 2]);

                tmpf3.Add(f3[i + 1]);
                tmpf3.Add(f3[i + 2]);
                tmpf3.Add(f3[i]);

                tmpf3.Add(f3[i]);
                tmpf3.Add(f3[i + 2]);
                tmpf3.Add(f3[i + 1]);
            }

            for (int i = 0; i < tmpf3.Count; i += 3)
            {
                zgadzaf3 = 0;
                ilef3 = 0;
                for (int j = 0; j < d.Count; j += 4)
                {
                    if ((tmpf3[i] == d[j] || tmpf3[i] == d[j + 1] || tmpf3[i] == d[j + 2] || tmpf3[i] == d[j + 3]) &&
                        (tmpf3[i + 1] == d[j] || tmpf3[i + 1] == d[j + 1] || tmpf3[i + 1] == d[j + 2] || tmpf3[i + 1] == d[j + 3]))
                    {
                        if ((tmpf3[i + 2] == d[j] || tmpf3[i + 2] == d[j + 1] || tmpf3[i + 2] == d[j + 2] || tmpf3[i + 2] == d[j + 3]))
                        {
                            zgadzaf3++;
                        }
                        ilef3++;
                    }
                }
                if(ilef3>0)
                {
                    wspf3 = ilef3 / 10;
                    ufnf3 = zgadzaf3 / ilef3;
                }
                if (wspf3 * ufnf3 >= 0.1)
                {
                    a1f3.Add(tmpf3[i]);
                    a1f3.Add(tmpf3[i + 1]);
                    a1f3.Add(tmpf3[i + 2]);
                }
                if (wspf3 * ufnf3 >= 0.2)
                {
                    a2f3.Add(tmpf3[i]);
                    a2f3.Add(tmpf3[i + 1]);
                    a2f3.Add(tmpf3[i + 2]);
                }
                if (wspf3 * ufnf3 >= 0.3)
                {
                    a3f3.Add(tmpf3[i]);
                    a3f3.Add(tmpf3[i + 1]);
                    a3f3.Add(tmpf3[i + 2]);
                }
                if (wspf3 * ufnf3 >= 0.4)
                {
                    a4f3.Add(tmpf3[i]);
                    a4f3.Add(tmpf3[i + 1]);
                    a4f3.Add(tmpf3[i + 2]);
                }
            }

            /////////////////////////

            List<string> tmpf2 = new List<string>();

            float zgadzaf2 = 0;
            float ilef2 = 0;
            float wspf2 = 0;
            float ufnf2 = 0;

            List<string> a1f2 = new List<string>();
            List<string> a2f2 = new List<string>();
            List<string> a3f2 = new List<string>();
            List<string> a4f2 = new List<string>();

            for (int i = 0; i < f2.Count; i += 2)
            {
                tmpf2.Add(f2[i]);
                tmpf2.Add(f2[i + 1]);

                tmpf2.Add(f2[i + 1]);
                tmpf2.Add(f2[i]);
            }

            for (int i = 0; i < tmpf2.Count; i += 2)
            {
                zgadzaf2 = 0;
                ilef2 = 0;
                for (int j = 0; j < d.Count; j += 4)
                {
                    if (tmpf2[i] == d[j] || tmpf2[i] == d[j + 1] || tmpf2[i] == d[j + 2] || tmpf2[i] == d[j + 3])
                    {
                        if (tmpf2[i + 1] == d[j] || tmpf2[i + 1] == d[j + 1] || tmpf2[i + 1] == d[j + 2] || tmpf2[i + 1] == d[j + 3])
                        {
                            zgadzaf2++;
                        }
                        ilef2++;
                    }
                }
                if(ilef2>0)
                {
                    wspf2 = ilef2 / 10;
                    ufnf2 = zgadzaf2 / ilef2;
                }
                
                if (wspf2 * ufnf2 >= 0.1)
                {
                    a1f2.Add(tmpf2[i]);
                    a1f2.Add(tmpf2[i + 1]);
                }
                if (wspf2 * ufnf2 >= 0.2)
                {
                    a2f2.Add(tmpf2[i]);
                    a2f2.Add(tmpf2[i + 1]);
                }
                if (wspf2 * ufnf2 >= 0.3)
                {
                    a3f2.Add(tmpf2[i]);
                    a3f2.Add(tmpf2[i + 1]);
                }
                if (wspf2 * ufnf2 >= 0.4)
                {
                    a4f2.Add(tmpf2[i]);
                    a4f2.Add(tmpf2[i + 1]);
                }
            }

            ////////////////

            Console.WriteLine("Po uwzglednieniu progu jakosci wsp * ufn >= 1/10, reguly o czestosci przynajmniej 2 maja postac: ");

            for (int i = 0; i < a1f4.Count; i += 4)
            {
                Console.WriteLine(a1f4[i] + " ^ " + a1f4[i + 1] + " ^ " + a1f4[i + 2] + " -> " + a1f4[i + 3]);
            }
            for (int i = 0; i < a1f3.Count; i += 3)
            {
                Console.WriteLine(a1f3[i] + " ^ " + a1f3[i + 1] + " -> " + a1f3[i + 2]);
            }
            for (int i = 0; i < a1f2.Count; i += 2)
            {
                Console.WriteLine(a1f2[i] + " -> " + a1f2[i + 1]);
            }

            Console.WriteLine("Po uwzglednieniu progu jakosci wsp * ufn >= 2/10, reguly o czestosci przynajmniej 2 maja postac: ");

            for (int i = 0; i < a2f4.Count; i += 4)
            {
                Console.WriteLine(a2f4[i] + " ^ " + a2f4[i + 1] + " ^ " + a2f4[i + 2] + " -> " + a2f4[i + 3]);
            }
            for (int i = 0; i < a2f3.Count; i += 3)
            {
                Console.WriteLine(a2f3[i] + " ^ " + a2f3[i + 1] + " -> " + a2f3[i + 2]);
            }
            for (int i = 0; i < a2f2.Count; i += 2)
            {
                Console.WriteLine(a2f2[i] + " -> " + a2f2[i + 1]);
            }

            Console.WriteLine("Po uwzglednieniu progu jakosci wsp * ufn >= 3/10, reguly o czestosci przynajmniej 2 maja postac: ");

            for (int i = 0; i < a3f4.Count; i += 4)
            {
                Console.WriteLine(a3f4[i] + " ^ " + a3f4[i + 1] + " ^ " + a3f4[i + 2] + " -> " + a3f4[i + 3]);
            }
            for (int i = 0; i < a3f3.Count; i += 3)
            {
                Console.WriteLine(a3f3[i] + " ^ " + a3f3[i + 1] + " -> " + a3f3[i + 2]);
            }
            for (int i = 0; i < a3f2.Count; i += 2)
            {
                Console.WriteLine(a3f2[i] + " -> " + a3f2[i + 1]);
            }

            Console.WriteLine("Po uwzglednieniu progu jakosci wsp * ufn >= 4/10, reguly o czestosci przynajmniej 2 maja postac: ");

            for (int i = 0; i < a4f4.Count; i += 4)
            {
                Console.WriteLine(a4f4[i] + " ^ " + a4f4[i + 1] + " ^ " + a4f4[i + 2] + " -> " + a4f4[i + 3]);
            }
            for (int i = 0; i < a4f3.Count; i += 3)
            {
                Console.WriteLine(a4f3[i] + " ^ " + a4f3[i + 1] + " -> " + a4f3[i + 2]);
            }
            for (int i = 0; i < a4f2.Count; i += 2)
            {
                Console.WriteLine(a4f2[i] + " -> " + a4f2[i + 1]);
            }





            Console.WriteLine();
          



			





            /****************** Koniec miejsca na rozwiązanie ********************************/
            Console.ReadKey();
        }
    }
}
