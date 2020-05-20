using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Item
{

    public class Ficha
    {

        public int Id { get; set; }
        public string AutoPrd { get; set; }
        public string AutoDepartamento { get; set; }
        public string AutoGrupo { get; set; }
        public string AutoSubGrupo { get; set; }
        public string AutoTasaIva { get; set; }

        public string CodigoPrd { get; set; }
        public string NombrePrd { get; set; }
        public string Decimales { get; set; }
        public string Categoria { get; set; }
        public int DiasEmpaqueGarantia { get; set; }

        public decimal Cantidad { get; set; }
        public decimal CantidadUnd { get; set; }
        public string EmpCodigo { get; set; }
        public string EmpDescripcion { get; set; }
        public int EmpContenido { get; set; }
        public decimal PrecioSugerido { get; set; }
        public decimal PrecioNeto { get; set; }
        public string TarifaPrecio { get; set; }

        public decimal CostoVenta { get; set; }
        public decimal MontoNeto { get; set; }
        public decimal TasaImpuesto { get; set; }
        public decimal MontoImpuesto { get; set; }
        public decimal MontoTotal { get; set; }

        public decimal PrecioItem { get; set; }
        public decimal PrecioFinal { get; set; }

        public decimal CostoCompra { get; set; }
        public decimal CostoCompraUnd { get; set; }
        public decimal CostoPromedio { get; set; }
        public decimal CostoPromedioUnd { get; set; }

        public bool EsPesado { get; set; }
        public string TipoIva { get; set; }

    }

}