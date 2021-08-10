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
        DtoLib.ResultadoAuto Proveedor_AgregarFicha(DtoLibCompra.Proveedor.Agregar.Ficha ficha);
        DtoLib.Resultado Proveedor_EditarFicha(DtoLibCompra.Proveedor.Editar.Ficha ficha);
        DtoLib.Resultado Proveedor_AgregarFicha_Validar(DtoLibCompra.Proveedor.Agregar.FichaValidar ficha);
        DtoLib.Resultado Proveedor_EditarFicha_Validar(DtoLibCompra.Proveedor.Editar.FichaValidar ficha);
        DtoLib.ResultadoLista<DtoLibCompra.Proveedor.Documento.Ficha> Proveedor_Documento_GetLista(DtoLibCompra.Proveedor.Documento.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibCompra.Proveedor.Articulos.Ficha > Proveedor_CompraArticulos_GetLista(DtoLibCompra.Proveedor.Articulos.Filtro filtro);
        DtoLib.Resultado Proveedor_Activar(DtoLibCompra.Proveedor.ActivarInactivar.Ficha ficha);
        DtoLib.Resultado Proveedor_Inactivar(DtoLibCompra.Proveedor.ActivarInactivar.Ficha ficha);

    }

}