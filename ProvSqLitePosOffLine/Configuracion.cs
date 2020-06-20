using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqLitePosOffLine
{

    public partial class Provider : IPosOffLine.IProvider
    {

        public DtoLib.ResultadoEntidad<decimal> Configuracion_FactorCambio()
        {
            var result = new DtoLib.ResultadoEntidad<decimal>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {

                    var ent = cnn.Sistema.Find("0000000001");
                    if (ent == null)
                    {
                        result.Mensaje = "ENTIDAD CONFIGURACION DEL POS NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };

                    result.Entidad = ent.factorCambio;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.ModoPos.Ficha> Configuracion_ModoPos()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.ModoPos.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {

                    var ent = cnn.Sistema.Find("0000000001");
                    if (ent == null)
                    {
                        result.Mensaje = "ENTIDAD CONFIGURACION DEL POS NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };

                    var modo= DtoLibPosOffLine.Configuracion.Enumerados.EnumModoPos.Detal;
                    switch (ent.modoOperacion.Trim().ToUpper())
                    {
                        case "D":
                            modo = DtoLibPosOffLine.Configuracion.Enumerados.EnumModoPos.Detal;
                            break;
                        case "M":
                            modo = DtoLibPosOffLine.Configuracion.Enumerados.EnumModoPos.Mayor;
                            break;
                    }
                    var nr = new DtoLibPosOffLine.Configuracion.ModoPos.Ficha() 
                    {
                        Modo=modo,
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

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Repesaje.Ficha> Configuracion_Repesaje()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Repesaje.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {

                    var ent = cnn.Sistema.Find("0000000001");
                    if (ent == null)
                    {
                        result.Mensaje = "ENTIDAD CONFIGURACION DEL POS NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };

                    var nr = new DtoLibPosOffLine.Configuracion.Repesaje.Ficha()
                    {
                         IsActivo=ent.activarRepesaje.Trim().ToUpper()=="S"?true:false,
                         LimiteValidoInferior=ent.limiteRepesajeInferior,
                         LimiteValidoSuperior=ent.limiteRepesajeSuperior,
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

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Serie.Ficha> Configuracion_Serie()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Serie.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {

                    var ent = cnn.Sistema.Find("0000000001");
                    if (ent == null)
                    {
                        result.Mensaje = "ENTIDAD CONFIGURACION DEL POS NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };

                    var factura = "";
                    var controlFactura = "";
                    if (ent.serieFactura != "") 
                    {
                        var entSerieFactura = cnn.Serie.Find(ent.serieFactura);
                        if (entSerieFactura != null) 
                        {
                            factura = entSerieFactura.serie1;
                            controlFactura = entSerieFactura.control;
                        }
                    }

                    var ntCredito= "";
                    var controlNtCredito = "";
                    if (ent.serieNotaCredito != "")
                    {
                        var entSerieNotaCredito= cnn.Serie.Find(ent.serieNotaCredito);
                        if (entSerieNotaCredito != null)
                        {
                            ntCredito = entSerieNotaCredito.serie1;
                            controlNtCredito = entSerieNotaCredito.control;
                        }
                    }

                    var ntDebito= "";
                    var controlNtdebito = "";
                    if (ent.serieNotaDebito != "")
                    {
                        var entSerieNotaDebito= cnn.Serie.Find(ent.serieNotaDebito);
                        if (entSerieNotaDebito != null)
                        {
                            ntDebito = entSerieNotaDebito.serie1;
                            controlNtdebito = entSerieNotaDebito.control;
                        }
                    }

                    var ntEntrega= "";
                    var controlNtEntrega = "";
                    if (ent.serieNotaEntrega != "")
                    {
                        var entSerieNotaEntrega = cnn.Serie.Find(ent.serieNotaEntrega);
                        if (entSerieNotaEntrega != null)
                        {
                            ntEntrega= entSerieNotaEntrega.serie1;
                            controlNtEntrega =entSerieNotaEntrega.control;
                        }
                    }

                    var nr = new DtoLibPosOffLine.Configuracion.Serie.Ficha()
                    {
                        ParaFactura = factura,
                        ParaNotaCredito = ntCredito,
                        ParaNotaDebito = ntDebito,
                        ParaNotaEntrega = ntEntrega,
                        ControlParaFactura = controlFactura,
                        ControlParaNotaCredito = controlNtCredito,
                        ControlParaNotaDebito = controlNtdebito,
                        ControlParaNotaEntrega = controlNtEntrega,
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

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Sucursal.Ficha> Configuracion_Sucursal()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Sucursal.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {

                    var ent = cnn.Sistema.Find("0000000001");
                    if (ent == null)
                    {
                        result.Mensaje = "ENTIDAD CONFIGURACION DEL POS NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };

                    var nr = new DtoLibPosOffLine.Configuracion.Sucursal.Ficha() 
                    {
                        Codigo= ent.sucursalCodigo,
                        EquipoNumero=ent.equipoNumero,
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

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Vendedor.Ficha> Configuracion_Vendedor()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Vendedor.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var ent = cnn.Sistema.Find("0000000001");
                    if (ent == null)
                    {
                        result.Mensaje = "ENTIDAD CONFIGURACION DEL POS NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };
                    var autoVnd = ent.autoVendedor;
                    if (autoVnd == "")
                    {
                        result.Mensaje = "VENDEDOR NO DEFINIDO EN [ CONFIGURACION ]";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var vnd = cnn.Vendedor.Find(autoVnd);
                    if (vnd== null)
                    {
                        result.Mensaje = "VENDEDOR NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    var nr = new DtoLibPosOffLine.Configuracion.Vendedor.Ficha()
                    {
                        Auto = vnd.auto,
                        Codigo = vnd.codigo,
                        Nombre = vnd.nombre,
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

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Deposito.Ficha> Configuracion_Deposito()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Deposito.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var ent = cnn.Sistema.Find("0000000001");
                    if (ent == null)
                    {
                        result.Mensaje = "ENTIDAD CONFIGURACION DEL POS NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };
                    var autoDep = ent.autoDeposito;
                    if (autoDep == "") 
                    {
                        result.Mensaje = "DEPOSITO NO DEFINIDO EN [ CONFIGURACION ]";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var dep = cnn.Deposito.Find(autoDep);
                    if (dep == null) 
                    {
                        result.Mensaje = "DEPOSITO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    var nr = new DtoLibPosOffLine.Configuracion.Deposito.Ficha()
                    {
                        Auto = dep.auto,
                        Codigo = dep.codigo,
                        Descripcion = dep.nombre,
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

        public DtoLib.ResultadoEntidad<bool> Configuracion_ActivarBusquedaPorDescripcion()
        {
            var result = new DtoLib.ResultadoEntidad<bool>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {

                    var ent = cnn.Sistema.Find("0000000001");
                    if (ent == null)
                    {
                        result.Mensaje = "ENTIDAD CONFIGURACION DEL POS NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };
                    result.Entidad = false;
                    if (ent.activarBusquedaPorDescripcion.Trim() == "S")
                    {
                        result.Entidad = true;
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

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Cobrador.Ficha> Configuracion_Cobrador()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Cobrador.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var ent = cnn.Sistema.Find("0000000001");
                    if (ent == null)
                    {
                        result.Mensaje = "ENTIDAD CONFIGURACION DEL POS NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };
                    var autoCob = ent.autoCobrador;
                    if (autoCob == "")
                    {
                        result.Mensaje = "COBRADOR NO DEFINIDO EN [ CONFIGURACION ]";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var cob = cnn.Cobrador.Find(autoCob);
                    if (cob == null)
                    {
                        result.Mensaje = "COBRADOR NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    var nr = new DtoLibPosOffLine.Configuracion.Cobrador.Ficha()
                    {
                        Auto = cob.auto,
                        Codigo = cob.codigo,
                        Nombre = cob.nombre,
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

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Transporte.Ficha> Configuracion_Transporte()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Transporte.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var ent = cnn.Sistema.Find("0000000001");
                    if (ent == null)
                    {
                        result.Mensaje = "ENTIDAD CONFIGURACION DEL POS NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };
                    var autoTransporte = ent.autoTransporte;
                    if (autoTransporte == "")
                    {
                        result.Mensaje = "TRANSPORTE NO DEFINIDO EN [ CONFIGURACION ]";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var transporte = cnn.Transporte.Find(autoTransporte);
                    if (transporte == null)
                    {
                        result.Mensaje = "TRANSPORTE NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    var nr = new DtoLibPosOffLine.Configuracion.Transporte.Ficha()
                    {
                        Auto = transporte.auto,
                        Codigo = transporte.codigo,
                        Nombre = transporte.nombre,
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

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.MedioCobro.Ficha> Configuracion_MedioCobro()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.MedioCobro.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var ent = cnn.Sistema.Find("0000000001");
                    if (ent == null)
                    {
                        result.Mensaje = "ENTIDAD CONFIGURACION DEL POS NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };

                    var autoMedioEfectivo = ent.autoMedioPagoEfectivo;
                    if (autoMedioEfectivo == "")
                    {
                        result.Mensaje = "MEDIO PAGO EFECTIVO NO DEFINIDO EN [ CONFIGURACION ]";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var autoMedioDivisa = ent.autoMedioPagoDivisa;
                    if (autoMedioDivisa == "")
                    {
                        result.Mensaje = "MEDIO PAGO DIVISA NO DEFINIDO EN [ CONFIGURACION ]";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var autoMedioElectronico = ent.autoMedioPagoElectronico;
                    if (autoMedioElectronico == "")
                    {
                        result.Mensaje = "MEDIO PAGO ELECTRONICO NO DEFINIDO EN [ CONFIGURACION ]";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var autoMedioOtro = ent.autoMedioPagoOtro;
                    if (autoMedioOtro == "")
                    {
                        result.Mensaje = "MEDIO PAGO OTRO NO DEFINIDO EN [ CONFIGURACION ]";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var efectivo= cnn.MedioCobro.Find(autoMedioEfectivo);
                    if (efectivo == null)
                    {
                        result.Mensaje = "MEDIO PAGO EFECTIVO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var divisa = cnn.MedioCobro.Find(autoMedioDivisa);
                    if (divisa == null)
                    {
                        result.Mensaje = "MEDIO PAGO DIVISA NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var electronico = cnn.MedioCobro.Find(autoMedioElectronico);
                    if (electronico == null)
                    {
                        result.Mensaje = "MEDIO PAGO ELECTRONICO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var otro = cnn.MedioCobro.Find(autoMedioOtro);
                    if (otro == null)
                    {
                        result.Mensaje = "MEDIO PAGO OTRO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var nr = new DtoLibPosOffLine.Configuracion.MedioCobro.Ficha()
                    {
                        Efectivo = new DtoLibPosOffLine.Configuracion.MedioCobro.Medio() { Auto = efectivo.auto, Codigo = efectivo.codigo, Descripcion = efectivo.descripcion },
                        Divisa = new DtoLibPosOffLine.Configuracion.MedioCobro.Medio() { Auto = divisa.auto, Codigo = divisa.codigo, Descripcion = divisa.descripcion },
                        Electronico = new DtoLibPosOffLine.Configuracion.MedioCobro.Medio() { Auto = electronico.auto, Codigo = electronico.codigo, Descripcion = electronico.descripcion },
                        Otro = new DtoLibPosOffLine.Configuracion.MedioCobro.Medio() { Auto = otro.auto, Codigo = otro.codigo, Descripcion = otro.descripcion },
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

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.ClaveAcceso.Ficha> Configuracion_ClavePos()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.ClaveAcceso.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var ent = cnn.Sistema.Find("0000000001");
                    if (ent == null)
                    {
                        result.Mensaje = "ENTIDAD CONFIGURACION DEL POS NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };

                    var clave = "";
                    switch (ent.clavePos) 
                    {
                        case 1:
                            clave = ent.clave1;
                            break;
                        case 2:
                            clave = ent.clave2;
                            break;
                        case 3:
                            clave = ent.clave3;
                            break;
                    }

                    var nr = new DtoLibPosOffLine.Configuracion.ClaveAcceso.Ficha();
                    nr.Clave = clave;
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

        public DtoLib.Resultado Configuracion_Actualizar(DtoLibPosOffLine.Configuracion.Guardar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var ent = cnn.Sistema.Find("0000000001");
                    if (ent == null)
                    {
                        result.Mensaje = "ENTIDAD CONFIGURACION DEL POS NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };

                    ent.activarRepesaje = ficha.ActivarRepesaje;
                    ent.limiteRepesajeInferior = ficha.LimiteInferiorRepesaje;
                    ent.limiteRepesajeSuperior = ficha.LimiteSuperiorRepesaje;
                    ent.activarBusquedaPorDescripcion = ficha.ActivarBusquedaPorDescripcion;
                    ent.clavePos = ficha.ClavePos;
                    ent.equipoNumero = ficha.EquipoNumero;

                    ent.serieFactura = ficha.SerieFactura;
                    ent.serieNotaCredito = ficha.SerieNotaCredito;
                    ent.serieNotaDebito= ficha.SerieNotaDebito;
                    ent.serieNotaEntrega= ficha.SerieNotaEntrega;

                    ent.autoCobrador = ficha.AutoCobrador;
                    ent.autoVendedor = ficha.AutoVendedor;
                    ent.autoTransporte = ficha.AutoTransporte;

                    ent.autoMedioPagoEfectivo = ficha.AutoMedioEfectivo;
                    ent.autoMedioPagoDivisa = ficha.AutoMedioDivisa;
                    ent.autoMedioPagoElectronico= ficha.AutoMedioElectronico;
                    ent.autoMedioPagoOtro = ficha.AutoMedioOtro;

                    ent.autoConceptoMovVenta = ficha.AutoMovConceptoVenta;
                    ent.autoConceptoMovDevVenta = ficha.AutoMovConceptoDevVenta;
                    ent.autoConceptoMovSalida = ficha.AutoMovConceptoSalida;

                    cnn.SaveChanges();
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Actual.Ficha> Configuracion_ActualCargar()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Actual.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var ent = cnn.Sistema.Find("0000000001");
                    if (ent == null)
                    {
                        result.Mensaje = "ENTIDAD CONFIGURACION DEL POS NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };

                    var ficha = new DtoLibPosOffLine.Configuracion.Actual.Ficha();
                    ficha.CodigoSucursal =ent.sucursalCodigo ;
                    ficha.EquipoNumero = ent.equipoNumero;

                    ficha.ActivarRepesaje= ent.activarRepesaje.Trim().ToUpper()=="S"?true:false;
                    ficha.LimiteInferiorRepesaje=ent.limiteRepesajeInferior;
                    ficha.LimiteSuperiorRepesaje=ent.limiteRepesajeSuperior;
                    ficha.ActivarBusquedaPorDescripcion=ent.activarBusquedaPorDescripcion.Trim().ToUpper()=="S"?true:false;
                    ficha.ClavePos=(int)ent.clavePos;
                    ficha.TarifaPrecio = ent.tarifaAsignada;
                    ficha.EtiquetarPrecioPorTipoNegocio = ent.EtiquetarPrecioPorTipoNegocio.Trim().ToUpper() == "S" ? true : false;

                    ficha.SerieFactura=ent.serieFactura;
                    ficha.SerieNotaCredito= ent.serieNotaCredito;
                    ficha.SerieNotaDebito= ent.serieNotaDebito;
                    ficha.SerieNotaEntrega = ent.serieNotaEntrega;

                    ficha.AutoDeposito = ent.autoDeposito;
                    ficha.AutoCobrador=ent.autoCobrador;
                    ficha.AutoVendedor=ent.autoVendedor;
                    ficha.AutoTransporte= ent.autoTransporte;

                    ficha.AutoMedioEfectivo= ent.autoMedioPagoEfectivo;
                    ficha.AutoMedioDivisa= ent.autoMedioPagoDivisa;
                    ficha.AutoMedioElectronico= ent.autoMedioPagoElectronico;
                    ficha.AutoMedioOtro= ent.autoMedioPagoOtro;

                    ficha.AutoMovConceptoVenta  = ent.autoConceptoMovVenta ;
                    ficha.AutoMovConceptoDevVenta = ent.autoConceptoMovDevVenta;
                    ficha.AutoMovConceptoSalida = ent.autoConceptoMovSalida;

                    result.Entidad = ficha;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<string> Configuracion_TarifaPrecio()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var ent = cnn.Sistema.Find("0000000001");
                    if (ent == null)
                    {
                        result.Mensaje = "ENTIDAD CONFIGURACION DEL POS NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };
                    result.Entidad = ent.tarifaAsignada;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<bool> Configuracion_EtiquetarPrecioPorTipoNegocio()
        {
            var result = new DtoLib.ResultadoEntidad<bool>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var ent = cnn.Sistema.Find("0000000001");
                    if (ent == null)
                    {
                        result.Mensaje = "ENTIDAD CONFIGURACION DEL POS NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };
                    result.Entidad = ent.EtiquetarPrecioPorTipoNegocio.Trim().ToUpper()=="S"?true:false;
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