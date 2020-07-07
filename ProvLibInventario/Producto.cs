﻿using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibInventario
{

    public partial class Provider : ILibInventario.IProvider
    {


        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Resumen> Producto_GetLista(DtoLibInventario.Producto.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Producto.Resumen>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var q = cnn.productos.ToList();

                    if (filtro.autoDepartamento != "") 
                    {
                        q = q.Where(w => w.auto_departamento == filtro.autoDepartamento).ToList();
                    }
                    if (filtro.autoGrupo != "")
                    {
                        q = q.Where(w => w.auto_grupo == filtro.autoGrupo).ToList();
                    }
                    if (filtro.autoTasa != "")
                    {
                        q = q.Where(w => w.auto_tasa == filtro.autoTasa).ToList();
                    }
                    if (filtro.autoDeposito != "")
                    {
                        q = q.Join(cnn.productos_deposito, p => p.auto , d => d.auto_producto, 
                            ( p , d ) => new {p ,d }).Where(w => w.d.auto_deposito == filtro.autoDeposito).Select(s => s.p).ToList();
                    }
                    if (filtro.autoProveedor != "")
                    {
                        q = q.Join(cnn.productos_proveedor, p => p.auto, prv => prv.auto_producto,
                            (p, prv) => new { p, prv }).Where(w => w.prv.auto_proveedor == filtro.autoProveedor).Select(s => s.p).ToList();
                    }


                    if (filtro.estatus != DtoLibInventario.Producto.Enumerados.EnumEstatus.SnDefinir)
                    {
                        if (filtro.estatus == DtoLibInventario.Producto.Enumerados.EnumEstatus.Suspendido)
                        {
                            q = q.Where(w => w.estatus_cambio.Trim().ToUpper() == "1").ToList();
                        }
                        else 
                        {
                            var _f = "ACTIVO";
                            if (filtro.estatus == DtoLibInventario.Producto.Enumerados.EnumEstatus.Inactivo)
                            {
                                _f = "INACTIVO";
                            }
                            q = q.Where(w => w.estatus.Trim().ToUpper() == _f).ToList();
                        }
                    }
                    if (filtro.admPorDivisa != DtoLibInventario.Producto.Enumerados.EnumAdministradorPorDivisa.SnDefinir)
                    {
                        var _f = "1";
                        if (filtro.admPorDivisa == DtoLibInventario.Producto.Enumerados.EnumAdministradorPorDivisa.No)
                        {
                            _f = "0";
                        }
                        q = q.Where(w => w.estatus_divisa == _f ).ToList();
                    }
                    if (filtro.categoria != DtoLibInventario.Producto.Enumerados.EnumCategoria.SnDefinir)
                    {
                        var _f = "";
                        switch (filtro.categoria) 
                        {
                            case DtoLibInventario.Producto.Enumerados.EnumCategoria.BienServicio:
                                _f = "BIEN DE SERVICIO";
                                break;
                            case DtoLibInventario.Producto.Enumerados.EnumCategoria.ProductoTerminado:
                                _f = "PRODUCTO TERMINADO";
                                break;
                            case DtoLibInventario.Producto.Enumerados.EnumCategoria.MateriaPrima:
                                _f = "MATERIA PRIMA ";
                                break;
                            case DtoLibInventario.Producto.Enumerados.EnumCategoria.SubProducto:
                                _f = "SUB PRODUCTO";
                                break;
                            case DtoLibInventario.Producto.Enumerados.EnumCategoria.UsoInterno:
                                _f = "USO INTERNO";
                                break;
                        }
                        q = q.Where(w => w.categoria.Trim().ToUpper() == _f).ToList();
                    }
                    if (filtro.origen != DtoLibInventario.Producto.Enumerados.EnumOrigen.SnDefinir)
                    {
                        var _f = "NACIONAL";
                        if (filtro.origen == DtoLibInventario.Producto.Enumerados.EnumOrigen.Importado) 
                        {
                            _f = "IMPORTADO";
                        }
                        q = q.Where(w => w.origen.Trim().ToUpper() == _f).ToList();
                    }
                    if (filtro.pesado != DtoLibInventario.Producto.Enumerados.EnumPesado.SnDefinir)
                    {
                        var _f = "1";
                        if (filtro.pesado== DtoLibInventario.Producto.Enumerados.EnumPesado.No)
                        {
                            _f = "0";
                        }
                        q = q.Where(w => w.estatus_pesado.Trim().ToUpper() == _f).ToList();
                    }
                    if (filtro.oferta != DtoLibInventario.Producto.Enumerados.EnumOferta.SnDefinir)
                    {
                        var _f = "1";
                        if (filtro.oferta == DtoLibInventario.Producto.Enumerados.EnumOferta.No )
                        {
                            _f = "0";
                        }
                        q = q.Where(w => w.estatus_oferta.Trim().ToUpper() == _f).ToList();
                    }


                    if (filtro.cadena != "")
                    {
                        if (filtro.MetodoBusqueda== DtoLibInventario.Producto.Enumerados.EnumMetodoBusqueda.Codigo)
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
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
                        if (filtro.MetodoBusqueda == DtoLibInventario.Producto.Enumerados.EnumMetodoBusqueda.Nombre)
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
                        if (filtro.MetodoBusqueda == DtoLibInventario.Producto.Enumerados.EnumMetodoBusqueda.Referencia)
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

                    var list = new List<DtoLibInventario.Producto.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var _estatus = s.estatus.Trim().ToUpper() == "ACTIVO" ? 
                                    DtoLibInventario.Producto.Enumerados.EnumEstatus.Activo : 
                                    DtoLibInventario.Producto.Enumerados.EnumEstatus.Inactivo;
                                var _admDivisa = s.estatus_divisa.Trim().ToUpper() == "1" ?
                                    DtoLibInventario.Producto.Enumerados.EnumAdministradorPorDivisa.Si :
                                    DtoLibInventario.Producto.Enumerados.EnumAdministradorPorDivisa.No;
                                var _depart = s.empresa_departamentos.nombre;
                                var _grupo = s.productos_grupo.nombre;
                                var _empaque = s.productos_medida2.nombre;
                                var _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.SnDefinir;
                                switch (s.categoria.Trim().ToUpper()) 
                                {
                                    case "PRODUCTO TERMINADO":
                                        _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.ProductoTerminado;
                                        break;
                                    case "BIEN DE SERVICIO":
                                        _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.BienServicio;
                                        break;
                                    case "MATERIA PRIMA":
                                        _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.MateriaPrima;
                                        break;
                                    case "USO INTERNO":
                                        _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.UsoInterno;
                                        break;
                                    case "SUB PRODUCTO":
                                        _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.SubProducto;
                                        break;
                                }

                                var r = new DtoLibInventario.Producto.Resumen()
                                {
                                    auto = s.auto,
                                    codigo = s.codigo,
                                    contenido = s.contenido_compras,
                                    nombre = s.nombre_corto,
                                    descripcion = s.nombre,
                                    modelo = s.modelo,
                                    referencia = s.referencia,
                                    estatus=_estatus,
                                    AdmPorDivisa=_admDivisa,
                                    departamento=_depart,
                                    grupo=_grupo,
                                    tasaIva=s.tasa,
                                    categoria=_categoria,
                                    empaque=_empaque,
                                };
                                return r;
                            }).ToList();
                        }
                    }
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

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Ficha> Producto_GetFicha(string autoPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var entPrd = cnn.productos.Find(autoPrd);
                    if (entPrd == null)
                    {
                        rt.Mensaje = "PRODUCTO NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    };

                    var f = new DtoLibInventario.Producto.VerData.Ficha();
                    var _depart= entPrd.empresa_departamentos.nombre;
                    var _codDepart=entPrd.empresa_departamentos.codigo;
                    var _grupo= entPrd.productos_grupo.nombre;
                    var _codGrupo= entPrd.productos_grupo.codigo;
                    var _marca = entPrd.productos_marca.nombre;
                    var _nombreTasaIva= entPrd.empresa_tasas.nombre;
                    var _empCompra = entPrd.productos_medida2.nombre;
                    var _decimales = entPrd.productos_medida2.decimales;
                    var _origen = entPrd.origen.Trim().ToUpper() == "NACIONAL" ? 
                        DtoLibInventario.Producto.Enumerados.EnumOrigen.Nacional : 
                        DtoLibInventario.Producto.Enumerados.EnumOrigen.Importado;   
                    var _estatus= entPrd.estatus.Trim().ToUpper() == "ACTIVO" ?  
                        DtoLibInventario.Producto.Enumerados.EnumEstatus.Activo: 
                        DtoLibInventario.Producto.Enumerados.EnumEstatus.Inactivo;
                    if (_estatus== DtoLibInventario.Producto.Enumerados.EnumEstatus.Activo && 
                        entPrd.estatus_cambio.Trim().ToUpper() == "1") 
                    {
                        _estatus = DtoLibInventario.Producto.Enumerados.EnumEstatus.Suspendido;
                    }
                    var _admDivisa = entPrd.estatus_divisa.Trim().ToUpper() == "1" ?
                        DtoLibInventario.Producto.Enumerados.EnumAdministradorPorDivisa.Si :
                        DtoLibInventario.Producto.Enumerados.EnumAdministradorPorDivisa.No;
                    var _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.SnDefinir;
                    switch (entPrd.categoria.Trim().ToUpper())
                    {
                        case "PRODUCTO TERMINADO":
                            _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.ProductoTerminado;
                            break;
                        case "BIEN DE SERVICIO":
                            _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.BienServicio;
                            break;
                        case "MATERIA PRIMA":
                            _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.MateriaPrima;
                            break;
                        case "USO INTERNO":
                            _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.UsoInterno;
                            break;
                        case "SUB PRODUCTO":
                            _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.SubProducto;
                            break;
                    }

                    var id = new DtoLibInventario.Producto.VerData.Identificacion()
                    {
                        AdmPorDivisa = _admDivisa,
                        advertencia = entPrd.advertencia,
                        auto = entPrd.auto,
                        categoria = _categoria,
                        codigo = entPrd.codigo,
                        codigoDepartamento = _codDepart,
                        codigoGrupo = _codGrupo,
                        comentarios = entPrd.comentarios,
                        contenidoCompra = entPrd.contenido_compras,
                        departamento = _depart,
                        descripcion = entPrd.nombre,
                        empaqueCompra = _empCompra,
                        estatus = _estatus,
                        fechaAlta = entPrd.fecha_alta,
                        fechaBaja = entPrd.fecha_baja,
                        fechaUltActualizacion = entPrd.fecha_cambio,
                        grupo = _grupo,
                        marca = _marca,
                        modelo = entPrd.modelo,
                        nombre = entPrd.nombre_corto,
                        nombreTasaIva = _nombreTasaIva,
                        origen = _origen,
                        presentacion = entPrd.presentacion,
                        referencia = entPrd.referencia,
                        tasaIva = entPrd.tasa,
                        tipoABC = entPrd.abc,
                    };
                    f.identidad = id;

                    var costo = new DtoLibInventario.Producto.VerData.Costo()
                    {
                        costo = entPrd.costo,
                        costoDivisa = entPrd.divisa,
                        costoImportacion = entPrd.costo_importacion,
                        costoPromedio = entPrd.costo_promedio,
                        costoProveedor = entPrd.costo_proveedor,
                        costoVario = entPrd.costo_varios,
                        fechaUltCambio = entPrd.fecha_ult_costo,
                    };
                    f.costo = costo;

                    var dep = cnn.productos_deposito.Where(w => w.auto_producto == autoPrd).ToList();
                    var ex = new DtoLibInventario.Producto.VerData.Existencia()
                    {
                        depositos = dep.Select(s=>
                        {
                            var dp = new DtoLibInventario.Producto.VerData.Deposito()
                            { 
                                codigo = s.empresa_depositos.codigo,
                                exDisponible = s.disponible,
                                exFisica = s.fisica,
                                exReserva = s.reservada,
                                nombre = s.empresa_depositos.nombre,
                            };
                            return dp;
                        }).ToList(),
                        decimales=_decimales,
                    };
                    f.existencia=ex;

                    var precio = new DtoLibInventario.Producto.VerData.Precio()
                    {
                        contenido1 = entPrd.contenido_1,
                        contenido2 = entPrd.contenido_2,
                        contenido3 = entPrd.contenido_3,
                        contenido4 = entPrd.contenido_4,
                        contenido5 = entPrd.contenido_pto,
                        finOferta = entPrd.fin,
                        inicioOferta = entPrd.inicio,
                        ofertaActiva = entPrd.estatus_oferta.Trim().ToUpper()=="1"? 
                        DtoLibInventario.Producto.Enumerados.EnumOferta.Si :
                        DtoLibInventario.Producto.Enumerados.EnumOferta.No,
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
                        precioOferta = entPrd.precio_oferta,
                        precioSugerido = entPrd.precio_sugerido,
                        utilidad1 = entPrd.utilidad_1,
                        utilidad2 = entPrd.utilidad_2,
                        utilidad3 = entPrd.utilidad_3,
                        utilidad4 = entPrd.utilidad_4,
                        utilidad5 = entPrd.utilidad_pto,
                    };
                    f.precio = precio;

                    List<string>alternos= cnn.productos_alterno.
                        Where(w=>w.auto_producto==autoPrd).
                        Select(new Func<productos_alterno,String>(s=> 
                        {
                            return s.codigo_alterno;
                        })).ToList();

                    List<DtoLibInventario.Producto.VerData.Proveedor> prv = cnn.productos_proveedor.
                        Where(w=>w.auto_producto==autoPrd).
                        Select(new Func<productos_proveedor,DtoLibInventario.Producto.VerData.Proveedor>(st=> 
                        {
                            var p=new DtoLibInventario.Producto.VerData.Proveedor()
                            {
                                 codigoPrv=st.proveedores.codigo,
                                 nombrePrv=st.proveedores.razon_social,
                                 codigoRefPrd=st.codigo_producto,
                            };
                            return p;
                        })).ToList<DtoLibInventario.Producto.VerData.Proveedor>();

                    var extra = new DtoLibInventario.Producto.VerData.Extra()
                    {
                        codigosAlterno = alternos,
                        diasEmpaque = entPrd.dias_garantia,
                        esPesado = entPrd.estatus_pesado.Trim().ToUpper()=="1"? 
                        DtoLibInventario.Producto.Enumerados.EnumPesado.Si : 
                        DtoLibInventario.Producto.Enumerados.EnumPesado.No,
                        imagen = null,
                        lugar = entPrd.lugar,
                        plu = entPrd.plu,
                        proveedores = prv,
                    };
                    f.extra = extra;

                    rt.Entidad = f;
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