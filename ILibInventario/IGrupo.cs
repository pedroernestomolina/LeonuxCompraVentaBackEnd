using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    
    public interface IGrupo
    {

        DtoLib.ResultadoLista<DtoLibInventario.Grupo.Resumen> Grupo_GetLista();

    }

}