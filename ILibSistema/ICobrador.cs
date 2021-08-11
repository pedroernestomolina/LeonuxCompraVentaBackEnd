using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibSistema
{
    
    public interface ICobrador
    {

        DtoLib.ResultadoLista<DtoLibSistema.Cobrador.Lista.Ficha> Cobrador_GetLista(DtoLibSistema.Cobrador.Lista.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibSistema.Cobrador.Entidad.Ficha> Cobrador_GetFicha_ById(string id);
        DtoLib.ResultadoAuto Cobrador_AgregarFicha(DtoLibSistema.Cobrador.Agregar.Ficha ficha);
        DtoLib.Resultado Cobrador_EditarFicha(DtoLibSistema.Cobrador.Editar.Ficha ficha);

    }

}