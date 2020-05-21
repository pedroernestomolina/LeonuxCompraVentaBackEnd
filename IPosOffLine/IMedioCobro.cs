using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPosOffLine
{
    
    public interface IMedioCobro
    {

        DtoLib.ResultadoLista<DtoLibPosOffLine.MedioCobro.Ficha> MedioCobro_Lista();

    }

}