using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{

    public interface IDocumentoAdm
    {

        DtoLib.ResultadoEntidad<DtoLibPos.DocumentoAdm.Anular.CapturarData.Ficha> DocumentoAdm_Anular_CapturarData(string idDoc);

    }

}