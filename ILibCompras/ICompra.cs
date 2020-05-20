using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibCompras
{
    
    public interface ICompra
    {
        DtoLib.ResultadoLista<DtoLibCompra.Compra.Resumen> CompraLista(DtoLibCompra.Compra.Filtro filtro);
    }

}