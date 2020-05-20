using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibVentas
{
    
    public interface ICobrador
    {

        DtoLib.ResultadoLista<DtoLibVenta.Cobrador.Resumen> CobradorLista();

    }

}
