using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Documento.Cargar.Factura
{
    
    public class FichaDetalle
    {

        public string autoProducto { get; set; }
        public string codigoProducto { get; set; }
        public string nombreProducto { get; set; }
        public string autoDepartamento { get; set; }
        public string autoGrupo { get; set; }
        public string autoSubGrupo { get; set; }
        public string autoDeposito { get; set; }
        public decimal cantidadFac { get; set; }
        public string empaquePrd { get; set; }
        public decimal valorPorcDescto1 { get; set; }
        public decimal valorPorcDescto2 { get; set; }
        public decimal valorPorcDescto3 { get; set; }
        public decimal montoDescto1 { get; set; }
        public decimal montoDescto2 { get; set; }
        public decimal montoDescto3 { get; set; }
        public decimal totalNeto { get; set; }
        public decimal valorTasaIva { get; set; }
        public decimal montoImpuesto { get; set; }
        public decimal montoTotal { get; set; }
        public string esAnulado { get; set; }
        public string tipoDocumento { get; set; }
        public string depositoNombre { get; set; }
        public string autoProveedor { get; set; }
        public string decimalesPrd { get; set; }
        public int contenidoEmpaque { get; set; }
        public decimal cantidadUnd { get; set; }
        public decimal costoUnd { get; set; }
        public string depositoCodigo { get; set; }
        public string detalle { get; set; }
        public string autoTasaIva { get; set; }
        public string categoriaPrd { get; set; }
        public decimal costoPromedioUnd { get; set; }
        public decimal costoCompra { get; set; }
        public string codigoProveedor { get; set; }
        public decimal cantidadBonoFac { get; set; }
        public decimal costoBruto { get; set; }
        public string estatusUnidad { get; set; }
        public DateTime fechaLote { get; set; }
        public string cierreFtp { get; set; }
        public int signo { get; set; }

    }

}