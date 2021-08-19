using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibSistema
{
    
    public interface ISerieFiscal
    {

        DtoLib.ResultadoLista<DtoLibSistema.SerieFiscal.Lista.Ficha> SerieFiscal_GetLista(DtoLibSistema.SerieFiscal.Lista.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibSistema.SerieFiscal.Entidad.Ficha> SerieFiscal_GetFicha_ById(string id);
        DtoLib.ResultadoAuto SerieFiscal_AgregarFicha(DtoLibSistema.SerieFiscal.Agregar.Ficha ficha);
        DtoLib.Resultado SerieFiscal_EditarFicha(DtoLibSistema.SerieFiscal.Editar.Ficha ficha);
        DtoLib.Resultado SerieFiscal_Activar(DtoLibSistema.SerieFiscal.ActivarInactivar.Ficha ficha);
        DtoLib.Resultado SerieFiscal_Inactivar(DtoLibSistema.SerieFiscal.ActivarInactivar.Ficha ficha);

        //

        DtoLib.Resultado SerieFiscal_Validar_Agregar(DtoLibSistema.SerieFiscal.Agregar.Ficha ficha);
        DtoLib.Resultado SerieFiscal_Validar_Editar(DtoLibSistema.SerieFiscal.Editar.Ficha ficha);

    }

}