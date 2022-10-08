using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace eBook
{ 
    class EBookTarolo : IEnumerable
    {
        class FaElem
        {
            public IOlvasmany tartalom; // ez egy dokumentum: dia, konyv, sajat          
            public FaElem bal;
            public FaElem jobb;
        }

        private FaElem gyoker;
       
        private int Hely = 150; // A tároló kapacitása
        public bool Vege = false; // A beolvasás végét jelzi
        public int DokTipus; // A feltöltendő dokumentum típusa
        public bool Elso = true; // Első beolvasás-e
        public int Kovetkezo; // A következő file indexe amit be kell olvasni

        
        public bool VanHely(int meret)
        {
            if (Hely - meret >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public void BeolvasFilebol(Esemeny uj, Esemeny torles, int holtart, int dokTipus)
        {
            Esemeny UjOlvasmany = uj;
            Esemeny TorlesDok = torles;
            string path = Directory.GetCurrentDirectory();
            string[] mappak = Directory.GetDirectories(path, "*.dok");
         
            if (Elso == true)// Első beolvasás exception nélkül 
            {
                Elso = false;
                int tipus = 0; //0 = dia, 1 = konyv, 2 = sajat;
                foreach (string mappa in mappak)
                {
                    if (tipus == 0)//diak
                    {
                        DiakBeolvas(UjOlvasmany, mappa, torles, holtart);
                    }
                    else if (tipus == 1)//konyv
                    {
                        KonyvBeolvas(UjOlvasmany, mappa, torles, holtart);
                    }
                    else//sajat
                    {
                        SajatBeolvas(UjOlvasmany, mappa, torles, holtart);
                    }
                    tipus++;
                }                
            }
            else// Volt exception már
            {
                // Megnézi hol volt a kivétel dobás
                if (dokTipus == 0)//diak
                {
                    DiakBeolvas(UjOlvasmany, "diak.dok", torles, holtart);
                    holtart = 0;
                    KonyvBeolvas(UjOlvasmany, "konyv.dok", torles, holtart);
                    holtart = 0;
                    SajatBeolvas(UjOlvasmany, "sajat.dok", torles, holtart);
                }
                else if (dokTipus == 1)//konyv
                {
                    KonyvBeolvas(UjOlvasmany, "konyv.dok", torles, holtart);
                    holtart = 0;
                    SajatBeolvas(UjOlvasmany, "sajat.dok", torles, holtart);
                }
                else//sajat
                {
                    SajatBeolvas(UjOlvasmany, "sajat.dok", torles, holtart);
                }               
            }
            Vege = true;
            ;
        }
               
        public void DiakBeolvas(Esemeny UjOlvasmany, string mappa, Esemeny torles,int holtart)
        {
            string[] files = Directory.GetFiles(mappa, "*.txt");

            if (holtart == 0)//simán mehet előlről
            {
                for (int i = 0; i < files.Length; i++)
                {
                    string[] sorok = File.ReadAllLines(files[i]);
                    Dia Uj = new Dia(
                        int.Parse(sorok[0]),
                        int.Parse(sorok[1]),
                        int.Parse(sorok[2]),
                        int.Parse(sorok[3]),
                        sorok[4],
                        sorok[5],
                        sorok[6]);

                    if (VanHely(Uj.Tarhely))
                    {
                        Beszuras(ref gyoker, Uj, Uj.Kulcs, UjOlvasmany);
                        holtart++;
                    }
                    else
                    {
                        holtart++;
                        DokTipus = 0;
                        this.Kovetkezo = holtart;
                        throw new NincsHelyexception(Uj, holtart);
                    }
                }
            }
            else// Onnan folytatja ahol a kivétel keletkezett
            {
                while (holtart < files.Length)
                {
                    string[] sorok = File.ReadAllLines(files[holtart]);
                    Dia Uj = new Dia(
                        int.Parse(sorok[0]),
                        int.Parse(sorok[1]),
                        int.Parse(sorok[2]),
                        int.Parse(sorok[3]),
                        sorok[4],
                        sorok[5],
                        sorok[6]);

                    if (VanHely(Uj.Tarhely))
                    {
                        Beszuras(ref gyoker, Uj, Uj.Kulcs, UjOlvasmany);
                        holtart++;
                    }
                    else// nincs elég hely
                    {
                        holtart++;
                        DokTipus = 0;
                        this.Kovetkezo = holtart; // eltároljuk a következő indexet ahonnan folytatni kell a beolvasást
                        throw new NincsHelyexception(Uj, holtart);
                    }
                }
            }  
        }
        public void KonyvBeolvas(Esemeny UjOlvasmany, string mappa, Esemeny torles,int holtart)
        {
            string[] files = Directory.GetFiles(mappa, "*.txt");

            if (holtart == 0)//simán mehet előlről
            {
                for (int i = 0; i < files.Length; i++)
                {
                    string[] sorok = File.ReadAllLines(files[i]);
                    Konyv Uj = new Konyv(
                        int.Parse(sorok[0]),
                        int.Parse(sorok[1]),
                        int.Parse(sorok[2]),
                        int.Parse(sorok[3]),
                        sorok[4],
                        sorok[5],
                        sorok[6]);

                    if (VanHely(Uj.Tarhely))
                    {
                        Beszuras(ref gyoker, Uj, Uj.Kulcs, UjOlvasmany);
                        holtart++;
                    }
                    else
                    {
                        holtart++;
                        this.Kovetkezo = holtart;
                        DokTipus = 1;
                        throw new NincsHelyexception(Uj, holtart);
                    }
                }
            }
            else// Onnan folytatja ahol a kivétel keletkezett
            {
                while (holtart < files.Length)
                {
                    string[] sorok = File.ReadAllLines(files[holtart]);
                    Konyv Uj = new Konyv(
                        int.Parse(sorok[0]),
                        int.Parse(sorok[1]),
                        int.Parse(sorok[2]),
                        int.Parse(sorok[3]),
                        sorok[4],
                        sorok[5],
                        sorok[6]);

                    if (VanHely(Uj.Tarhely))
                    {
                        Beszuras(ref gyoker, Uj, Uj.Kulcs, UjOlvasmany);
                        holtart++;
                    }
                    else
                    {
                        holtart++;
                        this.Kovetkezo = holtart;
                        DokTipus = 1;
                        throw new NincsHelyexception(Uj, holtart);
                    }
                }
            }                      
        }
        public void SajatBeolvas(Esemeny UjOlvasmany, string mappa, Esemeny torles, int holtart)
        {
            string[] files = Directory.GetFiles(mappa, "*.txt");
            if (holtart == 0)// előlről mehet
            {
                for (int i = 0; i < files.Length; i++)
                {
                    string[] sorok = File.ReadAllLines(files[i]);
                    Sajat Uj = new Sajat(
                        int.Parse(sorok[0]),
                        int.Parse(sorok[1]),
                        int.Parse(sorok[2]),
                        sorok[3]);

                    if (VanHely(Uj.Tarhely))
                    {
                        Beszuras(ref gyoker, Uj, Uj.Kulcs, UjOlvasmany);
                        holtart++;
                    }
                    else
                    {
                        holtart++;
                        this.Kovetkezo = holtart;
                        DokTipus = 2;
                        throw new NincsHelyexception(Uj, holtart);
                    }
                }
            }
            else// Onnan folytatja ahol a kivétel keletkezett
            {
                while (holtart < files.Length)
                {
                    string[] sorok = File.ReadAllLines(files[holtart]);
                    Sajat Uj = new Sajat(
                        int.Parse(sorok[0]),
                        int.Parse(sorok[1]),
                        int.Parse(sorok[2]),
                        sorok[3]);

                    if (VanHely(Uj.Tarhely))
                    {
                        Beszuras(ref gyoker, Uj, Uj.Kulcs, UjOlvasmany);
                        holtart++;
                    }
                    else// nincs elég hely
                    {
                        holtart++;
                        this.Kovetkezo = holtart;
                        DokTipus = 2;
                        throw new NincsHelyexception(Uj, holtart);
                    }
                }
            }                       
        }

        public void Helycsialo(IOlvasmany uJElem, Esemeny torles, Esemeny UjOlvasmany)
        {
            ;
            int ujMerete = uJElem.Tarhely;// A feltöltendő elem mérete        
            int kelloMeret = ujMerete - Hely;// Mennyi Hely kell még, hogy fel lehessen tölteni
            
            SajatLista<int> MarVolt = new SajatLista<int>();// Már vizsgált legkisebb értékelések

            int osszeg = 0;// A legkisebb értékelésű dokumentumok tárhelyének az összege
            int torloOsszeg = 0;
            SajatLista<int> torlendoKulcsok = new SajatLista<int>();// A kitörlendő dokumentumok kulcsai

            bool eleg = false;// Van-e elég Hely az új elem feltöltéséhez
            while (eleg == false)
            {
                int legKisebbErtekels = LegKisebbErtekeles(MarVolt);
                MarVolt.Add(legKisebbErtekels);

                SajatLista<IOlvasmany> talanTorlendok = LehetsegesTorlendo(legKisebbErtekels); // Azon elemek amiknek jelenleg a legkisebb az értékelése
                
                int i = 0;
                while (eleg == false && i < talanTorlendok.hossz) // Végigmegyünk a talántörlendő elemeken
                {
                    IOlvasmany o = talanTorlendok.BejarasIOlvasmany(i);
                    osszeg += o.Tarhely;
                    torloOsszeg += o.Tarhely;
                    torlendoKulcsok.Add(o.Kulcs);

                    if (osszeg >= kelloMeret)
                    {
                        eleg = true;
                    }
                    i++;
                }
            }
            ;
            TorlesIntezo(torlendoKulcsok, torles);           
            ;
            Beszuras(ref gyoker,uJElem,uJElem.Kulcs, UjOlvasmany);          
        }
        private void TorlesIntezo(SajatLista<int> torlendoKulcsok, Esemeny torles)
        {
            foreach (int kulcs in torlendoKulcsok)
            {
                Torles(ref gyoker, kulcs, torles);
            }
        }
        private SajatLista<IOlvasmany> LehetsegesTorlendo(int legKisebbErtekeles)
        {
            // Kiválogatja azokat az olvasmányoka, ahol az értékelés egyezik a legkisennel
            SajatLista<IOlvasmany> lehetsegesTorlendo = new SajatLista<IOlvasmany>();
            foreach (IOlvasmany item in this)
            {
                if (item.Ertekeles == legKisebbErtekeles)
                {
                    lehetsegesTorlendo.Add(item);
                }
            }
            return lehetsegesTorlendo;
        }
        private int LegKisebbErtekeles(SajatLista<int> MarVolt)
        {
            // Megkeresi a legkisebb értékelésű elemet ami még nem volt.
            int legkisebbErt = int.MaxValue;
            foreach (IOlvasmany item in this)
            {
                if (item.Ertekeles < legkisebbErt && !MarVolt.Contains(item.Ertekeles))
                {
                    legkisebbErt = item.Ertekeles;
                }
            }
            return legkisebbErt;
        }

        private void Torles(ref FaElem elem, int kulcs, Esemeny torles)
        {
            if (elem != null)
            {
                if (elem.tartalom.Kulcs > kulcs)
                {
                    Torles(ref elem.bal, kulcs, torles);
                }
                else if (elem.tartalom.Kulcs < kulcs)
                {
                    Torles(ref elem.jobb, kulcs, torles);
                }
                else
                {
                    if (elem.bal == null)//csak jobbra van tőle gyerek
                    {
                        Hely += elem.tartalom.Tarhely;
                        FaElem torlendo = elem;
                        elem = elem.jobb;
                        torles?.Invoke(torlendo.tartalom);
                        torlendo = null;   
                    }
                    else if (elem.jobb == null)//csak balra van tőle gyerek
                    {
                        Hely += elem.tartalom.Tarhely;
                        FaElem torlendo = elem;
                        elem = elem.bal;
                        torles?.Invoke(torlendo.tartalom);
                        torlendo = null;
                    }
                    else//jobbra és balra is van gyerek
                    {
                        TorlesKetGyerek(elem, ref elem.bal, torles);
                    }
                }
            }
            else
            {
                throw new Exception("Nem létezik a törlendő elem");
            }

        }
        private void TorlesKetGyerek(FaElem elem, ref FaElem r, Esemeny torles)
        {
            if (r.jobb != null)
            {
                TorlesKetGyerek(elem, ref r.jobb, torles);
            }
            else
            {
                Hely += elem.tartalom.Tarhely;
                elem.tartalom = r.tartalom;
                elem.tartalom.Kulcs = r.tartalom.Kulcs;
                FaElem q = r;
                r = r.bal;
                torles?.Invoke(q.tartalom);
                q = null;
                ;
            }
        }
              
        private void Beszuras(ref FaElem p, IOlvasmany tartalom, int kulcs, Esemeny UjOlvasmany)
        {
            if (p == null)
            {
                p = new FaElem();
                p.tartalom = tartalom;
                UjOlvasmany?.Invoke(tartalom);
                Hely -= tartalom.Tarhely;
            }
            else
            {
                if (p.tartalom.Kulcs < kulcs )
                {
                    //jobb részfába szúrunk be
                    Beszuras(ref p.jobb, tartalom, kulcs,  UjOlvasmany);       
                }
                else if (p.tartalom.Kulcs > kulcs)
                {
                    //bal részfába szúrunk be
                    Beszuras(ref p.bal, tartalom, kulcs,  UjOlvasmany);           
                }
                else
                {
                    Vege = true;
                    throw new MarVanIlyenException(tartalom as IOlvasmany);                  
                }               
            }           
        }       
        private IOlvasmany Kereses(FaElem elem ,int id)
        {
            if (elem != null)
            {
                if (elem.tartalom.Kulcs > id)
                {
                    return Kereses(elem.bal, id);
                }
                else if(elem.tartalom.Kulcs < id)
                {
                    return Kereses(elem.jobb, id);
                }
                else
                {
                    return elem.tartalom;
                }
            }
            else
            {
                throw new NincsIlyenElemException(id);
            }
        }
        
        public void Olvas(int id, Esemeny OlvasKiir, Esemeny ErtekelesValtozikKiir)
        {
            IOlvasmany keresett = Kereses(gyoker, id);

            Esemeny olvasas = OlvasKiir;
            Esemeny ertekeles = ErtekelesValtozikKiir;

            olvasas(keresett);
            if (Valasz())
            {               
                keresett.Ertekeles++;
                ertekeles(keresett);

            }
            else
            {
                if (keresett.Ertekeles > 1)
                {
                    keresett.Ertekeles--;
                    ertekeles(keresett);
                }
                else
                {
                    throw new Exception("<EXCEPTION> A dokumentum értékelése jelenleg a megengedett minimum, azaz 1. Nem lehet az értékelése '0' -a.");
                }
                
            }

        }
        private bool Valasz()
        {
            if (int.Parse(Console.ReadLine()) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // foreach hez
        private void Bejaras(List<IOlvasmany> lista, FaElem p)
        {
            if (p != null)
            {
                Bejaras(lista, p.bal);
                lista.Add(p.tartalom);
                Bejaras(lista, p.jobb);
            }
        }
        private IEnumerable Tartalom
        {
            get
            {
                List<IOlvasmany> tmp = new List<IOlvasmany>();
                Bejaras(tmp, gyoker);
                return tmp;
            }
        }
        public IEnumerator GetEnumerator()
        {
            return Tartalom.GetEnumerator();
        }
    }
}
