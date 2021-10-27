using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqLitePosOffLine
{

    public partial class Provider : IPosOffLine.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibPosOffLine.Producto.Resumen> ProductoLista(string xfiltro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPosOffLine.Producto.Resumen>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var filtro = xfiltro.Trim().ToUpper();
                    var q = cnn.Producto.ToList();
                    if (filtro != "")
                    {
                        if (filtro.Substring(0, 1) == "*")
                        {
                            filtro = filtro.Substring(1);
                            q = q.Where(w => w.nombrePrd.Contains(filtro)).ToList();
                        }
                        else
                        {
                            q = q.Where(w =>
                            {
                                var r = w.nombrePrd.Trim().ToUpper();
                                if (r.Length >= filtro.Length && r.Substring(0, filtro.Length) == filtro)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }).ToList();
                        }
                    }

                    var list = new List<DtoLibPosOffLine.Producto.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            result.Lista = q.Select(s =>
                            {
                                var isActivo = s.isActivo==1 ? true : false;
                                var r = new DtoLibPosOffLine.Producto.Resumen()
                                {
                                    Auto = s.auto,
                                    CodigoPrd = s.codigoPrd,
                                    NombrePrd = s.nombrePrd,
                                    IsActivo = isActivo,
                                };
                                return r;
                            }).ToList();
                        }
                        else
                        {
                            result.Lista = list;
                        }
                    }
                    else
                    {
                        result.Lista = list;
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<string> ProductoBuscarPorCodigoBarraPlu(string aBuscar)
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var filtro = aBuscar.Trim().ToUpper();

                    var entBarra = cnn.ProductoBarra.Find(aBuscar);
                    if (entBarra != null)
                    {
                        result.Entidad = entBarra.autoProducto;
                        return result;
                    }

                    var entProducto = cnn.Producto.Where(w => w.codigoPrd.Trim().ToUpper() == filtro || 
                        w.plu.Trim().ToUpper() == filtro).FirstOrDefault();
                    if (entProducto != null)
                    {
                        result.Entidad = entProducto.auto;
                        return result;
                    }
                    else 
                    {
                        result.Entidad = "";
                        return result;
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Producto.Ficha> Producto(string aBuscar)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Producto.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {

                    var fechaSistema = cnn.Database.SqlQuery<DateTime>("select date('now')").FirstOrDefault();

                    var entPrd = cnn.Producto.Find(aBuscar);
                    if (entPrd==null)
                    {
                        result.Mensaje = "PRODUCTO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };
                    
                    var isActivo = entPrd.isActivo == 1 ? true : false;
                    var isDivisa = entPrd.isDivisa == 1 ? true : false;
                    var isOferta = entPrd.isOferta == 1 ? true : false;
                    var isPesado = entPrd.isPesado == 1 ? true : false;
                    DateTime? ofertaDesde = null;
                    DateTime? ofertaHasta= null;
                    if (entPrd.ofertaDesde.Trim() != "") 
                    {
                        ofertaDesde= DateTime.Parse(entPrd.ofertaDesde);
                    }
                    if (entPrd.ofertaHasta.Trim() != "")
                    {
                        ofertaHasta = DateTime.Parse(entPrd.ofertaHasta);
                    }
                    var pv_1 =  new DtoLibPosOffLine.Producto.Precio()
                    {
                         Contenido=(int)entPrd.cont_1,
                         Decimales=entPrd.dec_1,
                         Neto=entPrd.precio_1,
                         Empaque=entPrd.emp_1,
                    };
                    var pv_2 =  new DtoLibPosOffLine.Producto.Precio()
                    {
                        Contenido = (int)entPrd.cont_2,
                        Decimales = entPrd.dec_2,
                        Neto = entPrd.precio_2,
                        Empaque = entPrd.emp_2,
                    };
                    var pv_3 = new DtoLibPosOffLine.Producto.Precio()
                    {
                        Contenido = (int)entPrd.cont_3,
                        Decimales = entPrd.dec_3,
                        Neto = entPrd.precio_3,
                        Empaque = entPrd.emp_3,
                    };
                    var pv_4 = new DtoLibPosOffLine.Producto.Precio()
                    {
                        Contenido = (int)entPrd.cont_4,
                        Decimales = entPrd.dec_4,
                        Neto = entPrd.precio_4,
                        Empaque = entPrd.emp_4,
                    };
                    var pv_5 = new DtoLibPosOffLine.Producto.Precio()
                    {
                        Contenido = (int)entPrd.cont_5,
                        Decimales = entPrd.dec_5,
                        Neto = entPrd.precio_5,
                        Empaque = entPrd.emp_5,
                    };
                    var pvMay_1 = new DtoLibPosOffLine.Producto.Precio()
                    {
                        Contenido = (int)entPrd.contMay_1,
                        Decimales = entPrd.decMay_1,
                        Neto = entPrd.precioMay_1.HasValue?entPrd.precioMay_1.Value:0.0m,
                        Empaque = entPrd.empMay_1,
                    };
                    var pvMay_2 = new DtoLibPosOffLine.Producto.Precio()
                    {
                        Contenido = (int)entPrd.contMay_2,
                        Decimales = entPrd.decMay_2,
                        Neto = entPrd.precioMay_2.HasValue ? entPrd.precioMay_2.Value : 0.0m,
                        Empaque = entPrd.empMay_2,
                    };

                    var r = new DtoLibPosOffLine.Producto.Ficha()
                    {
                        Auto = entPrd.auto,
                        AutoDepartamento=entPrd.autoDepartamento,
                        AutoGrupo = entPrd.autoGrupo,
                        AutoMarca = entPrd.autoMarca,
                        AutoSubGrupo = entPrd.autoSubGrupo,
                        AutoTasaIva=entPrd.autoTasaIva,
                        
                        Categoria=entPrd.categoria,
                        CodigoPrd = entPrd.codigoPrd,
                        CodDepartamento=entPrd.codDepartamento,
                        CodGrupo=entPrd.codGrupo,
                        CodigoPLU=entPrd.plu,

                        IsActivo= isActivo,
                        IsDivisa= isDivisa,
                        IsOferta= isOferta,
                        IsPesado= isPesado,

                        Marca=entPrd.marca,
                        Modelo=entPrd.modelo,
                        NombreDepartamento=entPrd.departamento,
                        NombreGrupo=entPrd.grupo,
                        NombrePrd = entPrd.nombrePrd,

                        OfertaDesde=ofertaDesde,
                        OfertaHasta=ofertaHasta,
                        OfertaPrecio=entPrd.ofertaprecio,
                        Pasillo=entPrd.pasilo,
                        Precio_1=pv_1,
                        Precio_2=pv_2,
                        Precio_3=pv_3,
                        Precio_4=pv_4,
                        Precio_5=pv_5,
                        PrecioMay_1=pvMay_1,
                        PrecioMay_2=pvMay_2,
                        Referencia = entPrd.referencia,
                        TasaImpuesto= entPrd.tasaImpuesto,

                        DiasEmpaqueGarantia=(int) entPrd.dias_Empaque_Garantia,
                        Costo=entPrd.costo,
                        CostoPromedio=entPrd.costoPromedio,
                        CostoUnidad=entPrd.costoUnidad,
                        CostoPromedioUnidad = entPrd.costoPromedioUnidad,
                        FechaServidor=fechaSistema,
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

        public DtoLib.ResultadoLista<DtoLibPosOffLine.Producto.Resumen> ProductoListaPlu()
        {
            var result = new DtoLib.ResultadoLista<DtoLibPosOffLine.Producto.Resumen>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var q = cnn.Producto.ToList();
                    q = q.Where(w => w.isPesado == 1).ToList();

                    var list = new List<DtoLibPosOffLine.Producto.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            result.Lista = q.Select(s =>
                            {
                                var isActivo = s.isActivo == 1 ? true : false;
                                var r = new DtoLibPosOffLine.Producto.Resumen()
                                {
                                    Auto = s.auto,
                                    CodigoPrd = s.codigoPrd,
                                    NombrePrd = s.nombrePrd,
                                    IsActivo = isActivo,
                                    CodigoPlu=s.plu,
                                    DiasEmpaqueGarantia=(int)s.dias_Empaque_Garantia,
                                };
                                return r;
                            }).ToList();
                        }
                        else
                        {
                            result.Lista = list;
                        }
                    }
                    else
                    {
                        result.Lista = list;
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoLista<DtoLibPosOffLine.Producto.Resumen> ProductoListaOferta()
        {
            var result = new DtoLib.ResultadoLista<DtoLibPosOffLine.Producto.Resumen>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var fechaSistema = cnn.Database.SqlQuery<DateTime>("select date('now')").FirstOrDefault();
                    var q = cnn.Producto.ToList();
                    q = q.Where(w => w.isOferta == 1).ToList();

                    var list = new List<DtoLibPosOffLine.Producto.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            result.Lista = q.Select(s =>
                            {
                                var isActivo = s.isActivo == 1 ? true : false;
                                DateTime? ofertaDesde = null;
                                DateTime? ofertaHasta = null;
                                if (s.ofertaDesde.Trim() != "")
                                {
                                    ofertaDesde = DateTime.Parse(s.ofertaDesde);
                                }
                                if (s.ofertaHasta.Trim() != "")
                                {
                                    ofertaHasta = DateTime.Parse(s.ofertaHasta);
                                }
                                var r = new DtoLibPosOffLine.Producto.Resumen()
                                {
                                    Auto = s.auto,
                                    CodigoPrd = s.codigoPrd,
                                    NombrePrd = s.nombrePrd,
                                    IsActivo = isActivo,
                                    CodigoPlu = s.plu,
                                    PrecioOferta=s.ofertaprecio,
                                    PrecioRegular=s.precio_1,
                                    OfertaDesde = ofertaDesde,
                                    OfertaHasta = ofertaHasta,
                                    FechaServidor=fechaSistema,
                                };
                                return r;
                            }).ToList();
                        }
                        else
                        {
                            result.Lista = list;
                        }
                    }
                    else
                    {
                        result.Lista = list;
                    }
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