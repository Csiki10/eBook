namespace eBook
{
    public class Dia : IOlvasmany
    {
        public string Eloado { get; set; }
        public string Tananyag { get; set; }
        public string Datum { get; set; }

        public int Oldalszam { get; set; }
        public int Ertekeles { get; set; }
        public int Tarhely { get; set; }
        public int Kulcs { get; set; }

        public Dia(int kulcs, int oldalszam, int ertekeles, int tarhely, string eloado, string tananyag, string datum)
        {
            Eloado = eloado;
            Tananyag = tananyag;
            Datum = datum;
            Oldalszam = oldalszam;
            Ertekeles = ertekeles;
            Tarhely = tarhely;
            Kulcs = kulcs;
        }
    }
}

