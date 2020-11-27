using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Producto.Data
{
    
    public class Ficha
    {

        public string auto { get; set; }
        public string autoDepartamento { get; set; }
        public string autoMarca { get; set; }
        public string autoGrupo { get; set; }
        public string autoTasa { get; set; }
        public string autoSubGrupo { get; set; }

        public string codigo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string modelo { get; set; }
        public string referencia { get; set; }
        public int contenidoCompra { get; set; }
        public string empaqueCompra { get; set; }
        public string decimales { get; set; }
        public string origen { get; set; }
        public string categoria { get; set; }
        public Enumerados.EnumEstatus estatus { get; set; }
        public Enumerados.EnumAdministradorPorDivisa AdmPorDivisa { get; set; }
        public string departamento { get; set; }
        public string codigoDepartamento { get; set; }
        public string grupo { get; set; }
        public string codigoGrupo { get; set; }
        public string marca { get; set; }
        public decimal tasaIva { get; set; }
        public string nombreTasaIva { get; set; }

        public decimal costoDivisa { get; set; }
        public decimal costo { get; set; }
        public DateTime? fechaUltCambio { get; set; }

    }

}