using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPosOffLine
{

    public interface IProducto
    {

        DtoLib.ResultadoLista<DtoLibPosOffLine.Producto.Resumen> ProductoLista(string filtro);
        DtoLib.ResultadoEntidad<string> ProductoBuscarPorCodigoBarraPlu(string aBuscar);
        DtoLib.ResultadoEntidad<DtoLibPosOffLine.Producto.Ficha> Producto(string aBuscar);
        DtoLib.ResultadoLista<DtoLibPosOffLine.Producto.Resumen> ProductoListaPlu();
        DtoLib.ResultadoLista<DtoLibPosOffLine.Producto.Resumen> ProductoListaOferta();

    }

}