using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvPos
{
    
    public partial class Provider : IPos.IProvider
    {

        public DtoLib.ResultadoEntidad<DtoLibPos.Configuracion.BusquedaCliente.Entidad.Ficha> Configuracion_BusquedaCliente()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Configuracion.BusquedaCliente.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL01");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var nr = new DtoLibPos.Configuracion.BusquedaCliente.Entidad.Ficha()
                    {
                        Usuario = ent.usuario,
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