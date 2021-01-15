using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Documento.Cargar
{
    
    public class Ficha
    {

        public string autoId { get; set; }
        public string provAuto { get; set; }
        public string provNombre { get; set; }
        public string provCiRif { get; set; }
        public string provCodigo { get; set; }
        public string provDirFiscal { get; set; }
        public string provTelefono { get; set; }
        public string documentoNro { get; set; }
        public string controlNro { get; set; }
        public string ordenCompraNro { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string horaRegistro { get; set; }
        public DateTime fechaEmision { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public int diasCredito { get; set; }
        public string mesRelacion { get; set; }
        public string anoRelacion { get; set; }
        public string documentoSerie { get; set; }
        public string documentoNombre { get; set; }
        public string documentoTipo { get; set; }
        public decimal montoTotal { get; set; }
        public decimal montoExento { get; set; }
        public decimal montoBase { get; set; }
        public decimal montoImpuesto { get; set; }
        public decimal subTotal { get; set; }
        public decimal descuentoPorct { get; set; }
        public decimal cargoPorct { get; set; }
        public decimal montoDescuento { get; set; }
        public decimal montoCargo { get; set; }
        public decimal montoBase1 { get; set; }
        public decimal montoBase2 { get; set; }
        public decimal montoBase3 { get; set; }
        public decimal montoIva1 { get; set; }
        public decimal montoIva2 { get; set; }
        public decimal montoIva3 { get; set; }
        public decimal tasaIva1 { get; set; }
        public decimal tasaIva2 { get; set; }
        public decimal tasaIva3 { get; set; }
        public string notas { get; set; }
        public decimal factorCambio { get; set; }
        public decimal montoDivisa { get; set; }
        public string usuarioNombre { get; set; }
        public string usuarioCodigo { get; set; }
        public string equipoEstacion { get; set; }
        public int renglones { get; set; }
        public int signo { get; set; }
        public string codigoSucursal { get; set; }
        public List<FichaDetalle> detalles { get; set; }

    }

}