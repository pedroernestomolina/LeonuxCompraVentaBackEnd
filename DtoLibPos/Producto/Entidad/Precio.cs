using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Producto.Entidad
{
    
    public class Precio
    {

        public decimal Neto { get; set; }
        public int Contenido { get; set; }
        public string Decimales { get; set; }
        public string Empaque { get; set; }


        public Precio()
        {
            Neto = 0.0m;
            Contenido = 0;
            Decimales = "";
            Empaque = "";
        }

    }

}