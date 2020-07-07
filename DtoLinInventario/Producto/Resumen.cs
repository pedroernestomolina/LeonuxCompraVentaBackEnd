using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto
{

    public class Resumen
    {

        public string auto { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string departamento { get; set; }
        public string grupo { get; set; }
        public string modelo { get; set; }
        public string referencia { get; set; }
        public Enumerados.EnumCategoria categoria { get; set; }

        public int contenido { get; set; }
        public string empaque { get; set; }
        public decimal tasaIva { get; set; }
        public Enumerados.EnumEstatus estatus { get; set; }
        public Enumerados.EnumAdministradorPorDivisa AdmPorDivisa { get; set; }

    }

}