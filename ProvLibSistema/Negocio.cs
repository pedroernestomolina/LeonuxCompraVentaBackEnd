using LibEntitySistema;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvLibSistema
{
    
    public partial class Provider : ILibSistema.IProvider
    {

        public DtoLib.ResultadoEntidad<DtoLibSistema.Negocio.Entidad.Ficha> Negocio_GetEntidad_ByAuto(string autoEmpresa)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Negocio.Entidad.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent = cnn.empresa .Find(autoEmpresa);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] ENTIDAD EMPRESA NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var nr = new DtoLibSistema.Negocio.Entidad.Ficha()
                    {
                        auto = ent.auto,
                        ciudad = ent.ciudad,
                        codPostal = ent.codigo_postal,
                        codSucursal = ent.codigo_sucursal,
                        contacto = ent.contacto,
                        direccion = ent.direccion,
                        email = ent.email,
                        estado = ent.estado,
                        fax = ent.fax,
                        nombre = ent.nombre,
                        pais = ent.pais,
                        rif = ent.rif,
                        sucursal = ent.sucursal,
                        telefono = ent.telefono,
                        webSite = ent.website,
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

        public DtoLib.Resultado Negocio_EditarFicha(DtoLibSistema.Negocio.Editar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent = cnn.empresa.Find(ficha.auto);
                        if (ent == null)
                        {
                            result.Mensaje = "[ ID ] ENTIDAD EMPRESA NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        ent.ciudad = ficha.ciudad;
                        ent.codigo_postal = ficha.codPostal;
                        ent.contacto = ficha.contacto;
                        ent.direccion = ficha.direccion;
                        ent.email = ficha.email;
                        ent.estado = ficha.estado;
                        ent.fax = ficha.fax;
                        ent.nombre = ficha.nombre;
                        ent.pais = ficha.pais;
                        ent.rif = ficha.rif;
                        ent.telefono = ficha.telefono;
                        ent.website = ficha.webSite;
                        cnn.SaveChanges();
                        ts.Complete();
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                var dbUpdateEx = ex as DbUpdateException;
                var sqlEx = dbUpdateEx.InnerException;
                if (sqlEx != null)
                {
                    var exx = (MySql.Data.MySqlClient.MySqlException)sqlEx.InnerException;
                    if (exx != null)
                    {
                        if (exx.Number == 1451)
                        {
                            result.Mensaje = "REGISTRO CONTIENE DATA RELACIONADA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (exx.Number == 1062)
                        {
                            result.Mensaje = exx.Message;
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                    }
                }
                result.Mensaje = ex.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
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