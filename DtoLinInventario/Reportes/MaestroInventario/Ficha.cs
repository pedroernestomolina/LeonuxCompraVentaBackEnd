using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.MaestroInventario
{
    
    public class Ficha
    {

        private int contenidoCompras { get; set; }
        private decimal costoDivisa { get; set; }


        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public string referenciaPrd { get; set; }
        public string modeloPrd { get; set; }
        private string estatusPrd { get; set; }
        private string estatusDivisaPrd { get; set; }
        private string estatusCambioPrd { get; set; }
        public string departamento { get; set; }
        public decimal? existencia { get; set; }
        public decimal costoUnd { get; set; }
        public string decimales { get; set; }


        public decimal costoDivisaUnd 
        {
            get 
            {
                var rt = 0.0m;
                rt = costoDivisa / contenidoCompras;
                return rt;
            } 
        }

        public enumerados.EnumAdministradorPorDivisa admDivisa
        {
            get
            {
                var rt = DtoLibInventario.Reportes.enumerados.EnumAdministradorPorDivisa.No;
                if (estatusDivisaPrd.Trim().ToUpper() == "1")
                    rt = DtoLibInventario.Reportes.enumerados.EnumAdministradorPorDivisa.Si;
                return rt;
            }
        }

        public enumerados.EnumEstatus estatus
        {
            get
            {
                var rt = DtoLibInventario.Reportes.enumerados.EnumEstatus.Activo;
                if (estatusPrd.Trim().ToUpper() != "ACTIVO")
                {
                    rt = DtoLibInventario.Reportes.enumerados.EnumEstatus.Inactivo;
                }
                return rt;
            }
        }

    }

}