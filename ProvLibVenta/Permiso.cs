using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibVenta
{

    public partial class Provider : ILibVentas.IProvider
    {

        class permiso
        {
            public string estatus { get; set; }
            public string seguridad { get; set; }
        }


        public DtoLib.ResultadoEntidad<DtoLibVenta.Permiso.Ficha> Venta_DarDescuento_Item(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibVenta.Permiso.Ficha>();

            try
            {
                using (var ctx = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    permiso cnf = ctx.Database.SqlQuery<permiso>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo={0} and codigo_funcion='0801020000'", autoGrupoUsuario).FirstOrDefault();
                    if (cnf == null)
                    {
                        result.Entidad = new DtoLibVenta.Permiso.Ficha();
                        return result;
                    }
                    var nt = new DtoLibVenta.Permiso.Ficha();
                    if (cnf.estatus.Trim().ToUpper() == "1")
                    {
                        nt.IsHabilitado = true;
                    }
                    switch (cnf.seguridad.Trim().ToUpper())
                    {
                        case "NINGUNA":
                            nt.NivelSeguridad = DtoLibVenta.Permiso.Enumerados.EnumNivelSeguridad.Niguna;
                            break;
                        case "MINIMA":
                            nt.NivelSeguridad = DtoLibVenta.Permiso.Enumerados.EnumNivelSeguridad.Minima;
                            break;
                        case "MEDIA":
                            nt.NivelSeguridad = DtoLibVenta.Permiso.Enumerados.EnumNivelSeguridad.Media;
                            break;
                        case "MAXIMA":
                            nt.NivelSeguridad = DtoLibVenta.Permiso.Enumerados.EnumNivelSeguridad.Maxima;
                            break;
                    }
                    result.Entidad = nt;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibVenta.Permiso.Ficha> Venta_Eliminar_Item(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibVenta.Permiso.Ficha>();

            try
            {
                using (var ctx = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    permiso cnf = ctx.Database.SqlQuery<permiso>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo={0} and codigo_funcion='0801010000'", autoGrupoUsuario).FirstOrDefault();
                    if (cnf == null)
                    {
                        result.Entidad = new DtoLibVenta.Permiso.Ficha();
                        return result;
                    }
                    var nt = new DtoLibVenta.Permiso.Ficha();
                    if (cnf.estatus.Trim().ToUpper() == "1")
                    {
                        nt.IsHabilitado = true;
                    }
                    switch (cnf.seguridad.Trim().ToUpper())
                    {
                        case "NINGUNA":
                            nt.NivelSeguridad = DtoLibVenta.Permiso.Enumerados.EnumNivelSeguridad.Niguna;
                            break;
                        case "MINIMA":
                            nt.NivelSeguridad = DtoLibVenta.Permiso.Enumerados.EnumNivelSeguridad.Minima;
                            break;
                        case "MEDIA":
                            nt.NivelSeguridad = DtoLibVenta.Permiso.Enumerados.EnumNivelSeguridad.Media;
                            break;
                        case "MAXIMA":
                            nt.NivelSeguridad = DtoLibVenta.Permiso.Enumerados.EnumNivelSeguridad.Maxima;
                            break;
                    }
                    result.Entidad = nt;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibVenta.Permiso.Ficha> Venta_PrecioLibre_Item(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibVenta.Permiso.Ficha>();

            try
            {
                using (var ctx = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    permiso cnf = ctx.Database.SqlQuery<permiso>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo={0} and codigo_funcion='0801040000'",autoGrupoUsuario).FirstOrDefault();
                    if (cnf == null)
                    {
                        result.Entidad = new DtoLibVenta.Permiso.Ficha();
                        return result;
                    }
                    var nt = new DtoLibVenta.Permiso.Ficha();
                    if (cnf.estatus.Trim().ToUpper() == "1")
                    {
                        nt.IsHabilitado=true;
                    }
                    switch (cnf.seguridad.Trim().ToUpper())
                    {
                        case "NINGUNA":
                            nt.NivelSeguridad= DtoLibVenta.Permiso.Enumerados.EnumNivelSeguridad.Niguna;
                            break;
                        case "MINIMA":
                            nt.NivelSeguridad = DtoLibVenta.Permiso.Enumerados.EnumNivelSeguridad.Minima;
                            break;
                        case "MEDIA":
                            nt.NivelSeguridad = DtoLibVenta.Permiso.Enumerados.EnumNivelSeguridad.Media;
                            break;
                        case "MAXIMA":
                            nt.NivelSeguridad = DtoLibVenta.Permiso.Enumerados.EnumNivelSeguridad.Maxima;
                            break;
                    }
                    result.Entidad = nt;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibVenta.Permiso.Ficha> Venta_DescuentoGlobal(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibVenta.Permiso.Ficha>();

            try
            {
                using (var ctx = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    permiso cnf = ctx.Database.SqlQuery<permiso>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo={0} and codigo_funcion='0801030000'",autoGrupoUsuario).FirstOrDefault();
                    if (cnf == null)
                    {
                        result.Entidad = new DtoLibVenta.Permiso.Ficha();
                        return result;
                    }
                    var nt = new DtoLibVenta.Permiso.Ficha();
                    if (cnf.estatus.Trim().ToUpper() == "1")
                    {
                        nt.IsHabilitado = true;
                    }
                    switch (cnf.seguridad.Trim().ToUpper())
                    {
                        case "NINGUNA":
                            nt.NivelSeguridad = DtoLibVenta.Permiso.Enumerados.EnumNivelSeguridad.Niguna;
                            break;
                        case "MINIMA":
                            nt.NivelSeguridad = DtoLibVenta.Permiso.Enumerados.EnumNivelSeguridad.Minima;
                            break;
                        case "MEDIA":
                            nt.NivelSeguridad = DtoLibVenta.Permiso.Enumerados.EnumNivelSeguridad.Media;
                            break;
                        case "MAXIMA":
                            nt.NivelSeguridad = DtoLibVenta.Permiso.Enumerados.EnumNivelSeguridad.Maxima;
                            break;
                    }
                    result.Entidad = nt;
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