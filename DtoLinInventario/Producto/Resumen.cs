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
        public string marca { get; set; }
        public string modelo { get; set; }
        public string referencia { get; set; }
        public Enumerados.EnumCategoria categoria { get; set; }
        public Enumerados.EnumOrigen origen { get; set; }

        public int contenido { get; set; }
        public string empaque { get; set; }
        public decimal tasaIva { get; set; }
        public string tasaIvaDescripcion { get; set; }
        public Enumerados.EnumEstatus estatus { get; set; }
        public Enumerados.EnumAdministradorPorDivisa admPorDivisa { get; set; }
        public Enumerados.EnumPesado esPesado { get; set; }
        public Enumerados.EnumOferta enOferta { get; set; }

        public DateTime? fechaAlta { get; set; }
        public DateTime? fechaUltCambioCosto { get; set; }
        public DateTime? fechaUltActualizacion { get; set; }

    }

}