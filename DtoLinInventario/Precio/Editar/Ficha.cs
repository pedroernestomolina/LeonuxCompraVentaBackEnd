using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Precio.Editar
{
    
    public class Ficha
    {

        public string autoProducto { get; set; }
        public string estacion { get; set; }
        public string autoUsuario { get; set; }
        public string codigoUsuario { get; set; }
        public string nombreUsuario { get; set; }

        public FichaPrecio precio_1 { get; set; }
        public FichaPrecio precio_2 { get; set; }
        public FichaPrecio precio_3 { get; set; }
        public FichaPrecio precio_4 { get; set; }
        public FichaPrecio precio_5 { get; set; }

        public List<FichaHistorica> historia { get; set; }

    }

}