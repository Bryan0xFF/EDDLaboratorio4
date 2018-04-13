using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Laboratorio4_1252016_1053016.Models
{
    public class NumCalcomania : IComparable
    {  
        public string Pais;
        public int Num;

        public int CompareTo(object obj)
        {
            return this.Pais.CompareTo((string)obj);
        }
    }
}