using LibEntitySistema;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvLibSistema
{
    
    public partial class Provider : ILibSistema.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibSistema.Vendedor.Lista.Ficha> Vendedor_GetLista(DtoLibSistema.Vendedor.Lista.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibSistema.Vendedor.Lista.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var sql = @"select v.auto as id, v.codigo, v.nombre, v.ci as ciRif, v.estatus  
                        FROM vendedores as v 
                        where 1=1 ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();

                    var list = cnn.Database.SqlQuery<DtoLibSistema.Vendedor.Lista.Ficha>(sql, p1, p2, p3, p4).ToList();
                    rt.Lista = list;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoEntidad<DtoLibSistema.Vendedor.Entidad.Ficha> Vendedor_GetFicha_ById(string id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Vendedor.Entidad.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent = cnn.vendedores.Find(id);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] VENDEDOR NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var nr = new DtoLibSistema.Vendedor.Entidad.Ficha()
                    {
                        advertencia = ent.advertencia,
                        ciRif = ent.ci,
                        codigo = ent.codigo,
                        contacto = ent.contacto,
                        direccion = ent.direccion,
                        email = ent.email,
                        estatus = ent.estatus,
                        fechaAlta = ent.fecha_alta,
                        fechaBaja = ent.fecha_baja,
                        id = ent.auto,
                        memo = ent.memo,
                        nombre = ent.nombre,
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

        public DtoLib.ResultadoAuto Vendedor_AgregarFicha(DtoLibSistema.Vendedor.Agregar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var fechaNula = new DateTime(2000,1,1);

                        var sql = "update sistema_contadores set a_vendedores=a_vendedores+1";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var auto= cnn.Database.SqlQuery<int>("select a_vendedores from sistema_contadores").FirstOrDefault();
                        var id= auto.ToString().Trim().PadLeft(10, '0');

                        var ent = new vendedores()
                        {
                            auto = id,
                            advertencia = ficha.advertencia,
                            castigop = 0.0m,
                            ci = ficha.ciRif,
                            cobranza_1 = 0.0m,
                            cobranza_2 = 0.0m,
                            cobranza_3 = 0.0m,
                            cobranza_4 = 0.0m,
                            cobranza_g = 0.0m,
                            codigo = ficha.codigo,
                            contacto = ficha.contacto,
                            direccion = ficha.direccion,
                            email = ficha.email,
                            estatus = "Activo",
                            estatus_cobranza = "0",
                            estatus_departamento = "0",
                            estatus_f1 = "0",
                            estatus_f2 = "0",
                            estatus_f3 = "0",
                            estatus_f4 = "0",
                            estatus_f5 = "0",
                            estatus_f6 = "0",
                            estatus_f7 = "0",
                            estatus_f8 = "0",
                            estatus_ventas = "0",
                            fecha_alta = fechaSistema.Date,
                            fecha_baja = fechaNula,
                            memo = ficha.memo,
                            nombre = ficha.nombre,
                            telefono = ficha.telefono,
                            tolerancia = 0,
                            ventas_1 = 0.0m,
                            ventas_2 = 0.0m,
                            ventas_3 = 0.0m,
                            ventas_4 = 0.0m,
                            ventas_g = 0.0m,
                            website = ficha.webSite,
                        };
                        cnn.vendedores.Add(ent);
                        cnn.SaveChanges();

                        ts.Complete();
                        result.Auto = id;
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                var dbUpdateEx = ex as System.Data.Entity.Infrastructure.DbUpdateException;
                var sqlEx = dbUpdateEx.InnerException;
                if (sqlEx != null)
                {
                    var exx = (MySql.Data.MySqlClient.MySqlException)sqlEx.InnerException;
                    if (exx != null)
                    {
                        if (exx.Number == 1452)
                        {
                            result.Mensaje = "PROBLEMA DE CLAVE FORANEA" + Environment.NewLine + exx.Message;
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        else
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

        public DtoLib.Resultado Vendedor_EditarFicha(DtoLibSistema.Vendedor.Editar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var fechaNula = new DateTime(2000,1,1);

                        var ent = cnn.vendedores.Find(ficha.id);
                        if (ent == null) 
                        {
                            result.Mensaje = "[ ID ] VENDEDOR NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        ent.advertencia = ficha.advertencia;
                        ent.ci = ficha.ciRif;
                        ent.codigo = ficha.codigo;
                        ent.contacto = ficha.contacto;
                        ent.direccion = ficha.direccion;
                        ent.email = ficha.email;
                        ent.memo = ficha.memo;
                        ent.nombre = ficha.nombre;
                        ent.telefono = ficha.telefono;
                        ent.website = ficha.webSite;

                        cnn.SaveChanges();
                        ts.Complete();
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                var dbUpdateEx = ex as System.Data.Entity.Infrastructure.DbUpdateException;
                var sqlEx = dbUpdateEx.InnerException;
                if (sqlEx != null)
                {
                    var exx = (MySql.Data.MySqlClient.MySqlException)sqlEx.InnerException;
                    if (exx != null)
                    {
                        if (exx.Number == 1452)
                        {
                            result.Mensaje = "PROBLEMA DE CLAVE FORANEA" + Environment.NewLine + exx.Message;
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        else
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

        public DtoLib.Resultado Vendedor_Activar(DtoLibSistema.Vendedor.ActivarInactivar.Ficha ficha)
        {
            var rt = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var fechaNula = new DateTime(2000, 1, 1);

                        var entVnd= cnn.vendedores.Find(ficha.id);
                        if (entVnd == null)
                        {
                            rt.Mensaje = "[ ID ] VENDEDOR NO ENCONTRADO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }
                        if (entVnd.estatus.Trim().ToUpper() == "ACTIVO")
                        {
                            rt.Mensaje = "VENDEDOR YA ACTIVO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }

                        entVnd.estatus = "Activo";
                        entVnd.fecha_baja = fechaNula;
                        cnn.SaveChanges();
                        ts.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.Resultado Vendedor_Inactivar(DtoLibSistema.Vendedor.ActivarInactivar.Ficha ficha)
        {
            var rt = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var fechaNula = new DateTime(2000, 1, 1);

                        var entVnd= cnn.vendedores.Find(ficha.id);
                        if (entVnd == null)
                        {
                            rt.Mensaje = "[ ID ] VENDEDOR NO ENCONTRADO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }
                        if (entVnd.estatus.Trim().ToUpper() == "INACTIVO")
                        {
                            rt.Mensaje = "VENDEDOR YA INACTIVO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }

                        entVnd.estatus = "Inactivo";
                        entVnd.fecha_baja = fechaSistema.Date;
                        cnn.SaveChanges();
                        ts.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        //

        public DtoLib.Resultado Vendedor_Validar_Agregar(DtoLibSistema.Vendedor.Agregar.Ficha ficha)
        {
            var rt = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    if (ficha.ciRif.Trim() == "")
                    {
                        rt.Mensaje = "[ CI/RIF ] CAMPO NO PUEDE ESTAR VACIO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    var entVnd = cnn.vendedores.FirstOrDefault(f => f.ci.Trim().ToUpper() == ficha.ciRif);
                    if (entVnd != null)
                    {
                        rt.Mensaje = "[ CI/RIF ] VENDEDOR YA REGISTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    };
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.Resultado Vendedor_Validar_Editar(DtoLibSistema.Vendedor.Editar.Ficha ficha)
        {
            var rt = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent = cnn.vendedores.Find(ficha.id);
                    if (ent == null)
                    {
                        rt.Mensaje = "[ ID ] VENDEDOR NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    if (ent.estatus.Trim().ToUpper() != "ACTIVO")
                    {
                        rt.Mensaje = "VENDEDOR EN ESTADO INACTIVO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    if (ficha.ciRif.Trim() == "")
                    {
                        rt.Mensaje = "[ CI/RIF ] CAMPO NO PUEDE ESTAR VACIO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    var entVnd = cnn.vendedores.FirstOrDefault(f => f.ci.Trim().ToUpper() == ficha.ciRif && f.auto != ficha.id);
                    if (entVnd != null)
                    {
                        rt.Mensaje = "[ CI/RIF ] VENDEDOR YA REGISTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    };
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

    }

}