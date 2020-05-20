using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPosOffLine
{

    public interface IItem
    {

        DtoLib.ResultadoLista<DtoLibPosOffLine.Item.Ficha> Item_Cargar();
        DtoLib.ResultadoEntidad<DtoLibPosOffLine.Item.Ficha> Item(int id);
        DtoLib.ResultadoId Item_Agregar(DtoLibPosOffLine.Item.Agregar ficha);
        DtoLib.Resultado Item_Limpiar();
        DtoLib.Resultado Item_Actualizar(DtoLibPosOffLine.Item.Actualizar ficha);
        DtoLib.Resultado Item_Eliminar(int id);

    }

}