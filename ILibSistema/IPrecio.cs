using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibSistema
{
    
    public interface IPrecio
    {

        DtoLib.ResultadoLista<DtoLibSistema.Precio.Resumen> Precio_GetLista();
        DtoLib.ResultadoEntidad<DtoLibSistema.Precio.Etiquetar.Ficha > Precio_Etiquetar_GetFicha();
        DtoLib.Resultado Precio_Etiquetar_Actualizar(DtoLibSistema.Precio.Etiquetar.Actualizar ficha);

    }

}