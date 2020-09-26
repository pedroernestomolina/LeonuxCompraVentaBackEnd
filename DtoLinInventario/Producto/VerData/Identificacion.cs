using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.VerData
{
    
    public class Identificacion
    {

        public string auto { get; set; }
        public string autoDepartamento { get; set; }
        public string autoMarca { get; set; }
        public string autoGrupo { get; set; }

        public string codigo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string modelo { get; set; }
        public string referencia { get; set; }
        public int contenidoCompra { get; set; }
        public string empaqueCompra { get; set; }
        public string decimales { get; set; }
        public Enumerados.EnumOrigen origen { get; set; }
        public Enumerados.EnumCategoria categoria { get; set; }
        public Enumerados.EnumEstatus estatus { get; set; }
        public Enumerados.EnumAdministradorPorDivisa AdmPorDivisa { get; set; }
        public string departamento { get; set; }
        public string codigoDepartamento { get; set; }
        public string grupo { get; set; }
        public string codigoGrupo { get; set; }
        public string marca { get; set; }
        public decimal tasaIva { get; set; }
        public string nombreTasaIva { get; set; }
        public DateTime fechaAlta { get; set; }
        public DateTime fechaBaja { get; set; }
        public DateTime fechaUltActualizacion { get; set; }
        public string tipoABC { get; set; }
        public string comentarios { get; set; }
        public string advertencia { get; set; }
        public string presentacion { get; set; }

    }

}