using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.Configuracion.ActualizarTasaDivisa.ActualizarData
{

    public class FichaProductoCostoPrecioDivisa
    {

        public string autoPrd { get; set; }
        public decimal costo { get; set; }
        public decimal costoUnd { get; set; }
        public decimal costoImportacion { get; set; }
        public decimal costoImportacionUnd { get; set; }
        public decimal costoVario { get; set; }
        public decimal costoVarioUnd { get; set; }
        public decimal costoProveedor { get; set; }
        public decimal costoProveedorUnd { get; set; }
        public decimal costoDivisa { get; set; }
        public decimal precio_1 { get; set; }
        public decimal precio_2 { get; set; }
        public decimal precio_3 { get; set; }
        public decimal precio_4 { get; set; }
        public decimal precio_5 { get; set; }
        public decimal precioMay_1 { get; set; }
        public decimal precioMay_2 { get; set; }
        public string serie { get; set; }
        public string documento { get; set; }
        public string nota { get; set; }


        public FichaProductoCostoPrecioDivisa()
        {
            autoPrd = "";
            costo = 0.0m;
            costoUnd = 0.0m;
            costoImportacion = 0.0m;
            costoImportacionUnd = 0.0m;
            costoVario=0.0m;
            costoVarioUnd=0.0m;
            costoProveedor = 0.0m;
            costoProveedorUnd = 0.0m;
            precio_1 = 0.0m;
            precio_2 = 0.0m;
            precio_3 = 0.0m;
            precio_4 = 0.0m;
            precio_5 = 0.0m;
            precioMay_1 = 0.0m;
            precioMay_2 = 0.0m;
            serie = "";
            documento = "";
            nota = "";
        }

    }

}