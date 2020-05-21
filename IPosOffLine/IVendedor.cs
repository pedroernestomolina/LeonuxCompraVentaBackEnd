using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPosOffLine
{
    
    public interface IVendedor
    {

        DtoLib.ResultadoLista<DtoLibPosOffLine.Vendedor.Ficha> Vendedor_Lista();

    }

}
