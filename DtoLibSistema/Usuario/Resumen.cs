using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.Usuario
{

    public class Resumen
    {

        public string uId{ get; set; }
        public string uNombre { get; set; }
        public string uApellido { get; set; }
        public string uCodigo { get; set; }
        public string uEstatus { get; set; }
        public DateTime uFechaAlta { get; set; }
        public DateTime uFechaBaja { get; set; }
        public DateTime uFechaUltSesion { get; set; }
        public string gNombre { get; set; }


        public Resumen() 
        {
            uId = "";
            uNombre = "";
            uApellido = "";
            uCodigo = "";
            uEstatus = "";
            uFechaAlta = DateTime.Now.Date;
            uFechaBaja = DateTime.Now.Date;
            uFechaUltSesion = DateTime.Now.Date;
            gNombre = "";
        }

    }

}