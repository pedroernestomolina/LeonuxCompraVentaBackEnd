using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibCompras
{
    
    public interface IEstado
    {

        DtoLib.ResultadoLista<DtoLibCompra.Maestros.Estado.Ficha> Estado_GetLista();

    }

}