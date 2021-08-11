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

    }

}
