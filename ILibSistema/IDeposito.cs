using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibSistema
{

    public interface IDeposito
    {

        DtoLib.ResultadoLista<DtoLibSistema.Deposito.Resumen> Deposito_GetLista();
        DtoLib.ResultadoEntidad<DtoLibSistema.Deposito.Ficha> Deposito_GetFicha(string auto);
        DtoLib.ResultadoAuto Deposito_Agregar(DtoLibSistema.Deposito.Agregar ficha);
        DtoLib.Resultado Deposito_Editar (DtoLibSistema.Deposito.Editar ficha);
        DtoLib.ResultadoEntidad<int> Deposito_GeneraCodigoAutomatico();
        DtoLib.Resultado Deposito_Activar(string idDep);
        DtoLib.Resultado Deposito_Inactivar(string idDep);

    }

}