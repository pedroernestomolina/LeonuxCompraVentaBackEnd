using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.VerData
{
    
    public class Precio
    {

        public string codigo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string nombreTasaIva { get; set; }
        public decimal tasaIva { get; set; }

        public string etiqueta1 { get; set; }
        public string etiqueta2 { get; set; }
        public string etiqueta3 { get; set; }
        public string etiqueta4 { get; set; }
        public string etiqueta5 { get; set; }

        public decimal precioNeto1 { get; set; }
        public decimal precioNeto2 { get; set; }
        public decimal precioNeto3 { get; set; }
        public decimal precioNeto4 { get; set; }
        public decimal precioNeto5 { get; set; }

        public string empaque1 { get; set; }
        public string empaque2 { get; set; }
        public string empaque3 { get; set; }
        public string empaque4 { get; set; }
        public string empaque5 { get; set; }

        public int contenido1 { get; set; }
        public int contenido2 { get; set; }
        public int contenido3 { get; set; }
        public int contenido4 { get; set; }
        public int contenido5 { get; set; }

        public decimal utilidad1 { get; set; }
        public decimal utilidad2 { get; set; }
        public decimal utilidad3 { get; set; }
        public decimal utilidad4 { get; set; }
        public decimal utilidad5 { get; set; }

        public decimal precioSugerido { get; set; }

        public Enumerados.EnumOferta estatusOferta { get; set; }
        public Enumerados.EnumOferta ofertaActiva { get; set; }
        public decimal precioOferta { get; set; }
        public DateTime inicioOferta { get; set; }
        public DateTime finOferta { get; set; }

        public decimal precioFullDivisa1 { get; set; }
        public decimal precioFullDivisa2 { get; set; }
        public decimal precioFullDivisa3 { get; set; }
        public decimal precioFullDivisa4 { get; set; }
        public decimal precioFullDivisa5 { get; set; }

    }

}