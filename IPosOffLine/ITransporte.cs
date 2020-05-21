using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPosOffLine
{
    
    public interface ITransporte
    {

        DtoLib.ResultadoLista<DtoLibPosOffLine.Transporte.Ficha> Transporte_Lista();

    }

}