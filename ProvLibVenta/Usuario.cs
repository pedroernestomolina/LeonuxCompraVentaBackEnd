using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibVenta
{
    
    public partial class Provider: ILibVentas.IProvider 
    {

        public DtoLib.ResultadoEntidad<DtoLibVenta.Usuario.Ficha> Usuario(string auto)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibVenta.Usuario.Ficha>();

            try
            {
                using (var cnn = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var q = cnn.usuarios.Find(auto);
                    if (q == null)
                    {
                        result.Mensaje = "[ ID ] USUARIO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var autoGrupo = q.auto_grupo;
                    var qGrupo = cnn.usuarios_grupo.Find(autoGrupo);
                    if (qGrupo == null)
                    {
                        result.Mensaje = "GRUPO AL CUAL PERTENECE EL USUARIO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var codigoGrupo=qGrupo.nombre;
                    var descripcionGrupo="";
                    var ent = new DtoLibVenta.Usuario.Ficha()
                    {
                        Auto = q.auto,
                        Codigo=q.codigo,
                        Descripcion=q.nombre,
                        AutoGrupo=autoGrupo,
                        CodigoGrupo=codigoGrupo,
                        DescripcionGrupo=descripcionGrupo
                    };
                    result.Entidad = ent;
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