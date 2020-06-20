using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPosOffLine
{

    public interface IMovConceptoInv
    {

        DtoLib.ResultadoLista<DtoLibPosOffLine.MovConceptoInv.Ficha> MovConceptoInv_Lista();

    }

}
