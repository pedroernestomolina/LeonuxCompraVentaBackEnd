using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    
    public interface IDeposito
    {

        DtoLib.ResultadoLista<DtoLibInventario.Deposito.Resumen> Deposito_GetLista();
        DtoLib.ResultadoEntidad<DtoLibInventario.Deposito.Ficha> Deposito_GetFicha(string autoDep);

    }

}