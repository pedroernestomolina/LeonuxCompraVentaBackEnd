using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCajaBanco.Reporte.Movimiento.CobranzaDiaria
{
    
    public class Filtro
    {

        public string codSucursal { get; set; }
        public DateTime desdeFecha { get; set; }
        public DateTime hastaFecha { get; set; }


        public Filtro()
        {
            codSucursal= "";
            desdeFecha = DateTime.Now.Date;
            hastaFecha = DateTime.Now.Date;
        }

    }

}