using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    
    public interface IProducto
    {

        DtoLib.ResultadoLista<DtoLibPos.Producto.Lista.Ficha> Producto_GetLista(DtoLibPos.Producto.Lista.Filtro filtro);

    }

}
