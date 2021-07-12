using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCajaBanco.Reporte.Analisis.VentaDiaria
{
    
    public class Ficha
    {

        public string auto { get; set; }
        public DateTime fecha { get; set; }
        public string codSucursal { get; set; }
        public string nomSucursal { get; set; }


        public Ficha()
        {
            auto = "";
            fecha = DateTime.Now.Date;
            codSucursal = "";
            nomSucursal = "";
        }

    }

}