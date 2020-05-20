﻿using System;
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

                    var nr = new DtoLibPosOffLine.Configuracion.Serie.Ficha()
                    {
                        ParaFactura=ent.serieFactura,
                        ParaNotaCredito=ent.serieNotaCredito,
                        ParaNotaDebito=ent.serieNotaDebito,
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

                    var transporte = cnn.Cobrador.Find(autoTransporte);
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

    }

}