using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibSistema
{
    
    public interface IVendedor
    {

        DtoLib.ResultadoLista<DtoLibSistema.Vendedor.Lista.Ficha> Vendedor_GetLista(DtoLibSistema.Vendedor.Lista.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibSistema.Vendedor.Entidad.Ficha> Vendedor_GetFicha_ById(string id);
        DtoLib.ResultadoAuto Vendedor_AgregarFicha(DtoLibSistema.Vendedor.Agregar.Ficha ficha);
        DtoLib.Resultado Vendedor_EditarFicha(DtoLibSistema.Vendedor.Editar.Ficha ficha);
        DtoLib.Resultado Vendedor_Activar(DtoLibSistema.Vendedor.ActivarInactivar.Ficha ficha);
        DtoLib.Resultado Vendedor_Inactivar(DtoLibSistema.Vendedor.ActivarInactivar.Ficha ficha);

        //
        DtoLib.Resultado Vendedor_Validar_Agregar(DtoLibSistema.Vendedor.Agregar.Ficha ficha);
        DtoLib.Resultado Vendedor_Validar_Editar(DtoLibSistema.Vendedor.Editar.Ficha ficha);
    }

}