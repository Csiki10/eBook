using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBook
{
    interface IOlvasmany
    {
        int Oldalszam { get; set; }
        int Ertekeles { get; set; }
        int Tarhely { get; set; }
        int Kulcs { get; set; }
    }
}
