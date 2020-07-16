using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{

    public interface IPrecio
    {

        DtoLib.ResultadoLista<DtoLibInventario.Precio.Historico.Resumen> Producto_Precio_Historico_Lista(DtoLibInventario.Precio.Historico.Filtro filtro);

    }

}