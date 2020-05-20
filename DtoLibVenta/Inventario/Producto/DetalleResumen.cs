using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibVenta.Inventario.Producto
{

    public class DetalleResumen
    {

        public string Auto { get; set; }
        public string CodigoPrd { get; set; }
        public string NombrePrd { get; set; }
        public string DescripcionPrd { get; set; }
        public string Referencia { get; set; }
        public string Departamento { get; set; }
        public string Grupo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public DateTime FechaUltVenta { get; set; }
        public DateTime FechaUltActPrecio { get; set; }
        public DateTime FechaUltActCosto { get; set; }
        public decimal TasaImpuesto { get; set; }
        public decimal CostoUnidad { get; set; }
        public string NombreEmpCompra { get; set; }
        public decimal ContenidoEmpCompra { get; set; }
        public string Comentarios { get; set; }
        public decimal PrecioSugerido { get; set; }
        public bool IsActivo { get; set; }
        public bool IsDivisa { get; set; }
        public bool IsPesado { get; set; }
        public decimal MontoDivisa { get; set; }
        public string PLU { get; set; }

    }

}