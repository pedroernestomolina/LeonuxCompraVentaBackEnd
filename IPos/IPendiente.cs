using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{

    public interface IPendiente
    {

        DtoLib.Resultado Pendiente_DejarCta(DtoLibPos.Pendiente.Dejar.Ficha ficha);
        DtoLib.ResultadoEntidad<int> Pendiente_CtasPendientes(int idOperador);
        DtoLib.ResultadoLista<DtoLibPos.Pendiente.Lista.Ficha> Pendiente_Lista(DtoLibPos.Pendiente.Lista.Filtro filtro);
        DtoLib.Resultado Pendiente_AbrirCta(int idCta);

    }

}