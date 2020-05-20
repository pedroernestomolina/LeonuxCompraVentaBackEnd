using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibVenta.Venta
{
    
    public class Agregar
    {

        public string AutoCliente { get; set; }
        public string AutoVendedor { get; set; }
        public string AutoCobrador { get; set; }
        public string AutoUsuario { get; set; }
        public string AutoTransporte { get; set; }

        public string ClienteCiRif { get; set; }
        public string ClienteCodigo { get; set; }
        public string ClienteNombre { get; set; }
        public string ClienteDirFiscal { get; set; }
        public string ClienteTelefono { get; set; }
        public string ClienteDenominacionFiscal { get; set; }
        public string ClienteTarifa { get; set; }

        public string VendedorCodigo { get; set; }
        public string VendedorNombre { get; set; }

        public string CobradorCodigo { get; set; }
        public string CobradorNombre { get; set; }


        public string UsuarioCodigo { get; set; }
        public string UsuarioNombre { get; set; }

        public string TransporteCodigo { get; set; }
        public string TransporteNombre { get; set; }

        public string CodigoSucursal { get; set; }
        public string Estacion { get; set; }
        public decimal FactorCambio { get; set; }
        public decimal MontoRecibido { get; set; }
        public decimal CambioDar { get; set; }
        public int Renglones { get; set; }
        public Enumerados.enumCondicionPago CondicionPago { get; set; }
        public int DiasCredito { get; set; }
        public string Notas { get; set; }
        public string SerialFiscal { get; set; }


        public string MesRelacion { get; set; }
        public string AnoRelacion { get; set; }
        public string CondicionPagoDescripcion { get; set; }
        public string DocumentoCodigo { get; set; }
        public string DocumentoNombre { get; set; }
        public string DocumentoTipo { get; set; }
        public string DocumentoSituacion { get; set; }
        public int DocumentoSigno { get; set; }


        public AgregarEncabezado AgregarEncabezado { get; set; }
        public IEnumerable<AgregarCuerpo> AgregarCuerpo { get; set; }
        public AgregarTotal AgregarTotales { get; set; }
        public IEnumerable<AgregarFormaPago> AgregarFormaPago { get; set; }
        public IEnumerable<AgregarKardex> AgregarKardex { get; set; }
        public IEnumerable<AgregarActProductoDeposito> AgregarActProductoDeposito { get; set; }
        public AgregarCxc AgregarCxc { get; set; }

    }

}