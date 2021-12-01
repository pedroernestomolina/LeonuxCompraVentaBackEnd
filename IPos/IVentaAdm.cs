using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    
    public interface IVentaAdm
    {

        DtoLib.ResultadoId VentaAdm_Temporal_Encabezado_Registrar(DtoLibPos.VentaAdm.Temporal.Encabezado.Registrar.Ficha ficha);
        DtoLib.Resultado VentaAdm_Temporal_Encabezado_Editar(DtoLibPos.VentaAdm.Temporal.Encabezado.Editar.Ficha ficha);
        DtoLib.Resultado VentaAdm_Temporal_Encabezado_Eliminar(int idEncabezado);
        DtoLib.ResultadoId VentaAdm_Temporal_Item_Registrar(DtoLibPos.VentaAdm.Temporal.Item.Registrar.Ficha ficha);
        DtoLib.Resultado VentaAdm_Temporal_Item_Eliminar(DtoLibPos.VentaAdm.Temporal.Item.Eliminar.Ficha ficha);
        DtoLib.Resultado VentaAdm_Temporal_Item_Limpiar(DtoLibPos.VentaAdm.Temporal.Item.Limpiar.Ficha ficha);
        DtoLib.ResultadoEntidad<DtoLibPos.VentaAdm.Temporal.Item.Entidad.Ficha> VentaAdm_Temporal_Item_GetFichaById(int idItem);
        DtoLib.Resultado VentaAdm_Temporal_Anular(DtoLibPos.VentaAdm.Temporal.Anular.Ficha ficha);

    }

}