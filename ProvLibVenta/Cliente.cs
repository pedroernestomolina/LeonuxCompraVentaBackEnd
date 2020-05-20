using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvLibVenta
{

    public partial class Provider: ILibVentas.IProvider 
    {

        public DtoLib.ResultadoLista<DtoLibVenta.Cliente.Resumen> ClienteLista(DtoLibVenta.Cliente.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibVenta.Cliente.Resumen>();

            try
            {
                using (var cnn = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var q = cnn.clientes.ToList();
                    if (filtro.cadena != "")
                    {
                        if (filtro.preferenciaBusqueda == DtoLibVenta.Cliente.Enumerados.enumPreferenciaBusqueda.Codigo)
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
                        if (filtro.preferenciaBusqueda ==  DtoLibVenta.Cliente.Enumerados.enumPreferenciaBusqueda.Nombre)
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                q = q.Where(w => w.razon_social.Contains(cad)).ToList();
                            }
                            else
                            {
                                q = q.Where(w =>
                                {
                                    var r = w.razon_social.Trim().ToUpper();
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
                        if (filtro.preferenciaBusqueda == DtoLibVenta.Cliente.Enumerados.enumPreferenciaBusqueda.CiRif)
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                q = q.Where(w => w.ci_rif.Contains(cad)).ToList();
                            }
                            else
                            {
                                q = q.Where(w =>
                                {
                                    var r = w.ci_rif.Trim().ToUpper();
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

                    var list = new List<DtoLibVenta.Cliente.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            result.Lista = q.Select(s =>
                            {
                                var isActivo = s.estatus.Trim().ToUpper() == "ACTIVO" ? true : false;
                                var r = new DtoLibVenta.Cliente.Resumen()
                                {
                                    Auto = s.auto,
                                    CiRif=s.ci_rif,
                                    Codigo= s.codigo,
                                    Nombre= s.razon_social,
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

        public DtoLib.ResultadoLista<DtoLibVenta.Cliente.PendientePorCobrar> ClienteDocPendientePorCobrar(string auto)
        {
            var result = new DtoLib.ResultadoLista<DtoLibVenta.Cliente.PendientePorCobrar>();

            try
            {
                using (var cnn = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var q = cnn.cxc.Where(w => w.auto_cliente == auto && w.tipo_documento.Trim().ToUpper() != "PAG").ToList();
                    q = q.Where(w => w.estatus_cancelado == "0").ToList();
                    q = q.Where(w => w.estatus_anulado == "0").ToList();

                    var list = new List<DtoLibVenta.Cliente.PendientePorCobrar>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            result.Lista = q.Select(s =>
                            {
                                var isAdministrativo = s.auto_documento == s.auto ? true : false;
                                var tipoDocumento = DtoLibVenta.Cliente.Enumerados.enumTipoDocumentoPorCobrar.SinDefinir;
                                switch (s.tipo_documento.Trim().ToUpper()) 
                                {
                                    case "FAC":
                                        tipoDocumento = DtoLibVenta.Cliente.Enumerados.enumTipoDocumentoPorCobrar.Factura;
                                        break;
                                    case "NCR":
                                        tipoDocumento = DtoLibVenta.Cliente.Enumerados.enumTipoDocumentoPorCobrar.NotaCredito;
                                        break;
                                    case "NDB":
                                        tipoDocumento = DtoLibVenta.Cliente.Enumerados.enumTipoDocumentoPorCobrar.NotaDebito;
                                        break;
                                }
                                var r = new DtoLibVenta.Cliente.PendientePorCobrar()
                                {
                                    Abonado=s.acumulado,
                                    AutoDocumento=s.auto_documento,
                                    Documento=s.documento ,
                                    FechaEmision=s.fecha ,
                                    MontoDocumento=Math.Abs(s.importe),
                                    Signo=s.signo,
                                    TipoDocumento=tipoDocumento,
                                    IsAdministrativo=isAdministrativo,
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

        public DtoLib.ResultadoEntidad<DtoLibVenta.Cliente.DetalleResumen> ClienteDetalleResumen(string auto)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibVenta.Cliente.DetalleResumen>();

            try
            {
                using (var cnn = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var q = cnn.clientes.Find(auto);
                    if (q == null) 
                    {
                        result.Mensaje = "[ ID ] CLIENTE NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var tarifa= DtoLibVenta.Cliente.Enumerados.enumTarifaPrecio.SinDefinir;
                    var isActivo= q.estatus.Trim().ToUpper()=="ACTIVO"?true:false;
                    var categoria= DtoLibVenta.Cliente.Enumerados.enumCategoria.SinDefinir;
                    var isCreditoHabilitado = q.estatus_credito.Trim().ToUpper()=="1"?true:false;
                    var denominacionFiscal = DtoLibVenta.Cliente.Enumerados.enumDenominacionFiscal.SinDefinir;

                    switch(q.categoria.Trim().ToUpper())
                    {
                        case "EVENTUAL":
                            categoria = DtoLibVenta.Cliente.Enumerados.enumCategoria.Eventual;
                            break;
                        case "ADMINISTRATIVO":
                            categoria = DtoLibVenta.Cliente.Enumerados.enumCategoria.Administrativo;
                            break;
                    }

                    switch (q.tarifa.Trim().ToUpper())
                    {
                        case "1":
                            tarifa = DtoLibVenta.Cliente.Enumerados.enumTarifaPrecio.Tarifa_1 ;
                            break;
                        case "2":
                            tarifa = DtoLibVenta.Cliente.Enumerados.enumTarifaPrecio.Tarifa_2 ;
                            break;
                        case "3":
                            tarifa = DtoLibVenta.Cliente.Enumerados.enumTarifaPrecio.Tarifa_3;
                            break;
                        case "4":
                            tarifa = DtoLibVenta.Cliente.Enumerados.enumTarifaPrecio.Tarifa_4;
                            break;
                    }

                    switch (q.denominacion_fiscal.Trim().ToUpper())
                    {
                        case "NO CONTRIBUYENTE":
                            denominacionFiscal = DtoLibVenta.Cliente.Enumerados.enumDenominacionFiscal.NoContribuyente;
                            break;
                        case "CONTRIBUYENTE":
                            denominacionFiscal = DtoLibVenta.Cliente.Enumerados.enumDenominacionFiscal.Contribuyente;
                            break;
                    }

                    var ent = new DtoLibVenta.Cliente.DetalleResumen()
                    {
                        Auto=q.auto,
                        AutoGrupo=q.auto_grupo,
                        AutoCobrador=q.auto_cobrador,
                        AutoVendedor=q.auto_vendedor,
                        AutoEstado=q.auto_estado,
                        AutoZona=q.auto_zona,
                        Codigo=q.codigo,
                        Nombre=q.razon_social,
                        CiRif=q.ci_rif,
                        DireccionFiscal=q.dir_fiscal,
                        Contacto=q.contacto,
                        Tarifa=tarifa,
                        PorctDescuento=q.descuento,
                        PorctRecargo=q.recargo,
                        IsCreditoHabilitado=isCreditoHabilitado,
                        DiasCredito=q.dias_credito,
                        Categoria=categoria,
                        LimitePorDocumento=q.doc_pendientes,
                        LimitePorMonto=q.limite_credito,
                        Notas=q.memo,
                        Aviso=q.aviso,
                        IsActivo= isActivo,
                        Email=q.email,
                        Telefono_1=q.telefono,
                        Telefono_2=q.telefono2,
                        Celular=q.celular,
                        GrupoCodigo=q.clientes_grupo.codigo,
                        GrupoDescripcion=q.clientes_grupo.nombre,
                        ZonaCodigo=q.clientes_zonas.codigo,
                        ZonaDescripcion=q.clientes_zonas.nombre,
                        CobradorCodigo=q.empresa_cobradores.codigo,
                        CobradorNombre=q.empresa_cobradores.nombre,
                        VendedorCodigo=q.vendedores.codigo,
                        VendedorNombre=q.vendedores.nombre,
                        EstadoDescripcion=q.clientes_zonas.nombre,
                        DenominacionFiscal=denominacionFiscal,
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

        public DtoLib.ResultadoAuto ClienteAgregarEventual(DtoLibVenta.Cliente.AgregarEventual ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var ctx = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var r = ctx.Database.ExecuteSqlCommand("update sistema_contadores set a_clientes=a_clientes+1");
                        if (r == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR CONTADOR DE CLIENTE";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var fechaSistema = ctx.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var cntCliente = ctx.Database.SqlQuery<int>("select a_clientes from sistema_contadores").FirstOrDefault();
                        var AutoCliente = cntCliente.ToString().Trim().PadLeft(10, '0');

                        var ent = new LibEntityVentas.clientes()
                        {
                            auto = AutoCliente,
                            auto_grupo = "0000000001",
                            auto_zona = "0000000001",
                            auto_estado = "0000000001",
                            auto_agencia = "0000000001",
                            auto_cobrador = "0000000001",
                            auto_vendedor = "0000000001",
                            auto_codigo_anticipos = "0000000001",
                            auto_codigo_cobrar = "0000000001",
                            auto_codigo_ingresos = "0000000001",

                            ci_rif = ficha.CiRif,
                            razon_social = ficha.NombreRazonSocial,
                            dir_fiscal = ficha.DireccionFiscal,
                            telefono = ficha.Telefono,
                            estatus = "Activo",
                            estatus_credito = "0",
                            categoria = "Eventual",
                            tarifa = " ",
                            dias_credito = 0,
                            limite_credito = 0,
                            doc_pendientes = 0,
                            pais = "VZLA",
                            fecha_alta = fechaSistema.Date,
                            denominacion_fiscal = "No Contribuyente",

                            codigo = "",
                            nombre = "",
                            dir_despacho = "",
                            contacto = "",
                            email = "",
                            website = "",
                            codigo_postal = "",
                            retencion_iva = 0.0m,
                            retencion_islr = 0.0m,
                            descuento = 0.0m,
                            recargo = 0.0m,
                            estatus_morosidad = "0",
                            estatus_lunes = "0",
                            estatus_martes = "0",
                            estatus_miercoles = "0",
                            estatus_jueves = "0",
                            estatus_viernes = "0",
                            estatus_sabado = "0",
                            estatus_domingo = "0",
                            fecha_baja = new DateTime(2000, 01, 01),
                            fecha_ult_pago = new DateTime(2000, 01, 01),
                            fecha_ult_venta = new DateTime(2000, 01, 01),
                            anticipos = 0.0m,
                            debitos = 0.0m,
                            creditos = 0.0m,
                            saldo = 0.0m,
                            disponible = 0.0m,
                            memo = "",
                            aviso = "",
                            cuenta = "",
                            iban = "",
                            swit = "",
                            dir_banco = "",
                            descuento_pronto_pago = 0.0m,
                            importe_ult_pago = 0.0m,
                            importe_ult_venta = 0.0m,
                            telefono2 = "",
                            fax = "",
                            celular = "",
                            abc = " ",
                        };
                        ctx.clientes.Add(ent);
                        ctx.SaveChanges();

                        ts.Complete();
                        result.Auto = AutoCliente;
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
                result.Mensaje = msg ;
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