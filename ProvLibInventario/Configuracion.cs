using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibInventario
{

    public partial class Provider : ILibInventario.IProvider
    {

        public DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaBusqueda> Configuracion_PreferenciaBusqueda()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaBusqueda>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f=>f.codigo=="GLOBAL03");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var modo=DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaBusqueda.SinDefinir;
                    switch (ent.usuario.Trim().ToUpper()) 
                    {
                        case "CODIGO":
                            modo = DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaBusqueda.PorCodigo;
                            break;
                        case "NOMBRE":
                            modo = DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaBusqueda.PorNombre;
                            break;
                        case "REFERENCIA":
                            modo = DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaBusqueda.PorReferencia;
                            break;
                    }

                    result.Entidad = modo;
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