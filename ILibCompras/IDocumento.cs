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
        DtoLib.ResultadoAuto Compra_DocumentoAgregarFactura(DtoLibCompra.Documento.Cargar.Factura.Ficha docFac);
        DtoLib.ResultadoEntidad<DtoLibCompra.Documento.Visualizar.Ficha> Compra_DocumentoVisualizar(string auto);

    }

}