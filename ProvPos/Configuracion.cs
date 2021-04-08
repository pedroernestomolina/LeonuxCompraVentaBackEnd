using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvPos
{
    
    public partial class Provider: IPos.IProvider
    {

        public DtoLib.ResultadoEntidad<decimal> Configuracion_FactorDivisa()
        {
            var result = new DtoLib.ResultadoEntidad<decimal>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL12");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION GLOBAL NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var m1 = 0.0m;
                    var cnf = ent.usuario;
                    if (cnf.Trim() != "")
                    {
                        var style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
                        //var culture = CultureInfo.CreateSpecificCulture("es-ES");
                        var culture = CultureInfo.CreateSpecificCulture("en-EN");
                        Decimal.TryParse(cnf, style, culture, out m1);
                    }
                    result.Entidad = m1;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.Resultado Configuracion_Pos_Actualizar(DtoLibPos.Configuracion.Actualizar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.p_configuracion.Find(1);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    ent.idClaveUsar = ficha.idClaveUsar;
                    ent.idConceptoDevVenta = ficha.idConceptoDevVenta;
                    ent.idConceptoSalida = ficha.idConceptoSalida;
                    ent.idConceptoVenta = ficha.idConceptoVenta;
                    ent.idMedioPagoDivisa = ficha.idMedioPagoDivisa;
                    ent.idMedioPagoEfectivo = ficha.idMedioPagoEfectivo;
                    ent.idMedioPagoOtros = ficha.idMedioPagoOtros;
                    ent.idMedioPagoElectronico = ficha.idMedioPagoElectronico;
                    ent.idSucursal = ficha.idSucursal;
                    ent.idDeposito = ficha.idDeposito;
                    ent.idCobrador = ficha.idCobrador;
                    ent.idTransporte = ficha.idTransporte;
                    ent.idVendedor = ficha.idVendedor;
                    ent.idClaveUsar = ficha.idClaveUsar;
                    ent.idPrecioManejar = ficha.idPrecioManejar;
                    ent.validarExistencia = ficha.validarExistencia;
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

        public DtoLib.ResultadoEntidad<DtoLibPos.Configuracion.Entidad.Ficha> Configuracion_Pos_GetFicha()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Configuracion.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.p_configuracion.Find(1);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var nr = new DtoLibPos.Configuracion.Entidad.Ficha();
                    nr.idClaveUsar = ent.idClaveUsar;
                    nr.idConceptoDevVenta = ent.idConceptoDevVenta;
                    nr.idConceptoSalida = ent.idConceptoSalida;
                    nr.idConceptoVenta = ent.idConceptoVenta;
                    nr.idMedioPagoDivisa = ent.idMedioPagoDivisa;
                    nr.idMedioPagoEfectivo = ent.idMedioPagoEfectivo;
                    nr.idMedioPagoOtros = ent.idMedioPagoOtros;
                    nr.idMedioPagoElectronico = ent.idMedioPagoElectronico;
                    nr.idSucursal = ent.idSucursal;
                    nr.idDeposito = ent.idDeposito;
                    nr.idCobrador = ent.idCobrador;
                    nr.idTransporte = ent.idTransporte;
                    nr.idVendedor = ent.idVendedor;
                    nr.idClaveUsar = ent.idClaveUsar;
                    nr.idPrecioManejar = ent.idPrecioManejar;
                    nr.validarExistencia = ent.validarExistencia;
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