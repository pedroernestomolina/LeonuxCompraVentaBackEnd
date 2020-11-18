using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibCajaBanco
{

    public interface ISucursal
    {

        DtoLib.ResultadoLista<DtoLibCajaBanco.Sucursal.Resumen> Sucursal_GetLista();
        DtoLib.ResultadoEntidad<DtoLibCajaBanco.Sucursal.Ficha> Sucursal_GetPrincipal();
        DtoLib.ResultadoEntidad<DtoLibCajaBanco.Sucursal.Ficha> Sucursal_GetFicha(string auto);

    }

}