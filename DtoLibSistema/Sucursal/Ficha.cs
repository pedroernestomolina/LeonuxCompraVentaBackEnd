using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.Sucursal
{
    
    public class Ficha
    {

        public string auto { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string autoDepositoPrincipal { get; set; }
        public string codigoDepositoPrincipal { get; set; }
        public string nombreDepositoPrincipal { get; set; }
        public string autoGrupoSucursal { get; set; }
        public string nombreGrupoSucursal { get; set; }
        public string precioId { get; set; }
        public string estatusFacturarMayor { get; set; }


        public Ficha() 
        {
            auto = "";
            codigo = "";
            nombre = "";
            autoDepositoPrincipal = "";
            codigoDepositoPrincipal = "";
            nombreDepositoPrincipal = "";
            autoGrupoSucursal = "";
            nombreGrupoSucursal = "";
            precioId = "";
            estatusFacturarMayor = "";
        }

    }

}