using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibVentas
{
    
    public interface IUsuario
    {

        DtoLib.ResultadoEntidad<DtoLibVenta.Usuario.Ficha> Usuario(string auto);

    }

}