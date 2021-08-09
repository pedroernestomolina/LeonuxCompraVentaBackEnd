using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Proveedor.Lista
{
    
    public class Resumen
    {

        private string estatus {get;set;}

        public string auto { get; set; }
        public string ciRif { get; set; }
        public string codigo { get; set; }
        public string nombreRazonSocial { get; set; }
        public string dirFiscal { get; set; }
        public string telefono { get; set; }
        public string nombreGrupo { get; set; }
        public string nombreEstado { get; set; }
        public string nombreContacto { get; set; }
        public DateTime fechaAlta { get; set; }
        public DateTime fechaUltCompra { get; set; }
        public DateTime fechaBaja { get; set; }
        public Enumerados.EnumEstatus estatusPrv 
        {
            get 
            {
                var rt= Enumerados.EnumEstatus.Activo;
                if (estatus.Trim().ToUpper() != "ACTIVO")
                    rt = Enumerados.EnumEstatus.Inactivo;
                return rt;
            }
        }

    }

}