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

        public DtoLib.ResultadoLista<DtoLibSistema.SerieFiscal.Lista.Ficha> SerieFiscal_GetLista(DtoLibSistema.SerieFiscal.Lista.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibSistema.SerieFiscal.Lista.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var sql = @"select sf.auto as id, sf.serie, sf.correlativo, sf.control, sf.estatus  
                        FROM empresa_series_fiscales as sf 
                        where 1=1 ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();

                    var list = cnn.Database.SqlQuery<DtoLibSistema.SerieFiscal.Lista.Ficha>(sql, p1, p2, p3, p4).ToList();
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

        public DtoLib.ResultadoEntidad<DtoLibSistema.SerieFiscal.Entidad.Ficha> SerieFiscal_GetFicha_ById(string id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.SerieFiscal.Entidad.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent = cnn.empresa_series_fiscales.Find(id);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] SERIE FISCAL NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var nr = new DtoLibSistema.SerieFiscal.Entidad.Ficha()
                    {
                        control = ent.control,
                        correlativo = ent.correlativo,
                        estatus = ent.estatus,
                        estatusFactura = ent.estatus_factura,
                        estatusNtCredito = ent.estatus_nc,
                        estatusNtDebito = ent.estatus_nd,
                        estatusNtEntrega = ent.estatus_ne,
                        id = ent.auto,
                        serie = ent.serie,
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

        public DtoLib.ResultadoAuto SerieFiscal_AgregarFicha(DtoLibSistema.SerieFiscal.Agregar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var fechaNula = new DateTime(2000, 1, 1);

                        var sql = "update sistema_contadores set a_empresa_series_fiscales=a_empresa_series_fiscales+1";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var auto = cnn.Database.SqlQuery<int>("select a_empresa_series_fiscales from sistema_contadores").FirstOrDefault();
                        var id = auto.ToString().Trim().PadLeft(10, '0');

                        var ent = new empresa_series_fiscales()
                        {
                            auto = id,
                            control = ficha.control,
                            correlativo = ficha.correlativo,
                            estatus = "Activo",
                            estatus_factura = ficha.estatusFactura,
                            estatus_nc = ficha.estatusNtCredito,
                            estatus_nd = ficha.estatusNtDebito,
                            estatus_ne = ficha.estatusNtEntrega,
                            serie = ficha.serie,
                        };
                        cnn.empresa_series_fiscales.Add(ent);
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

        public DtoLib.Resultado SerieFiscal_EditarFicha(DtoLibSistema.SerieFiscal.Editar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var fechaNula = new DateTime(2000, 1, 1);

                        var ent = cnn.empresa_series_fiscales.Find(ficha.id);
                        if (ent == null)
                        {
                            result.Mensaje = "[ ID ] SERIE FISCAL NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        ent.control = ficha.control;
                        ent.correlativo = ficha.correlativo;
                        ent.serie = ficha.serie;

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

        public DtoLib.Resultado SerieFiscal_Activar(DtoLibSistema.SerieFiscal.ActivarInactivar.Ficha ficha)
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

                        var entSer= cnn.empresa_series_fiscales.Find(ficha.id);
                        if (entSer == null)
                        {
                            rt.Mensaje = "[ ID ] SERIE NO ENCONTRADO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }
                        if (entSer.estatus.Trim().ToUpper() == "ACTIVO")
                        {
                            rt.Mensaje = "SERTIE YA ACTIVA";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }

                        entSer.estatus = "Activo";
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

        public DtoLib.Resultado SerieFiscal_Inactivar(DtoLibSistema.SerieFiscal.ActivarInactivar.Ficha ficha)
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

                        var entSer = cnn.empresa_series_fiscales.Find(ficha.id);
                        if (entSer == null)
                        {
                            rt.Mensaje = "[ ID ] SERIE NO ENCONTRADO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }
                        if (entSer.estatus.Trim().ToUpper() == "INACTIVO")
                        {
                            rt.Mensaje = "SERTIE YA INACTIVA";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }

                        entSer.estatus = "Inactivo";
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

        public DtoLib.Resultado SerieFiscal_Validar_Agregar(DtoLibSistema.SerieFiscal.Agregar.Ficha ficha)
        {
            var rt = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    if (ficha.serie.Trim() == "")
                    {
                        rt.Mensaje = "[ SERIE ] CAMPO NO PUEDE ESTAR VACIO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    var entSF= cnn.empresa_series_fiscales.FirstOrDefault(f => f.serie.Trim().ToUpper() == ficha.serie);
                    if (entSF != null)
                    {
                        rt.Mensaje = "[ SERIE ] YA REGISTRADA";
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

        public DtoLib.Resultado SerieFiscal_Validar_Editar(DtoLibSistema.SerieFiscal.Editar.Ficha ficha)
        {
            var rt = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent = cnn.empresa_series_fiscales.Find(ficha.id);
                    if (ent == null)
                    {
                        rt.Mensaje = "[ ID ] SERIE FISCAL NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    if (ficha.serie.Trim() == "")
                    {
                        rt.Mensaje = "[ SERIE ] CAMPO NO PUEDE ESTAR VACIO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    var entSF = cnn.empresa_series_fiscales.FirstOrDefault(f => f.serie.Trim().ToUpper() == ficha.serie && f.auto != ficha.id);
                    if (entSF != null)
                    {
                        rt.Mensaje = "[ SERIE ] YA REGISTRADA";
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