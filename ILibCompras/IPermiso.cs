using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibCompras
{
    
    public interface IPermiso
    {

        DtoLib.ResultadoEntidad<string> Permiso_PedirClaveAcceso_NivelMaximo();
        DtoLib.ResultadoEntidad<string> Permiso_PedirClaveAcceso_NivelMedio();
        DtoLib.ResultadoEntidad<string> Permiso_PedirClaveAcceso_NivelMinimo();

        DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_ToolCompra(string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_Registrar_Factura(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_Registrar_Nc(string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_Reportes(string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_AdmDoc (string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_AdmDoc_Anular(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_AdmDoc_Visualizar(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_AdmDoc_Reporte(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_AdmDoc_Corrector(string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_Grupo(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_CrearGrupo(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_ModificarGrupo(string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_Proveedor(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_Proveedor_Agregar(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_Proveedor_Editar(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_Proveedor_CambiarEstatus(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_Proveedor_Reportes(string autoGrupoUsuario);

    }

}