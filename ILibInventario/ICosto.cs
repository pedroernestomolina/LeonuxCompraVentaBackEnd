using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    
    public interface ICosto
    {

        DtoLib.ResultadoLista<DtoLibInventario.Costo.Historico.Resumen> Producto_Costo_Historico_Lista(DtoLibInventario.Costo.Historico.Filtro filtro);

    }

}