using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibCompras
{
    
    public interface IConcepto
    {

        DtoLib.ResultadoEntidad<DtoLibCompra.Concepto.Ficha> Concepto_GetFicha(string auto);

    }

}