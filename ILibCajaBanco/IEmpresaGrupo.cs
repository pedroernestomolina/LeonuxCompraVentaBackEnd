using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibCajaBanco
{
    
    public interface IEmpresaGrupo
    {

        DtoLib.ResultadoEntidad<DtoLibCajaBanco.EmpresaGrupo.Ficha> EmpresaGrupo_GetFicha(string autoGrupo);

    }

}