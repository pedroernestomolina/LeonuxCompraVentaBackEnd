using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibVentas
{
    
    public interface IProducto
    {

        DtoLib.ResultadoLista<DtoLibVenta.Inventario.Producto.Resumen> ProductoLista(DtoLibVenta.Inventario.Producto.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibVenta.Inventario.Existencia.Resumen> ProductoExistencia(string auto);
        DtoLib.ResultadoLista<DtoLibVenta.Inventario.Precio.Resumen> ProductoPrecios(string auto);
        DtoLib.ResultadoEntidad<DtoLibVenta.Inventario.Producto.DetalleResumen> ProductoDetalleResumen(string auto);
        DtoLib.ResultadoEntidad<DtoLibVenta.Inventario.Producto.Ficha> Producto(string auto);

    }

}