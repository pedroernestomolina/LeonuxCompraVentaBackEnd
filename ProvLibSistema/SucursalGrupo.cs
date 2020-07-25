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

        DtoLib.ResultadoLista<DtoLibSistema.GrupoSucursal.Resumen> ILibSistema.ISucursalGrupo.SucursalGrupo_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibSistema.GrupoSucursal.Resumen>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var q = cnn.empresa_grupo.ToList();

                    var list = new List<DtoLibSistema.GrupoSucursal.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var _precioDesc="";
                                var ent = cnn.empresa.FirstOrDefault();
                                if (ent!=null)
                                {
                                    switch (s.idPrecio)
                                    {
                                        case "1":
                                            _precioDesc = "Precio 1";
                                            if (ent.precio_1.Trim() != "") 
                                            {
                                                _precioDesc = ent.precio_1;
                                            }
                                            break;
                                        case "2":
                                            _precioDesc = "Precio 2";
                                            if (ent.precio_2.Trim() != "") 
                                            {
                                                _precioDesc = ent.precio_2;
                                            }
                                            break;
                                        case "3":
                                            _precioDesc = "Precio 3";
                                            if (ent.precio_3.Trim() != "") 
                                            {
                                                _precioDesc = ent.precio_3;
                                            }
                                            break;
                                        case "4":
                                            _precioDesc = "Precio 4";
                                            if (ent.precio_4.Trim() != "") 
                                            {
                                                _precioDesc = ent.precio_4;
                                            }
                                            break;
                                        case "5":
                                            _precioDesc = "Precio 5";
                                            if (ent.precio_5.Trim() != "") 
                                            {
                                                _precioDesc = ent.precio_5;
                                            }
                                            break;
                                    }
                                }

                                var r = new DtoLibSistema.GrupoSucursal.Resumen()
                                {
                                    auto = s.auto,
                                    nombre = s.nombre,
                                    precioId = s.idPrecio,
                                    precioDescripcion = _precioDesc,
                                };
                                return r;
                            }).ToList();
                        }
                    }
                    result.Lista = list;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibSistema.GrupoSucursal.Ficha> SucursalGrupo_GetFicha(string auto)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.GrupoSucursal.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent = cnn.empresa_grupo.Find(auto);
                    if (ent == null) 
                    {
                        result.Mensaje = "[ ID ] ENTIDAD GRUPO SUCURSAL NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var _precioDesc = "";
                    var entEmpresa = cnn.empresa.FirstOrDefault();
                    if (entEmpresa != null)
                    {
                        switch (ent.idPrecio)
                        {
                            case "1":
                                _precioDesc = "Precio 1";
                                if (entEmpresa.precio_1.Trim() != "")
                                {
                                    _precioDesc = entEmpresa.precio_1;
                                }
                                break;
                            case "2":
                                _precioDesc = "Precio 2";
                                if (entEmpresa.precio_2.Trim() != "")
                                {
                                    _precioDesc = entEmpresa.precio_2;
                                }
                                break;
                            case "3":
                                _precioDesc = "Precio 3";
                                if (entEmpresa.precio_3.Trim() != "")
                                {
                                    _precioDesc = entEmpresa.precio_3;
                                }
                                break;
                            case "4":
                                _precioDesc = "Precio 4";
                                if (entEmpresa.precio_4.Trim() != "")
                                {
                                    _precioDesc = entEmpresa.precio_4;
                                }
                                break;
                            case "5":
                                _precioDesc = "Precio 5";
                                if (entEmpresa.precio_5.Trim() != "")
                                {
                                    _precioDesc = entEmpresa.precio_5;
                                }
                                break;
                        }
                    }

                    var nr = new DtoLibSistema.GrupoSucursal.Ficha()
                    {
                        auto = ent.auto,
                        nombre = ent.nombre,
                        precioId = ent.idPrecio,
                        precioDescripcion = _precioDesc,
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

        public DtoLib.ResultadoAuto SucursalGrupo_Agregar(DtoLibSistema.GrupoSucursal.Agregar ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var sql = "update sistema_contadores set a_empresa_grupo=a_empresa_grupo+1";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var aEmpresaGrupo = cnn.Database.SqlQuery<int>("select a_empresa_grupo from sistema_contadores").FirstOrDefault();
                        var autoEmpresaGrupo = aEmpresaGrupo.ToString().Trim().PadLeft(10, '0');

                        var ent = new empresa_grupo()
                        {
                            auto = autoEmpresaGrupo,
                            nombre = ficha.nombre,
                            idPrecio = ficha.precioId,
                        };
                        cnn.empresa_grupo.Add(ent);
                        cnn.SaveChanges();

                        ts.Complete();
                        result.Auto = autoEmpresaGrupo;
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
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.Resultado SucursalGrupo_Editar(DtoLibSistema.GrupoSucursal.Editar ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent = cnn.empresa_grupo.Find(ficha.auto);
                        if (ent == null) 
                        {
                            result.Mensaje = "[ ID ] ENTIDAD GRUPO SUCURSAL NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        };

                        ent.nombre = ficha.nombre;
                        ent.idPrecio = ficha.precioId;
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
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                result.Mensaje = msg;
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