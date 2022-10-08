namespace eBook
{
    public class Sajat : IOlvasmany
    {
        public string Nev { get; set; }


        public int Oldalszam { get; set; }
        public int Tarhely { get; set; }
        public int Ertekeles { get; set; }
        public int Kulcs { get; set; }

        public Sajat(int kulcs, int oldalszam, int tarhely, string nev)
        {
            this.Kulcs = kulcs;
            Nev = nev;
            Oldalszam = oldalszam;
            Tarhely = tarhely;
            Ertekeles = 5;

        }
    }
}

