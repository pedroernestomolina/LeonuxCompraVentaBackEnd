using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{

    public interface IReportes
    {

        DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroProducto.Ficha> Reportes_MaestroProducto ( DtoLibInventario.Reportes.MaestroProducto.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroInventario.Ficha> Reportes_MaestroInventario (DtoLibInventario.Reportes.MaestroInventario.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibInventario.Reportes.Top20.Ficha> Reportes_Top20(DtoLibInventario.Reportes.Top20.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibInventario.Reportes.TopDepartUtilidad.Ficha> Reportes_TopDepartUtilidad(DtoLibInventario.Reportes.TopDepartUtilidad.Filtro filtro);

    }

}