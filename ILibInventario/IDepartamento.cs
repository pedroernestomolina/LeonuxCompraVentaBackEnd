using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    
    public interface IDepartamento
    {

        DtoLib.ResultadoLista<DtoLibInventario.Departamento.Resumen> Departamento_GetLista();

    }

}