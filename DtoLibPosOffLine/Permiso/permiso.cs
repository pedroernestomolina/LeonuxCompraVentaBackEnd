using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Permiso
{
    
    public class permiso
    {

        public enum EnumAcceso { SinDefinir = -1, SinAcceso = 0, PedirClave, Libre }

        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public EnumAcceso RequiereClave { get; set; }


        public permiso()
        {
            Codigo = "";
            Descripcion = "";
            RequiereClave = EnumAcceso.SinDefinir;
        }

    }

}