using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibSistema
{
    

    public interface ITablaPrecio
    {

        DtoLib.Resultado TablaPrecio_Crear(DtoLibSistema.TablaPrecio.Crear.Ficha ficha);

    }

}