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
        public int desdeCierre { get; set; }
        public int hastaCierre { get; set; }
        public bool porFecha { get; set; }
        public bool porCierre { get; set; }


        public Filtro()
        {
            porFecha = false;
            porCierre = true;
            codSucursal= "";
            desdeFecha = DateTime.Now.Date;
            hastaFecha = DateTime.Now.Date;
            desdeCierre = 60;
            hastaCierre = 61;
        }

    }

}