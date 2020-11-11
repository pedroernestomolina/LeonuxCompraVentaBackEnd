using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibCompras
{
    
    public interface IProveedor
    {

        DtoLib.ResultadoLista<DtoLibCompra.Proveedor.Lista.Resumen> Proveedor_GetLista(DtoLibCompra.Proveedor.Lista.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibCompra.Proveedor.Data.Ficha> Proveedor_GetFicha(string autoPrv);

    }

}