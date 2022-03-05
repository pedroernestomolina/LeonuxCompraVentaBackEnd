using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibSistema
{
    
    public interface IPermisos
    {

        DtoLib.ResultadoEntidad<string> Permiso_PedirClaveAcceso_NivelMaximo();
        DtoLib.ResultadoEntidad<string> Permiso_PedirClaveAcceso_NivelMedio();
        DtoLib.ResultadoEntidad<string> Permiso_PedirClaveAcceso_NivelMinimo();

        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ToolSistema (string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_InicializarBD(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_InicializarBD_Sucursal(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_AjustarTasaDivisa(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_AjustarTasaDivisaRecepcionPos(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_EtiquetaParaPrecios(string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_AsignarDepositoSucursal(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_AsignarDepositoSucursal_EliminarAsignacion(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_AsignarDepositoSucursal_EditarAsignacion(string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlSucursalGrupo(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlSucursalGrupo_Agregar(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlSucursalGrupo_Editar(string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlSucursal(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlSucursal_Agregar(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlSucursal_Editar(string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlDeposito(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlDeposito_Agregar(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlDeposito_Editar(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlDeposito_ActivarInactivar (string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlUsuarioGrupo(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlUsuarioGrupo_Agregar(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlUsuarioGrupo_Editar(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlUsuarioGrupo_Eliminar(string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlUsuario(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlUsuario_Agregar(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlUsuario_Editar(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlUsuario_ActivarInactivar(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlUsuario_Eliminar(string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlVendedor(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlVendedor_Agregar(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlVendedor_Editar(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlVendedor_ActivarInactivar(string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlCobrador(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlCobrador_Agregar(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlCobrador_Editar(string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlSerieFiscal(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlSerieFiscal_Agregar(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlSerieFiscal_Editar(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlSerieFiscal_ActivarInactivar(string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_Configuracion_Sistema(string autoGrupoUsuario);

    }

}