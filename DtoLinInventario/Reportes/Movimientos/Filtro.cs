using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.Movimientos
{
    
    public class Filtro
    {

        public string autoSucursal { get; set; }
        public DateTime? desdeFecha { get; set; }
        public DateTime? hastaFecha { get; set; }


        public Filtro()
        {
            autoSucursal = "";
        }

    }

}