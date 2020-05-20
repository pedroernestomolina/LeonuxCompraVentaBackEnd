using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibVentas
{

    public interface IMedioCobro
    {

        DtoLib.ResultadoLista<DtoLibVenta.MedioCobro.Resumen> MedioCobroLista();

    }

}