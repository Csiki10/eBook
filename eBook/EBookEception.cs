using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBook
{   
    class MarVanIlyenException : Exception
    {
        IOlvasmany Olvasmany { get; set; }
        public string Uz { get; set; }
        public MarVanIlyenException(IOlvasmany olvasmany)
        {
            this.Olvasmany = olvasmany;
            string uzenet = "";

            if (olvasmany is Dia)
            {
                Dia d = olvasmany as Dia;
                uzenet += "----------------------------------------------------------------------------------------------\n" +
                    "<EXCEPTION> A feltöltendő dokumentum már megtalálható: Előadó: " + d.Eloado + " Tananyag:" +d.Tananyag+ " nem tölthető fel.";
            }
            else if (olvasmany is Konyv)
            {
                Konyv k = olvasmany as Konyv;
                uzenet += "----------------------------------------------------------------------------------------------\n "+
                    "<EXCEPTION> A feltöltendő dokumentum már megtalálható: Cim: " + k.Cim + " Szerző: " + k.Szerzo + " nem tölthető fel.";
            }
            else
            {
                Sajat s = olvasmany as Sajat;
                uzenet += "----------------------------------------------------------------------------------------------\n "+
                    "<EXCEPTION> A feltöltendő dokumentum már megtalálható: Nev: " + s.Nev + " nem tölthető fel.";
            }
            this.Uz = uzenet;
        }      
    }
    class NincsHelyexception : Exception
    {
        public IOlvasmany Olvasmany { get; set; }
        public int Holtart { get; set; }
        public string Uz { get; set; }
        public NincsHelyexception(IOlvasmany olvasmany,int holtart) 
        {
            this.Olvasmany = olvasmany;           
            string uzenet = "";
            Holtart = holtart;
            if (olvasmany is Dia)
            {
                Dia d = olvasmany as Dia;
                uzenet += "----------------------------------------------------------------------------------------------\n" +
                    "<EXCEPTION> Nincs több Hely a tárolón! a(z): Előadó: " + d.Eloado + ", Tananyag: " + d.Tananyag + " nem tölthető fel.";
            }
            else if (olvasmany is Konyv)
            {
                Konyv k = olvasmany as Konyv;
                uzenet += "----------------------------------------------------------------------------------------------\n" +
                    "<EXCEPTION> Nincs több Hely a tárolón! a(z): Cim: " + k.Cim + ", Szerző: " + k.Szerzo + " nem tölthető fel.";
            }
            else
            {
                Sajat s = olvasmany as Sajat;
                uzenet += "----------------------------------------------------------------------------------------------\n" +
                    "<EXCEPTION> Nincs több Hely a tárolón! a(z): Nev: " + s.Nev + " nem tölthető fel.";
            }
            this.Uz = uzenet;
        }
    }    
    class NincsIlyenElemException : Exception
    {
        public int Id { get; set; }
        public NincsIlyenElemException(int id) :base($"----------------------------------------------------------------------------------------------\n<EXCEPTION> A(z) {id} azonosítójú elem nem található! ")
        {
            Id = id;
        }
    }
}
