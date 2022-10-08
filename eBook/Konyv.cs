namespace eBook
{
    public class Konyv : IOlvasmany
    {
        public string Szerzo { get; set; }
        public string Cim { get; set; }
        public string Szereplok { get; set; }


        public int Oldalszam { get; set; }
        public int Ertekeles { get; set; }
        public int Tarhely { get; set; }
        public int Kulcs { get; set; }

        public Konyv(int kulcs, int oldalszam, int ertekeles, int tarhely, string szerzo, string cim, string szereplok)
        {
            Szerzo = szerzo;
            Cim = cim;
            Szereplok = szereplok;
            Oldalszam = oldalszam;
            Ertekeles = ertekeles;
            Tarhely = tarhely;
            Kulcs = kulcs;
        }
    }
}

