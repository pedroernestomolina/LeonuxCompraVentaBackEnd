using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.MaestroExistencia
{
    
    public class Ficha
    {

        public string autoPrd { get; set; }
        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public decimal? exFisica { get; set; }
        public string autoDep { get; set; }
        public string codigoDep { get; set; }
        public string nombreDep { get; set; }
        public string decimales { get; set; }

    }

}