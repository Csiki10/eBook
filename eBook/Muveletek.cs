using System;

namespace eBook
{
    static class Muveletek
    {            
        public static void Beolvas(EBookTarolo tarolo)
        {
            while (tarolo.Vege == false)
            {
                try
                {
                    tarolo.BeolvasFilebol(Esemenyek.UjOlvasmanyKiir, Esemenyek.TorlesKiir, tarolo.Kovetkezo, tarolo.DokTipus);
                }
                catch (MarVanIlyenException e)
                {
                    Console.WriteLine(e.Uz);
                }
                catch (NincsHelyexception f)
                {
                    Console.WriteLine(f.Uz);
                    tarolo.Helycsialo(f.Olvasmany, Esemenyek.TorlesKiir, Esemenyek.UjOlvasmanyKiir);
                }
                catch (Exception g)
                {
                    Console.WriteLine(g.Message);
                }
            }
            tarolo.Kovetkezo = 0;
            tarolo.Elso = true;
            tarolo.Vege = false;
        }
        public static void UjDokumentumBeolvasas(EBookTarolo tarolo, Esemeny torles, Esemeny UjOlvasmany, int holtart)
        {
            Console.WriteLine("(0 = nem / 1 = igen)");
            bool mehet = false;
            while (mehet == false)
            {
                Console.Write("\nSzeretnél dokumentumokat feltölteni? \n > Válasz: ");
                int valasz = int.Parse(Console.ReadLine());
                if (valasz == 1)
                {
                    Beolvas(tarolo);
                }
                else
                {
                    mehet = true;
                }               
            }
        }
        public static void Olvasas(EBookTarolo tarolo)
        {
            Console.WriteLine("(0 = nem / 1 = igen)");
            bool mehet = false;
            while (mehet == false)
            {
                Console.Write("\nSzeretnél dokumentumot olvasni? \n > Válasz: ");
                int valasz = int.Parse(Console.ReadLine());
                if (valasz == 1)
                {
                    Console.Write("Add meg az olvasni kívánt dokumentum kulcsát (pl.: 5) \n > Válasz: ");
                    int kulcs = int.Parse(Console.ReadLine());
                    tarolo.Olvas(kulcs, Esemenyek.OlvasKiir, Esemenyek.ErtekelesValtozikKiir);
                }
                else
                {
                    mehet = true;
                }
                
            }
        }
    }
}