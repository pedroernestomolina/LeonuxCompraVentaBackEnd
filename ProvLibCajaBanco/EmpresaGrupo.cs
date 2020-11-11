using LibEntityCajaBanco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibCajaBanco
{
 
    public partial class Provider : ILibCajaBanco.IProvider 
    {

        public DtoLib.ResultadoEntidad<DtoLibCajaBanco.EmpresaGrupo.Ficha> EmpresaGrupo_GetFicha(string autoGrupo)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCajaBanco.EmpresaGrupo.Ficha>();

            try
            {
                using (var cnn = new cajaBancoEntities(_cnCajBanco.ConnectionString))
                {
                    var ent = cnn.empresa_grupo .Find(autoGrupo);
                    if (ent == null)
                    {
                        result.Mensaje = "[ AUTO ] EMPRESA GRUPO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                    }

                    var r = new DtoLibCajaBanco.EmpresaGrupo.Ficha()
                    {
                        auto = ent.auto,
                        nombre = ent.nombre,
                        idPrecio = ent.idPrecio
                    };

                    result.Entidad = r;
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