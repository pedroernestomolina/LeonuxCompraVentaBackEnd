using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibCompras
{
    
    public interface IReportes
    {

        DtoLib.ResultadoLista<DtoLibCompra.Reportes.CompraPorDepartamento.Ficha> Reportes_ComprasPorDepartamento(DtoLibCompra.Reportes.CompraPorDepartamento.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibCompra.Reportes.CompraDocumento.Ficha> Reportes_ComprasDocumento(DtoLibCompra.Reportes.CompraDocumento.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibCompra.Reportes.CompraPorProductoDetalle.Ficha> Reportes_CompraPorProductoDetalle(DtoLibCompra.Reportes.CompraPorProductoDetalle.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibCompra.Reportes.CompraPorProducto.Ficha> Reportes_CompraPorProducto(DtoLibCompra.Reportes.CompraPorProducto.Filtro filtro);

    }

}