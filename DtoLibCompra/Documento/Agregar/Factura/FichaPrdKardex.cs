using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Documento.Agregar.Factura
{
    
    public class FichaPrdKardex
    {

        public string autoPrd { get; set; }
        public decimal montoTotal { get; set; }
        public string autoDeposito { get; set; }
        public string autoConcepto { get; set; }
        public string documentoNro { get; set; }
        public string modulo { get; set; }
        public string entidad { get; set; }
        public int signoDocumento { get; set; }
        public decimal cantidadFac { get; set; }
        public decimal cantidadBonoFac { get; set; }
        public decimal cantidadUnd { get; set; }
        public decimal costoUnd { get; set; }
        public string esAnulado { get; set; }
        public string nota { get; set; }
        public decimal precioUnd { get; set; }
        public string codigoMovDoc { get; set; }
        public string siglasMovDoc { get; set; }
        public string codigoSucursal { get; set; }
        public string cierreFtp { get; set; }
        public string codigoDeposito { get; set; }
        public string nombreDeposito { get; set; }
        public string codigoConcepto { get; set; }
        public string nombreConcepto { get; set; }
        public string nombrePrd { get; set; }


        public FichaPrdKardex()
        {
            autoPrd = "";
            autoDeposito = "";
            autoConcepto = "";
            montoTotal = 0.0m;
            documentoNro = "";
            modulo = "";
            entidad = "";
            signoDocumento = 1;
            cantidadBonoFac = 0.0m;
            cantidadFac = 0.0m;
            cantidadUnd = 0.0m;
            costoUnd = 0.0m;
            esAnulado = "";
            nota = "";
            precioUnd = 0.0m;
            codigoMovDoc = "";
            siglasMovDoc = "";
            codigoSucursal = "";
            cierreFtp = "";
            codigoDeposito = "";
            nombreDeposito = "";
            codigoConcepto = "";
            nombreConcepto = "";
            nombrePrd = "";
        }

    }

}