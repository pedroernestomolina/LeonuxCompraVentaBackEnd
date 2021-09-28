using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibSistema
{
    
    public interface IReconversionMonetaria
    {

        DtoLib.ResultadoEntidad<int> ReconversionMonetaria_GetCount();
        DtoLib.ResultadoEntidad<DtoLibSistema.ReconversionMonetaria.Data.Ficha> ReconversionMonetaria_GetData();
        DtoLib.Resultado ReconversionMonetaria_Actualizar(DtoLibSistema.ReconversionMonetaria.Actualizar.Ficha ficha);

    }

}