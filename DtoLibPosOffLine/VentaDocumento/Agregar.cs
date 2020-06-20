using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.VentaDocumento
{
    
    public class Agregar
    {

        public int IdJornada { get; set; }
        public int IdOperador { get; set; }

        public string AutoUsuario { get; set; }
        public string UsuarioCodigo { get; set; }
        public string UsuarioDescripcion { get; set; }
        public string Estacion { get; set; }

        public string HoraEmision { get; set; }
        public string Serie { get; set; }
        public string Documento { get; set; }
        public string Control { get; set; }
        public string Aplica { get; set; }
        public int TipoDocumento { get; set; }
        public int EstatusDocumento { get; set; }
        public int SignoDocumento { get; set; }
        public int Renglones { get; set; }
        public string CodigoSucursal { get; set; }
        public string Prefijo { get; set; }

        public int ClienteId { get; set; }
        public string ClienteCiRif { get; set; }
        public string ClienteNombreRazonSocial { get; set; }
        public string ClienteDirFiscal { get; set; }
        public string ClienteTelefono { get; set; }

        public decimal MontoExento { get; set; }
        public decimal MontoBase { get; set; }
        public decimal MontoImpuesto { get; set; }
        public decimal MontoTotal { get; set; }

        public decimal MontoBase_1 { get; set; }
        public decimal MontoBase_2 { get; set; }
        public decimal MontoBase_3 { get; set; }
        public decimal MontoIva_1 { get; set; }
        public decimal MontoIva_2 { get; set; }
        public decimal MontoIva_3 { get; set; }
        public decimal TasaIva_1 { get; set; }
        public decimal TasaIva_2 { get; set; }
        public decimal TasaIva_3 { get; set; }

        public decimal MontoDescuento_1 { get; set; }
        public decimal MontoDescuento_2 { get; set; }
        public decimal MontoCargo_1 { get; set; }
        public decimal PorcDescuento_1 { get; set; }
        public decimal PorcDescuento_2 { get; set; }
        public decimal PorcCargo_1 { get; set; }

        public decimal FactorCambio { get; set; }
        public decimal MontoDivisa { get; set; }

        public decimal MontoSubTotalNeto { get; set; }
        public decimal MontoSubTotalImpuesto { get; set; }
        public decimal MontoSubTotal { get; set; }

        public decimal MontoVentaNeta { get; set; }
        public decimal MontoCostoVenta { get; set; }
        public decimal MontoUtilidad { get; set; }
        public decimal PorcUtilidad { get; set; }

        public string AutoVendedor { get; set; }
        public string CodigoVendedor { get; set; }
        public string NombreVendedor { get; set; }

        public string AutoDeposito{ get; set; }
        public string CodigoDeposito { get; set; }
        public string DescripcionDeposito { get; set; }

        public string AutoCobrador { get; set; }
        public string CodigoCobrador { get; set; }
        public string NombreCobrador { get; set; }

        public string AutoTransporte { get; set; }
        public string CodigoTransporte { get; set; }
        public string NombreTransporte { get; set; }

        public string IsCredito { get; set; }
        public decimal MontoRecibido { get; set; }
        public decimal CambioDar { get; set; }

        public string Tarifa { get; set; }
        public decimal SaldoPendiente { get; set; }

        public string AutoConceptoMov { get; set; }
        public string CodigoConceptoMov { get; set; }
        public string NombreConceptoMov { get; set; }

        public List<AgregarItem> Items { get; set; }
        public List<AgregarMetodoPago> MetodosPago { get; set; }
        public List<AgregarItemLimpiar> ItemsLimpiar { get; set; }

    }

}