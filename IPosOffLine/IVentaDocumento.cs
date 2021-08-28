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
        DtoLib.Resultado VentaDocumento_Anular(int idDocumento);
        DtoLib.ResultadoEntidad<DtoLibPosOffLine.VentaDocumento.Cargar.Ficha> VentaDocumento_Cargar(int idDocumento);
        //
        DtoLib.Resultado VentaDocumento_Resumen_Generar(DtoLibPosOffLine.ResumenVentaPos.Generar.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibPosOffLine.ResumenVentaPos.Entidad.Ficha> VentaDocumento_Resumen_GetLista(DtoLibPosOffLine.ResumenVentaPos.Lista.Filtro filtro);
        DtoLib.Resultado VentaDocumento_Resumen_Subir(DtoLibPosOffLine.ResumenVentaPos.Subir.Ficha ficha);

    }

}