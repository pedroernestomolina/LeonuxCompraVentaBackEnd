using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibVenta.Inventario.Producto
{

    public class Ficha
    {

        public string Auto { get; set; }
        public string AutoDepartamento { get; set; }
        public string AutoGrupo { get; set; }
        public string AutoSubGrupo { get; set; }
        public string AutoMarca { get; set; }
        public string AutoTasa { get; set; }

        public string CodigoPrd { get; set; }
        public string NombrePrd { get; set; }
        public string DescripcionPrd { get; set; }
        public string Referencia { get; set; }

        public string CodigoDepartamento { get; set; }
        public string NombreDepartamento { get; set; }

        public string CodigoGrupo { get; set; }
        public string NombreGrupo { get; set; }

        public string Marca { get; set; }
        public string Modelo { get; set; }
        public DateTime FechaUltVenta { get; set; }
        public DateTime FechaUltActPrecio { get; set; }
        public DateTime FechaUltActCosto { get; set; }
        public decimal TasaImpuesto { get; set; }

        public decimal Costo { get; set; }
        public decimal CostoPromedio { get; set; }
        public decimal CostoUnidad { get; set; }
        public decimal CostoPromedioUnidad { get; set; }

        public string NombreEmpCompra { get; set; }
        public decimal ContenidoEmpCompra { get; set; }
        public string Comentarios { get; set; }
        public decimal PrecioSugerido { get; set; }

        public bool IsActivo { get; set; }
        public bool IsDivisa { get; set; }
        public bool IsPesado { get; set; }
        public bool IsGarantia { get; set; }
        public bool IsSerial { get; set; }

        public decimal MontoDivisa { get; set; }
        public string PLU { get; set; }
        public int DiasGarantia { get; set; }
        public string Categoria { get; set; }
        public string Decimales { get; set; }

    }

}
