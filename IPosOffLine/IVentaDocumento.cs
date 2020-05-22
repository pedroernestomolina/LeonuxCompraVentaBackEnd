using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPosOffLine
{
    
    public interface IVentaDocumento
    {

        DtoLib.ResultadoId VentaDocumento_Agregar(DtoLibPosOffLine.VentaDocumento.Agregar agregar);
        DtoLib.ResultadoLista<DtoLibPosOffLine.VentaDocumento.Lista.Resumen> VentaDocumento_Lista(DtoLibPosOffLine.VentaDocumento.Lista.Filtro filtro);

    }

}