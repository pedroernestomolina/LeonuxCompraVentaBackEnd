using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPosOffLine
{
    
    public interface IServidor
    {

        DtoLib.Resultado Servidor_Test();
        DtoLib.Resultado Servidor_ActualizarData();
        DtoLib.ResultadoEntidad<int> Productos_TotalItems();

    }

}
