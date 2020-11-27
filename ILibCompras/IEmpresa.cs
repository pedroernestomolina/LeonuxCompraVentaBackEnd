using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibCompras
{
    
    public interface IEmpresa
    {

        DtoLib.ResultadoEntidad<DtoLibCompra.Empresa.Data.Ficha> Empresa_Datos();
        DtoLib.ResultadoEntidad<DtoLibCompra.Empresa.Fiscal.Ficha> Empresa_GetTasas();

    }

}