using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Documento.Pendiente.Abrir
{
    
    public class Ficha
    {

        public string entidadAuto{ get; set; }
        public string entidadCodigo { get; set; }
        public string entidadCiRif { get; set; }
        public string entidadNombre { get; set; }
        public string entidadDirFiscal { get; set; }
        public decimal docFactorCambio { get; set; }
        public string docNumero { get; set; }
        public string docControl { get; set; }
        public int docDiasCredito { get; set; }
        public string docNotas { get; set; }
        public string docOrdenCompra { get; set; }
        public DateTime docFechaEmision { get; set; }
        public string autoDeposito { get; set; }
        public string autoSucursal { get; set; }
        public List<FichaDetalle> items { get; set; }


        public Ficha()
        {
            entidadAuto = "";
            entidadCodigo = "";
            entidadCiRif = "";
            entidadNombre = "";
            entidadDirFiscal="";
            docFactorCambio = 0.0m;
            docNumero = "";
            docControl = "";
            docDiasCredito=0;
            docNotas = "";
            docFechaEmision = DateTime.Now.Date;
            docDiasCredito = 0;
            autoDeposito = "";
            autoSucursal = "";
            items = new List<FichaDetalle>();
        }

    }

}