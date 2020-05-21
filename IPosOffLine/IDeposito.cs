using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPosOffLine
{
    
    public interface IDeposito
    {

        DtoLib.ResultadoLista<DtoLibPosOffLine.Deposito.Ficha> Deposito_Lista();

    }

}
