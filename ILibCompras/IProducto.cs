using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibCompras
{
    
    public interface IProducto
    {

        DtoLib.ResultadoLista<DtoLibCompra.Producto.Lista.Resumen> Producto_GetLista(DtoLibCompra.Producto.Lista.Filtro filtro);

    }

}