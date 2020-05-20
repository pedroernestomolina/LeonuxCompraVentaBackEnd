using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibVenta.Permiso
{
    
    public class Ficha
    {

        public bool IsHabilitado { get; set; }
        public Enumerados.EnumNivelSeguridad NivelSeguridad { get; set; }


        public Ficha() 
        {
            IsHabilitado = false;
            NivelSeguridad = Enumerados.EnumNivelSeguridad.SinDefinir;
        }

    }

}