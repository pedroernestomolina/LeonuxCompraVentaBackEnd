using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibCompras
{
    
    public interface IDocumento
    {

        DtoLib.ResultadoLista<DtoLibCompra.Documento.Lista.Resumen> Compra_DocumentoGetLista(DtoLibCompra.Documento.Lista.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibCompra.Documento.ListaRemision.Ficha> Compra_DocumentoGetListaRemision(DtoLibCompra.Documento.ListaRemision.Filtro filtro);

        DtoLib.ResultadoEntidad<DtoLibCompra.Documento.Cargar.Ficha> Compra_DocumentoGetFicha(string autoDoc);
        DtoLib.ResultadoAuto Compra_DocumentoAgregarFactura(DtoLibCompra.Documento.Agregar.Factura.Ficha docFac);
        DtoLib.ResultadoAuto Compra_DocumentoAgregarNotaCredito (DtoLibCompra.Documento.Agregar.NotaCredito.Ficha docNC);
        DtoLib.ResultadoEntidad<DtoLibCompra.Documento.Visualizar.Ficha> Compra_DocumentoVisualizar(string auto);
        DtoLib.Resultado Compra_DocumentoAnularFactura(DtoLibCompra.Documento.Anular.Factura.Ficha ficha);
        DtoLib.Resultado Compra_DocumentoAnularNotaCredito(DtoLibCompra.Documento.Anular.NotaCredito.Ficha ficha);
        DtoLib.Resultado Compra_DocumentoAnular_Verificar(string autoDoc);

    }

}