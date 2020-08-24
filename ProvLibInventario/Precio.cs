using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvLibInventario
{
    
    public partial class Provider : ILibInventario.IProvider
    {

        public DtoLib.ResultadoEntidad<DtoLibInventario.Precio.Historico.Resumen> HistoricoPrecio_GetLista(DtoLibInventario.Precio.Historico.Filtro filtro)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Precio.Historico.Resumen>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {

                    var prd = cnn.productos.Find(filtro.autoProducto);
                    if (prd == null) 
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO";
                        return result;
                    }

                    var et1 = "Precio 1";
                    var et2 = "Precio 2";
                    var et3 = "Precio 3";
                    var et4 = "Precio 4";
                    var et5 = "Precio 5";
                    var emp = cnn.empresa.FirstOrDefault();
                    if (emp != null)
                    {
                        if (emp.precio_1.Trim() != "") 
                        {
                            et1 = emp.precio_1;
                        }
                        if (emp.precio_2.Trim() != "")
                        {
                            et2 = emp.precio_2;
                        }
                        if (emp.precio_3.Trim() != "")
                        {
                            et3 = emp.precio_3;
                        }
                        if (emp.precio_4.Trim() != "")
                        {
                            et4 = emp.precio_4;
                        }
                        if (emp.precio_5.Trim() != "")
                        {
                            et5 = emp.precio_5;
                        }
                    }

                    var q = cnn.productos_precios.Where(f => f.auto_producto == filtro.autoProducto).ToList();
                    var list = new List<DtoLibInventario.Precio.Historico.Data>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var r = new DtoLibInventario.Precio.Historico.Data()
                                {
                                    estacion = s.estacion,
                                    fecha = s.fecha,
                                    hora = s.hora,
                                    nota = s.nota,
                                    usuario = s.usuario,
                                    precio=s.precio,
                                    idPrecio=s.precio_id,
                                };
                                switch (s.precio_id) 
                                {
                                    case "1":
                                        r.etqPrecio = et1;
                                        break;
                                    case "2":
                                        r.etqPrecio = et2;
                                        break;
                                    case "3":
                                        r.etqPrecio = et3;
                                        break;
                                    case "4":
                                        r.etqPrecio = et4;
                                        break;
                                    case "PTO":
                                        r.etqPrecio = et5;
                                        break;
                                }
                                return r;
                            }).ToList();
                        }
                    }
                    var nr = new DtoLibInventario.Precio.Historico.Resumen();
                    nr.data = list;
                    nr.codigo = prd.codigo;
                    nr.descripcion = prd.nombre;

                    result.Entidad=nr;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Precio.PrecioCosto.Ficha> PrecioCosto_GetFicha(string autoPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.Precio.PrecioCosto.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var entPrd = cnn.productos.Find(autoPrd);
                    if (entPrd == null)
                    {
                        rt.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }

                    var entEmpresa = cnn.empresa.FirstOrDefault();
                    if (entEmpresa == null)
                    {
                        rt.Mensaje = "ENTIDAD [ EMPRESA ] NO ENCONTRADA";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }

                    var entTasa = cnn.empresa_tasas.Find(entPrd.auto_tasa);
                    var _fechaUltActCosto = "";
                    if (entPrd.fecha_ult_costo!=new DateTime(2000,01,01))
                    {
                        _fechaUltActCosto = entPrd.fecha_ult_costo.ToShortDateString();
                    }

                    var precio = new DtoLibInventario.Precio.PrecioCosto.Ficha()
                    {
                        codigo = entPrd.codigo,
                        nombre = entPrd.nombre_corto,
                        descripcion = entPrd.nombre,
                        tasaIva = entPrd.tasa,
                        nombreTasaIva = entTasa.nombre,
                        admDivisa = entPrd.estatus_divisa,

                        etiqueta1 = entEmpresa.precio_1,
                        etiqueta2 = entEmpresa.precio_2,
                        etiqueta3 = entEmpresa.precio_3,
                        etiqueta4 = entEmpresa.precio_4,
                        etiqueta5 = entEmpresa.precio_5,

                        contenido1 = entPrd.contenido_1,
                        contenido2 = entPrd.contenido_2,
                        contenido3 = entPrd.contenido_3,
                        contenido4 = entPrd.contenido_4,
                        contenido5 = entPrd.contenido_pto,

                        precioNeto1 = entPrd.precio_1,
                        precioNeto2 = entPrd.precio_2,
                        precioNeto3 = entPrd.precio_3,
                        precioNeto4 = entPrd.precio_4,
                        precioNeto5 = entPrd.precio_pto,

                        precioFullDivisa1 = entPrd.pdf_1,
                        precioFullDivisa2 = entPrd.pdf_2,
                        precioFullDivisa3 = entPrd.pdf_3,
                        precioFullDivisa4 = entPrd.pdf_4,
                        precioFullDivisa5 = entPrd.pdf_pto,

                        utilidad1 = entPrd.utilidad_1,
                        utilidad2 = entPrd.utilidad_2,
                        utilidad3 = entPrd.utilidad_3,
                        utilidad4 = entPrd.utilidad_4,
                        utilidad5 = entPrd.utilidad_pto,

                        autoEmp1 = entPrd.auto_precio_1,
                        autoEmp2 = entPrd.auto_precio_2,
                        autoEmp3 = entPrd.auto_precio_3,
                        autoEmp4 = entPrd.auto_precio_4,
                        autoEmp5 = entPrd.auto_precio_pto,

                        costoUnd = entPrd.costo_und,
                        costoDivisa = entPrd.divisa,
                        contempCompra=entPrd.contenido_compras,
                        empCompra= entPrd.productos_medida2.nombre,
                        fechaUltActualizacion=_fechaUltActCosto,
                    };

                    rt.Entidad = precio;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.Resultado PrecioProducto_Actualizar(DtoLibInventario.Precio.Editar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var entPrd = cnn.productos.Find(ficha.autoProducto);
                        if (entPrd == null)
                        {
                            result.Mensaje = "[ ID ] Producto, No Encontrado";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        entPrd.fecha_cambio = fechaSistema.Date;

                        entPrd.auto_precio_1 = ficha.precio_1.autoEmp;
                        entPrd.precio_1 = ficha.precio_1.precioNeto;
                        entPrd.utilidad_1 = ficha.precio_1.utilidad ;
                        entPrd.pdf_1 = ficha.precio_1.precio_divisa_Neto;
                        entPrd.contenido_1 = ficha.precio_1.contenido;

                        entPrd.auto_precio_2 = ficha.precio_2.autoEmp;
                        entPrd.precio_2 = ficha.precio_2.precioNeto;
                        entPrd.utilidad_2 = ficha.precio_2.utilidad;
                        entPrd.pdf_2 = ficha.precio_2.precio_divisa_Neto;
                        entPrd.contenido_2 = ficha.precio_2.contenido;

                        entPrd.auto_precio_3 = ficha.precio_3.autoEmp;
                        entPrd.precio_3 = ficha.precio_3.precioNeto;
                        entPrd.utilidad_3 = ficha.precio_3.utilidad;
                        entPrd.pdf_3 = ficha.precio_3.precio_divisa_Neto;
                        entPrd.contenido_3 = ficha.precio_3.contenido;

                        entPrd.auto_precio_4 = ficha.precio_4.autoEmp;
                        entPrd.precio_4 = ficha.precio_4.precioNeto;
                        entPrd.utilidad_4 = ficha.precio_4.utilidad;
                        entPrd.pdf_4 = ficha.precio_4.precio_divisa_Neto;
                        entPrd.contenido_4 = ficha.precio_4.contenido;

                        entPrd.auto_precio_pto = ficha.precio_5.autoEmp;
                        entPrd.precio_pto = ficha.precio_5.precioNeto;
                        entPrd.utilidad_pto = ficha.precio_5.utilidad;
                        entPrd.pdf_pto = ficha.precio_5.precio_divisa_Neto;
                        entPrd.contenido_pto= ficha.precio_5.contenido;
                        cnn.SaveChanges();

                        foreach (var it in ficha.historia) 
                        {
                            var entHist = new productos_precios()
                            {
                                auto_producto = ficha.autoProducto,
                                estacion = ficha.estacion,
                                fecha = fechaSistema.Date,
                                hora = fechaSistema.ToShortTimeString(),
                                usuario = ficha.nombreUsuario,
                                nota = it.nota,
                                precio = it.precio,
                                precio_id = it.precio_id,
                            };
                            cnn.productos_precios.Add(entHist);
                            cnn.SaveChanges();
                        }

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