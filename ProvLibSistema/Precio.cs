using LibEntitySistema;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvLibSistema
{

    public partial class Provider : ILibSistema.IProvider 
    {

        public DtoLib.ResultadoLista<DtoLibSistema.Precio.Resumen> Precio_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibSistema.Precio.Resumen>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent= cnn.empresa.FirstOrDefault();
                    if (ent == null) 
                    {
                        result.Mensaje = "ENTIDAD [ EMPRESA ] NO ENCONTRADA";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var _des = "";
                    _des = ent.precio_1.Trim();
                    var list = new List<DtoLibSistema.Precio.Resumen>();
                    var p1 = new DtoLibSistema.Precio.Resumen()
                    {
                        id = "1",
                        descripcion =  _des==""?"Precio 1":_des,
                    };
                    list.Add(p1);

                    _des = ent.precio_2.Trim();
                    var p2 = new DtoLibSistema.Precio.Resumen()
                    {
                        id = "2",
                        descripcion = _des == "" ? "Precio 2" : _des,
                    };
                    list.Add(p2);

                    _des = ent.precio_3.Trim();
                    var p3 = new DtoLibSistema.Precio.Resumen()
                    {
                        id = "3",
                        descripcion = _des == "" ? "Precio 3" : _des,
                    };
                    list.Add(p3);

                    _des = ent.precio_4.Trim();
                    var p4 = new DtoLibSistema.Precio.Resumen()
                    {
                        id = "4",
                        descripcion = _des == "" ? "Precio 4" : _des,
                    };
                    list.Add(p4);

                    _des = ent.precio_5.Trim();
                    var p5 = new DtoLibSistema.Precio.Resumen()
                    {
                        id = "5",
                        descripcion = _des == "" ? "Precio 5" : _des,
                    };
                    list.Add(p5);

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

        public DtoLib.ResultadoEntidad<DtoLibSistema.Precio.Etiquetar.Ficha> Precio_Etiquetar_GetFicha()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Precio.Etiquetar.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent = cnn.empresa.FirstOrDefault();
                    if (ent == null)
                    {
                        result.Mensaje = "ENTIDAD [ EMPRESA ] NO ENCONTRADA";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var p1 = new DtoLibSistema.Precio.Ficha()
                    {
                        id = "1",
                        descripcion = ent.precio_1,
                    };
                    var p2 = new DtoLibSistema.Precio.Ficha()
                    {
                        id = "2",
                        descripcion = ent.precio_2,
                    };
                    var p3 = new DtoLibSistema.Precio.Ficha()
                    {
                        id = "3",
                        descripcion = ent.precio_3,
                    };
                    var p4 = new DtoLibSistema.Precio.Ficha()
                    {
                        id = "4",
                        descripcion = ent.precio_4,
                    };
                    var p5 = new DtoLibSistema.Precio.Ficha()
                    {
                        id = "5",
                        descripcion = ent.precio_5,
                    };
                    var nr = new DtoLibSistema.Precio.Etiquetar.Ficha()
                    {
                        Precio1 = p1,
                        Precio2 = p2,
                        Precio3 = p3,
                        Precio4 = p4,
                        Precio5 = p5,
                    };

                    result.Entidad =nr ;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.Resultado Precio_Etiquetar_Actualizar(DtoLibSistema.Precio.Etiquetar.Actualizar ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var sql = "";

                        var p1 = new MySqlParameter ("@precio1", ficha.descripcion_1);
                        var p2 = new MySqlParameter("@precio2", ficha.descripcion_2);
                        var p3 = new MySqlParameter("@precio3", ficha.descripcion_3);
                        var p4 = new MySqlParameter("@precio4", ficha.descripcion_4);
                        var p5 = new MySqlParameter("@precio5", ficha.descripcion_5);
                        sql = "update empresa set precio_1=@precio1, precio_2=@precio2, precio_3=@precio3, precio_4=@precio4, precio_5=@precio5";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql, p1,p2,p3,p4,p5);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ENTIDAD EMPRESA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
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