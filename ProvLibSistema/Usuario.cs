using LibEntitySistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibSistema
{

    public partial class Provider : ILibSistema.IProvider 
    {

        public DtoLib.ResultadoEntidad<DtoLibSistema.Usuario.Ficha> Usuario_Principal()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Usuario.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent = cnn.usuarios.Find("0000000001");

                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] USUARIO PRINCIPAL NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var nr = new DtoLibSistema.Usuario.Ficha()
                    {
                        auto = ent.auto,
                        codigo = ent.codigo,
                        nombre = ent.nombre,
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