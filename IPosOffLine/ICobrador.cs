using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPosOffLine
{

    public interface ICobrador
    {

        DtoLib.ResultadoLista<DtoLibPosOffLine.Cobrador.Ficha> Cobrador_Lista();

    }

}