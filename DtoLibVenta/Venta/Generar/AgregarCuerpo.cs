using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibVenta.Venta
{

    public class AgregarCuerpo
    {

        public string AutoProducto { get; set; }
        public string AutoDepartamento { get; set; }
        public string AutoGrupo { get; set; }
        public string AutoSubGrupo { get; set; }
        public string AutoTasaImpuesto { get; set; }
        public string AutoDeposito { get; set; }
        public string AutoVendedor { get; set; }
        public string AutoCliente { get; set; }

        public string CodigoPrd { get; set; }
        public string NombrePrd { get; set; }
        public bool IsSerial { get; set; }
        public bool IsGarantia { get; set; }
        public int DiasGarantia { get; set; }
        public string Categoria { get; set; }

        public string Decimales { get; set; }
        public decimal Cantidad { get; set; }
        public decimal CantidadUnd { get; set; }
        public string Empaque { get; set; }
        public int EmpaqueContenido { get; set; }
        public decimal PrecioNeto { get; set; }
        public decimal PrecioFinal { get; set; }
        public decimal PrecioUnd { get; set; }
        public decimal PrecioItem { get; set; }
        public decimal DescuentoPorc_1 { get; set; }
        public decimal DescuentoPorc_2 { get; set; } 
        public decimal DescuentoPorc_3 { get; set; }
        public decimal DescuentoMonto_1 { get; set; }
        public decimal DescuentoMonto_2 { get; set; }
        public decimal DescuentoMonto_3 { get; set; }

        public string TarifaPrecio { get; set; }
        public decimal MontoDescuento { get; set; }

        public decimal CostoPromedioCompra { get; set; }
        public decimal CostoPromedioUnd { get; set; }

        public decimal CostoUnd { get; set; }
        public decimal CostoVenta { get; set; }
        public decimal TotalNeto { get; set; }
        public decimal TasaIva { get; set; }
        public decimal MontoImpuesto { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal UtilidadMonto { get; set; }
        public decimal UtilidadPorc { get; set; }

        public string Notas { get; set; }

        public string DepositoCodigo { get; set; }
        public string DepositoDescripcion { get; set; }
        public string VendedorCodigo { get; set; }
        public decimal TotalItem { get; set; }
        public string Tipo { get; set; }
        public int Signo { get; set; }

    }

}