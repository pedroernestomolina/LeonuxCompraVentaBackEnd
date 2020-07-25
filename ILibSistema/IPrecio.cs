using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibSistema
{
    
    public interface IPrecio
    {

        DtoLib.ResultadoLista<DtoLibSistema.Precio.Resumen> Precio_GetLista();

    }

}