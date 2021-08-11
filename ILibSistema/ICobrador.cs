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

    }

}