using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.Configuracion.ActualizarTasaDivisa.CapturarData
{
    
    public class Ficha
    {

        private string auto { get; set; }
        private string estatus_divisa { get; set; }
        private decimal divisa { get; set; }
        private decimal costo { get; set; }
        private int contenido_compras { get; set; }
        private decimal pdf_1 { get; set; }
        private decimal pdf_2 { get; set; }
        private decimal pdf_3 { get; set; }
        private decimal pdf_4 { get; set; }
        private decimal pdf_pto { get; set; }
        private decimal tasa { get; set; }
        private decimal precio_1 { get; set; }
        private decimal precio_2 { get; set; }
        private decimal precio_3 { get; set; }
        private decimal precio_4 { get; set; }
        private decimal precio_pto { get; set; }
        private decimal precio_may_1 { get; set; }
        private decimal precio_may_2 { get; set; }
        private decimal pdmf_1 { get; set; }
        private decimal pdmf_2 { get; set; }


        public string autoPrd { get { return auto; } }
        public bool isAdmDivisa { get { return estatus_divisa == "1" ? true : false; } }
        public decimal costoDivisa { get { return divisa; } }
        public decimal costoMoneda { get { return costo; } }
        public int contenido { get { return contenido_compras; } }
        public decimal precioFullDivisa_1 { get { return pdf_1; } }
        public decimal precioFullDivisa_2 { get { return pdf_2; } }
        public decimal precioFullDivisa_3 { get { return pdf_3; } }
        public decimal precioFullDivisa_4 { get { return pdf_4; } }
        public decimal precioFullDivisa_5 { get { return pdf_pto; } }
        public decimal precioFullDivisaMay_1 { get { return pdmf_1; } }
        public decimal precioFullDivisaMay_2 { get { return pdmf_2 ; } }
        public decimal precioNetoMoneda_1 { get { return precio_1; } }
        public decimal precioNetoMoneda_2 { get { return precio_2; } }
        public decimal precioNetoMoneda_3 { get { return precio_3; } }
        public decimal precioNetoMoneda_4 { get { return precio_4; } }
        public decimal precioNetoMoneda_5 { get { return precio_pto; } }
        public decimal precioNetoMonedaMay_1 { get { return precio_may_1; } }
        public decimal precioNetoMonedaMay_2 { get { return precio_may_2; } }
        public decimal tasaIva { get { return tasa; } }

    }

}