using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibVenta
{

    public partial class Provider: ILibVentas.IProvider 
    {

        public DtoLib.ResultadoLista<DtoLibVenta.Inventario.Producto.Resumen> ProductoLista(DtoLibVenta.Inventario.Producto.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibVenta.Inventario.Producto.Resumen>();

            try
            {
                using (var cnn = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var q = cnn.productos.ToList();
                    if (filtro.cadena != "") 
                    {
                        if (filtro.preferenciaBusqueda == DtoLibVenta.Inventario.Enumerados.enumPreferenciaBusqueda.Codigo) 
                        {
                            var cad= filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                q = q.Where(w => w.codigo.Contains(cad)).ToList();
                            }
                            else 
                            {
                                q = q.Where(w =>
                                {
                                    var r = w.codigo.Trim().ToUpper();
                                    if (r.Length >= cad.Length && r.Substring(0, cad.Length) == cad)
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
                        if (filtro.preferenciaBusqueda == DtoLibVenta.Inventario.Enumerados.enumPreferenciaBusqueda.Nombre)
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                q = q.Where(w => w.nombre.Contains(cad)).ToList();
                            }
                            else
                            {
                                q = q.Where(w =>
                                {
                                    var r = w.nombre.Trim().ToUpper();
                                    if (r.Length >= cad.Length && r.Substring(0, cad.Length) == cad)
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
                        if (filtro.preferenciaBusqueda == DtoLibVenta.Inventario.Enumerados.enumPreferenciaBusqueda.Referencia)
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                q = q.Where(w => w.referencia.Contains(cad)).ToList();
                            }
                            else
                            {
                                q = q.Where(w =>
                                {
                                    var r = w.referencia.Trim().ToUpper();
                                    if (r.Length >= cad.Length && r.Substring(0, cad.Length) == cad)
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
                    }

                    var list = new List<DtoLibVenta.Inventario.Producto.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            result.Lista = q.Select(s =>
                            {
                                var isActivo = s.estatus.Trim().ToUpper() == "ACTIVO" ? true : false;
                                var r = new DtoLibVenta.Inventario.Producto.Resumen()
                                {
                                    Auto = s.auto,
                                    CodigoPrd=s.codigo,
                                    NombrePrd=s.nombre,
                                    DescripcionPrd=s.nombre, 
                                    ReferenciaPrd=s.referencia,
                                    IsActivo=isActivo,
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

        public DtoLib.ResultadoLista<DtoLibVenta.Inventario.Existencia.Resumen> ProductoExistencia(string auto)
        {
            var result = new DtoLib.ResultadoLista<DtoLibVenta.Inventario.Existencia.Resumen>();

            try
            {
                using (var cnn = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var list = new List<DtoLibVenta.Inventario.Existencia.Resumen>();
                    var q = cnn.productos_deposito.Where(w => w.auto_producto == auto).ToList();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            result.Lista = q.Select(s =>
                            {
                                var codigoDep = s.empresa_depositos.codigo;
                                var descripcionDep = s.empresa_depositos.nombre;
                                var r = new DtoLibVenta.Inventario.Existencia.Resumen()
                                {
                                    autoDeposito=s.auto_deposito,
                                    cntDisponible=s.disponible,
                                    cntFisica=s.fisica,
                                    cntReservada=s.reservada,
                                    CodigoDeposito=codigoDep,
                                    DescripcionDeposito=descripcionDep,
                                    Ubicacion_1=s.ubicacion_1,
                                    Ubicacion_2 = s.ubicacion_2,
                                    Ubicacion_3 = s.ubicacion_3,
                                    Ubicacion_4 = s.ubicacion_4,
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

        public DtoLib.ResultadoLista<DtoLibVenta.Inventario.Precio.Resumen> ProductoPrecios(string auto)
        {
            var result = new DtoLib.ResultadoLista<DtoLibVenta.Inventario.Precio.Resumen>();

            try
            {
                using (var cnn = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var list = new List<DtoLibVenta.Inventario.Precio.Resumen>();
                    var q = cnn.productos.Find(auto);
                    if (q != null)
                    {
                        var p1 = new DtoLibVenta.Inventario.Precio.Resumen()
                        {
                            Id = "1",
                            PrecioNeto = q.precio_1,
                            ContEmpVenta= q.contenido_1,
                            DescEmpVenta= q.productos_medida3.nombre,
                            UtilidadMargen=q.utilidad_1,
                            Decimales=q.productos_medida3.decimales,
                        };
                        var p2 = new DtoLibVenta.Inventario.Precio.Resumen()
                        {
                            Id = "2",
                            PrecioNeto = q.precio_2,
                            ContEmpVenta = q.contenido_2,
                            DescEmpVenta = q.productos_medida4.nombre,
                            UtilidadMargen = q.utilidad_2,
                            Decimales=q.productos_medida4.decimales,
                        };
                        var p3 = new DtoLibVenta.Inventario.Precio.Resumen()
                        {
                            Id = "3",
                            PrecioNeto = q.precio_3,
                            ContEmpVenta = q.contenido_3,
                            DescEmpVenta = q.productos_medida5.nombre,
                            UtilidadMargen = q.utilidad_3,
                            Decimales=q.productos_medida5.decimales,
                        };
                        var p4 = new DtoLibVenta.Inventario.Precio.Resumen()
                        {
                            Id = "4",
                            PrecioNeto = q.precio_4,
                            ContEmpVenta = q.contenido_4,
                            DescEmpVenta = q.productos_medida.nombre,
                            UtilidadMargen = q.utilidad_4,
                            Decimales=q.productos_medida.decimales,
                        };
                        var p5 = new DtoLibVenta.Inventario.Precio.Resumen()
                        {
                            Id = "5",
                            PrecioNeto = q.precio_pto,
                            ContEmpVenta = q.contenido_pto,
                            DescEmpVenta = q.productos_medida1.nombre,
                            UtilidadMargen = q.utilidad_pto,
                            Decimales=q.productos_medida1.decimales,
                        };

                        list.Add(p1);
                        list.Add(p2);
                        list.Add(p3);
                        list.Add(p4);
                        list.Add(p5);

                        result.Lista = list;
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

        public DtoLib.ResultadoEntidad<DtoLibVenta.Inventario.Producto.DetalleResumen> ProductoDetalleResumen(string auto)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibVenta.Inventario.Producto.DetalleResumen>();

            try
            {
                using (var cnn = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var q = cnn.productos.Find(auto);
                    if (q == null) 
                    {
                        result.Mensaje="[ ID ] PRODUCTO NO ENCONTRADO";
                        result.Result= DtoLib.Enumerados.EnumResult.isError;
                        return  result;
                    }

                    var ent = new DtoLibVenta.Inventario.Producto.DetalleResumen()
                    {
                        Auto=q.auto,
                        CodigoPrd=q.codigo,
                        Comentarios=q.comentarios,
                        ContenidoEmpCompra=q.contenido_compras,
                        CostoUnidad=q.costo_und,
                        Departamento=q.empresa_departamentos.nombre,
                        DescripcionPrd="",
                        FechaUltActCosto=q.fecha_ult_costo,
                        FechaUltActPrecio=q.fecha_cambio,
                        FechaUltVenta=q.fecha_ult_venta,
                        Grupo=q.productos_grupo.nombre,
                        IsActivo=q.estatus.Trim().ToUpper()=="ACTIVO"?true:false,
                        IsDivisa=q.estatus_divisa.Trim().ToUpper()=="1"?true:false,
                        IsPesado=q.estatus_pesado.Trim().ToUpper()=="1"?true:false,
                        Marca=q.productos_marca.nombre,
                        Modelo=q.modelo,
                        MontoDivisa=q.divisa,
                        NombreEmpCompra=q.productos_medida2.nombre,
                        NombrePrd=q.nombre,
                        PLU=q.plu,
                        PrecioSugerido=q.precio_sugerido,
                        Referencia=q.referencia,
                        TasaImpuesto=q.tasa,
                    };
                    result.Entidad = ent;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibVenta.Inventario.Producto.Ficha> Producto(string auto)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibVenta.Inventario.Producto.Ficha>();

            try
            {
                using (var cnn = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var q = cnn.productos.Find(auto);
                    if (q == null)
                    {
                        result.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var ent = new DtoLibVenta.Inventario.Producto.Ficha()
                    {
                        Auto = q.auto,
                        AutoDepartamento=q.auto_departamento,
                        AutoGrupo=q.auto_grupo,
                        AutoMarca=q.auto_marca,
                        AutoSubGrupo=q.auto_subgrupo,
                        AutoTasa=q.auto_tasa,
                        Categoria=q.categoria,
                        CodigoDepartamento=q.empresa_departamentos.codigo,
                        CodigoGrupo=q.productos_grupo.codigo,
                        CodigoPrd = q.codigo,
                        Comentarios = q.comentarios,
                        ContenidoEmpCompra = q.contenido_compras,
                        Costo=q.costo,
                        CostoPromedio=q.costo_promedio,
                        CostoPromedioUnidad=q.costo_promedio_und,
                        CostoUnidad = q.costo_und,
                        DiasGarantia= q.dias_garantia,
                        DescripcionPrd = "",
                        FechaUltActCosto = q.fecha_ult_costo,
                        FechaUltActPrecio = q.fecha_cambio,
                        FechaUltVenta = q.fecha_ult_venta,
                        IsActivo = q.estatus.Trim().ToUpper() == "ACTIVO" ? true : false,
                        IsDivisa = q.estatus_divisa.Trim().ToUpper() == "1" ? true : false,
                        IsPesado = q.estatus_pesado.Trim().ToUpper() == "1" ? true : false,
                        IsGarantia = q.estatus_garantia.Trim().ToUpper() == "1" ? true : false,
                        IsSerial = q.estatus_serial.Trim().ToUpper() == "1" ? true : false,
                        Marca = q.productos_marca.nombre,
                        Modelo = q.modelo,
                        MontoDivisa = q.divisa,
                        NombreEmpCompra = q.productos_medida2.nombre,
                        NombrePrd = q.nombre,
                        NombreDepartamento = q.empresa_departamentos.nombre,
                        NombreGrupo = q.productos_grupo.nombre,
                        PLU = q.plu,
                        PrecioSugerido = q.precio_sugerido,
                        Referencia = q.referencia,
                        TasaImpuesto = q.tasa,
                        Decimales=q.productos_medida2.decimales,
                    };
                    result.Entidad = ent;
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