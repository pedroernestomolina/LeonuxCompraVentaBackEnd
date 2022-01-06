using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvPos
{

    public partial class Provider: IPos.IProvider
    {

        public DtoLib.ResultadoEntidad<DtoLibPos.DocumentoAdm.Anular.CapturarData.Ficha> DocumentoAdm_Anular_CapturarData(string idDoc)
        {
            throw new NotImplementedException();
        }

    }

}