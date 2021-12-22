using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibCompras
{
    
    public interface IDeposito
    {

        DtoLib.ResultadoLista<DtoLibCompra.Deposito.Lista.Resumen> Deposito_GetLista(DtoLibCompra.Deposito.Lista.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibCompra.Deposito.Data.Ficha> Deposito_GetFicha(string autoDeposito);

    }

}