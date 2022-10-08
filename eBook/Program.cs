using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace eBook
{

    class Program
    {               
        static void Main(string[] args)
        {               
            Futas();
            ;
            Console.ReadKey();
        }        
        static void Futas()
        {
            EBookTarolo tarolo = new EBookTarolo();

            Console.WriteLine("__________DOKUMENTUM FELTÖLTÉSE__________");
            Muveletek.Beolvas(tarolo);
            ;

            try
            {
                Console.WriteLine("\n__________OLVASÁS__________");
                Muveletek.Olvasas(tarolo);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            
            Console.WriteLine("\n__________ÚJ DOKUMENTUM FELTÖLTÉSE__________");
            Muveletek.UjDokumentumBeolvasas(tarolo,Esemenyek.TorlesKiir,Esemenyek.UjOlvasmanyKiir,tarolo.Kovetkezo);

            try
            {
                Console.WriteLine("\n__________OLVASÁS__________");
                Muveletek.Olvasas(tarolo);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            ;
            Console.WriteLine("\n__________PROGRAM VÉGE__________");
        }        
    }
}

