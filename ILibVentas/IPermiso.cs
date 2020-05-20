using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibVentas
{

    public interface IPermiso
    {

        DtoLib.ResultadoEntidad<DtoLibVenta.Permiso.Ficha> Venta_DarDescuento_Item(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibVenta.Permiso.Ficha> Venta_Eliminar_Item(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibVenta.Permiso.Ficha> Venta_PrecioLibre_Item(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibVenta.Permiso.Ficha> Venta_DescuentoGlobal(string autoGrupoUsuario);

    }

}