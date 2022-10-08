using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBook
{
    delegate void Esemeny(IOlvasmany olvasmany);
    static class Esemenyek
    {      
        public static void OlvasKiir(IOlvasmany olvasmany)
        {
            Console.Write("<OLVASÁS> Tetszett a(z) " + olvasmany.Kulcs + " azonosítójú olvasmány? \n > Válasz: ");
        }
        public static void ErtekelesValtozikKiir(IOlvasmany olvasmany)
        {
            Console.WriteLine(UzenetErtekeles(olvasmany));
        }
        public static void TorlesKiir(IOlvasmany olvasmany)
        {
            Console.WriteLine(UzenetTorles(olvasmany));
        }
        public static void UjOlvasmanyKiir(IOlvasmany olvasmany)
        {
            Console.WriteLine(UzenetUJOlvasmany(olvasmany));
        }

        static string UzenetUJOlvasmany(IOlvasmany olvasmany)
        {
            if (olvasmany is Dia)
            {
                Dia d = olvasmany as Dia;
                return ("\n<ÚJ OLVASMÁNY> Tananyag: " + d.Tananyag + ", \nMérete: " + d.Tarhely + ", Értékelése: " + d.Ertekeles + ", Kulcsa: " + d.Kulcs);
            }
            else if (olvasmany is Konyv)
            {
                Konyv v = olvasmany as Konyv;
                return ("\n<ÚJ OLVASMÁNY> Cím: " + v.Cim + ", \nMérete: " + v.Tarhely + ", Értékelése: " + v.Ertekeles + ", Kulcsa: " + v.Kulcs);
            }
            else
            {
                Sajat s = olvasmany as Sajat;
                return ("\n<ÚJ OLVASMÁNY> Név: " + s.Nev + ", \nMérete: " + s.Tarhely + ", Értékelése: " + s.Ertekeles + ", Kulcsa: " + s.Kulcs);
            }
        }
        static string UzenetErtekeles(IOlvasmany olvasmany)
        {
            if (olvasmany is Dia)
            {
                Dia d = olvasmany as Dia;
                return ("<ÉRTÉKELÉS> Megváltozott az értékelése, a(z): Tananyag: '" + d.Tananyag + "' című dokumentumnak.\nÚJ Értékelése: " + d.Ertekeles);
            }
            else if (olvasmany is Konyv)
            {
                Konyv v = olvasmany as Konyv;
                return ("<ÉRTÉKELÉS> Megváltozott az értékelése, a(z): Cím:'" + v.Cim + "'  dokumentumnak.\nÚJ Értékelése: " + v.Ertekeles);
            }
            else
            {
                Sajat s = olvasmany as Sajat;
                return ("<ÉRTÉKELÉS> Megváltozott az értékelése, a(z): Nev:'" + s.Nev + "' dokumentumnak.\nÚJ Értékelése: " + s.Ertekeles);
            }
        }
        static string UzenetTorles(IOlvasmany olvasmany)
        {
            if (olvasmany is Dia)
            {
                Dia d = olvasmany as Dia;
                return ($"<TÖRLÉS> Tananyag: {d.Tananyag}, Méret: { olvasmany.Tarhely}, Értékelés: { olvasmany.Ertekeles}, Kulcs: { olvasmany.Kulcs} dokumentum kitörlődött.");
            }
            else if (olvasmany is Konyv)
            {
                Konyv v = olvasmany as Konyv;
                return ($"<TÖRLÉS> Cím: { v.Cim}, Méret: { olvasmany.Tarhely}, Értékelés: { olvasmany.Ertekeles}, Kulcs: { olvasmany.Kulcs} dokumentum kitörlődött.");
            }
            else
            {
                Sajat s = olvasmany as Sajat;
                return ($"<TÖRLÉS> Nev: {s.Nev}, Méret: { olvasmany.Tarhely}, Értékelés: {olvasmany.Ertekeles}, Kulcs: {olvasmany.Kulcs} dokumentum kitörlődött. ");
            }

        }

    }
}
