using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    
    public interface IProveedor
    {

        DtoLib.ResultadoLista<DtoLibInventario.Proveedor.Resumen> Proveedor_GetLista(DtoLibInventario.Proveedor.Filtro filtro);

    }

}