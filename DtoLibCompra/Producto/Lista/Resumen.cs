using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Producto.Lista
{
    
    public class Resumen
    {

        private string estatus { get; set; }
        private string estatusDivisa { get; set; }

        public string autoPrd { get; set; }
        public string codigoPrd { get; set; }
        public string descripcionPrd { get; set; }
        public string nombreDepartamento { get; set; }
        public string nombreGrupo { get; set; }
        public string nombreMarca { get; set; }
        public string modeloPrd { get; set; }
        public string referenciaPrd { get; set; }
        public string categoriaPrd { get; set; }
        public string origenPrd { get; set; }
        public int contenidoEmpaquePrd { get; set; }
        public string empaqueCompraPrd { get; set; }
        public decimal tasaIvaPrd { get; set; }
        public string tasaIvaDescripcion { get; set; }


        public Enumerados.EnumEstatus estatusPrd 
        {
            get 
            {
                var rt= Enumerados.EnumEstatus.Activo;
                if (estatus.Trim().ToUpper() != "ACTIVO")
                    rt = Enumerados.EnumEstatus.Inactivo;
                return rt;
            }
        }

        public Enumerados.EnumAdministradorPorDivisa admPorDivisa 
        {
            get 
            {
                var rt = Enumerados.EnumAdministradorPorDivisa.Si;
                if (estatusDivisa.Trim().ToUpper() != "1")
                    rt = Enumerados.EnumAdministradorPorDivisa.No;
                return rt;
            }
        }

    }

}