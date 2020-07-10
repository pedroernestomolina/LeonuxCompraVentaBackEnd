using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqLitePosOffLine
{

    public partial class Provider : IPosOffLine.IProvider
    {

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Usuario.Ficha> Usuario_Cargar(DtoLibPosOffLine.Usuario.Cargar ficha)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Usuario.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var ent = cnn.UsuarioGrupo.FirstOrDefault(f => f.usuarioCodigo.Trim().ToUpper() == ficha.Codigo && 
                        f.usuarioClave.Trim().ToUpper()== ficha.PassWord);
                    if (ent == null)
                    {
                        result.Entidad = null;
                        result.Mensaje = "USUARIO NO ENCONTRADO, VERIFIQUE POR FAVOR";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    if (ent.usuarioEstatus.Trim().ToUpper() != "A")
                    {
                        result.Entidad = null;
                        result.Mensaje = "USUARIO EN ESTADO INACTIVO, VERIFIQUE POR FAVOR";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var nr = new DtoLibPosOffLine.Usuario.Ficha()
                    {
                        GrupoAuto = ent.autoGrupo,
                        GrupoDescripcion = ent.grupoDescripcion,
                        UsuarioAuto = ent.autoUsuario,
                        UsuarioClave = ent.usuarioClave,
                        UsuarioCodigo = ent.usuarioCodigo,
                        UsuarioDescripcion = ent.usuarioDescripcion,
                    };
                    result.Entidad = nr; 
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

    }

}