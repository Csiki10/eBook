using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBook
{
    class ListaElem<T>
    {
        public T Tartalom { get; set; }
        public ListaElem<T> Kovetkezo { get; set; }
    }
    class SajatLista<T> : IEnumerable<T>
    {
        private ListaElem<T> fej;
        public int hossz = 0;

        public void Add(T tartalom)
        {
            ListaElem<T> uj = new ListaElem<T>();
            uj.Tartalom = tartalom;
            uj.Kovetkezo = fej;
            fej = uj;
            hossz++;
        }
        public IOlvasmany BejarasIOlvasmany(int meddig)
        {
            // egy db a soron következő elemt ad vissza a listából
            int db = 0;
            ListaElem<T> p = fej;
            while (p != null && db != meddig)
            {
                db++;
                p = p.Kovetkezo;
            }
            if (db == meddig)
            {
                return p.Tartalom as IOlvasmany;
            }
            else
            {
                return null; ;
            }
        }

        // foreach es bejáráshoz:
        private IEnumerable<T> Tartalom
        {
            get
            {
                List<T> tmp = new List<T>();
                Bejaras(tmp);
                return tmp;
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            return Tartalom.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Tartalom.GetEnumerator();
        }
        public void Bejaras(List<T> tmp)
        {
            
            ListaElem<T> p = fej;
            while (p != null)
            {
                tmp.Add(p.Tartalom);
                p = p.Kovetkezo;
            }
        }

       
    }
}
