using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.MaestroInventario
{
    
    public class Filtro
    {

        public string autoDepartamento { get; set; }
        public enumerados.EnumAdministradorPorDivisa admDivisa { get; set; }
        public enumerados.EnumEstatus estatus { get; set; }


        public Filtro()
        {
            admDivisa = enumerados.EnumAdministradorPorDivisa.SnDefinir;
            estatus = enumerados.EnumEstatus.SnDefinir;
            autoDepartamento = "";
        }

    }

}