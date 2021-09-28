using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqLitePosOffLine
{
    
    public partial class Provider : IPosOffLine.IProvider
    {

        public DtoLib.ResultadoId Operador_Abrir(DtoLibPosOffLine.Operador.Abrir.Ficha ficha)
        {
            var result = new DtoLib.ResultadoId();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {

                    var ent = new LibEntitySqLitePosOffLine.Operador()
                    {
                        idJornada = ficha.IdJornada,
                        autoUsuario = ficha.AutoUsuario,
                        codigoUsuario = ficha.CodigoUsuario,
                        usuario = ficha.Usuario,
                        fechaApertura = ficha.Fecha,
                        horaApertura = ficha.Hora,
                        estatus = ficha.Estatus,
                        fechaCierre = "",
                        horaCierre = "",
                        prefijo=ficha.Prefijo,
                    };
                    cnn.Operador.Add(ent);
                    cnn.SaveChanges();
                    result.Id = (int)ent.id;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.Resultado Operador_Cerrar(DtoLibPosOffLine.Operador.Cerrar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    using (var ts = cnn.Database.BeginTransaction())
                    {

                        var ent = cnn.Operador.Find(ficha.IdOperador);
                        if (ent == null)
                        {
                            result.Mensaje = "[ ID ] OPERADOR NO ENCONTRADA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (ent.estatus == "C")
                        {
                            result.Mensaje = "ESTATUS OPERADOR CERRADA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //CIERRE ENCABEZADO
                        ent.estatus = ficha.Estatus;
                        ent.fechaCierre = ficha.Fecha;
                        ent.horaCierre = ficha.Hora;
                        ent.estatus = "C";
                        cnn.SaveChanges();

                        //CIERRE MOVIMIENTOS
                        var entCierre = new LibEntitySqLitePosOffLine.OperadorCierre();
                        entCierre.idOperador = ent.id;
                        entCierre.diferencia = ficha.Movimientos.diferencia;
                        entCierre.efectivo = ficha.Movimientos.efectivo;
                        entCierre.divisa = ficha.Movimientos.divisa;
                        entCierre.tarjeta = ficha.Movimientos.tarjeta;
                        entCierre.otros = ficha.Movimientos.otros;
                        entCierre.firma= ficha.Movimientos.firma;
                        entCierre.devolucion = ficha.Movimientos.devolucion;
                        entCierre.subTotal = ficha.Movimientos.subTotal;
                        entCierre.total = ficha.Movimientos.total;
                        entCierre.mEfectivo = ficha.Movimientos.mEfectivo;
                        entCierre.mDivisa = ficha.Movimientos.mDivisa;
                        entCierre.mTarjeta = ficha.Movimientos.mTarjeta;
                        entCierre.mOtros= ficha.Movimientos.mOtro;
                        entCierre.mFirma= ficha.Movimientos.mFirma;
                        entCierre.mSubTotal = ficha.Movimientos.mSubTotal;
                        entCierre.mTotal= ficha.Movimientos.mTotal;
                        //
                        entCierre.cntDivisa = ficha.Movimientos.cntDivisa;
                        entCierre.cntDivisaUsu = ficha.Movimientos.cntDivisaUsu;
                        entCierre.cntDoc = ficha.Movimientos.cntDoc;
                        entCierre.cntDocFac = ficha.Movimientos.cntDocFac;
                        entCierre.cntDocNcr = ficha.Movimientos.cntDocNcr;
                        entCierre.montoFac = ficha.Movimientos.montoFac;
                        entCierre.montoNcr = ficha.Movimientos.montoNcr;

                        cnn.OperadorCierre.Add(entCierre);
                        cnn.SaveChanges();

                        ts.Commit();
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

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Operador.Cargar.Ficha> Operador_Cargar(int idOperador)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Operador.Cargar.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var ent = cnn.Operador.Find(idOperador);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] OPERADOR NO ENCONTRADA";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    var estatus = DtoLibPosOffLine.Operador.Enumerado.EnumEstatus.SinDefinir;
                    DateTime? fechaCierre = null;
                    if (ent.fechaCierre.Trim().ToUpper() != "")
                    {
                        fechaCierre = DateTime.Parse(ent.fechaCierre);
                    }
                    estatus = ent.estatus == "A" ? DtoLibPosOffLine.Operador.Enumerado.EnumEstatus.Abierta : DtoLibPosOffLine.Operador.Enumerado.EnumEstatus.Cerrada;

                    var nr = new DtoLibPosOffLine.Operador.Cargar.Ficha()
                    {
                        Id = (int)ent.id,
                        IdJornada=(int)ent.idJornada,
                        AutoUsuario=ent.autoUsuario,
                        CodigoUsuario=ent.codigoUsuario,
                        Usuario=ent.usuario,
                        Estatus = estatus,
                        FechaApertura = DateTime.Parse(ent.fechaApertura),
                        HoraApertura = ent.horaApertura,
                        FechaCierre = fechaCierre,
                        HoraCierre = ent.horaCierre,
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

        public DtoLib.ResultadoEntidad<int> Operador_Activa()
        {
            var result = new DtoLib.ResultadoEntidad<int>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var ent = cnn.Operador.FirstOrDefault(f => f.estatus.Trim().ToUpper() == "A");
                    if (ent == null)
                    {
                        result.Entidad = -1;
                        return result;
                    }
                    result.Entidad = (int)ent.id;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<bool> Operador_Abrir_VerificaIsOk()
        {
            var result = new DtoLib.ResultadoEntidad<bool>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var ent = cnn.Operador.FirstOrDefault(f => f.estatus.Trim().ToUpper() == "A");
                    if (ent == null) //NO HAY NINGUN OPERADOR ABIERTO, TODO OK
                    {
                        result.Entidad = true;
                    }
                    else // HAY UN OPERADOR ABIERTO, PROBLEMA
                    {
                        result.Mensaje = "EXISTE UNA OPERADOR ABIERTO";
                        result.Entidad = false;
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

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Operador.Movimiento.Ficha> Operador_Movimientos(int idOperador)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Operador.Movimiento.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var ent = cnn.Operador.Find(idOperador);
                    if (ent == null) 
                    {
                        result.Mensaje = "[ ID ] OPERADOR NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    if (ent.estatus.Trim().ToUpper()=="C")
                    {
                        result.Mensaje = "OPERADOR CON ESTATUS DE CERRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var ficha = new DtoLibPosOffLine.Operador.Movimiento.Ficha();
                    var entGVta = cnn.Venta.
                        Where(w => w.idOperador == idOperador && w.estatusActivo == 1).
                        GroupBy(g => g.tipoDocumento).
                        Select(s=> new {key=s.Key, cnt=s.Count(), monto=s.Sum(sm=>sm.montoTotal)}).
                        ToList();
                    foreach(var tp in entGVta)
                    {
                        switch (tp.key) 
                        {
                            case 1:
                                ficha.cntFactura = tp.cnt;
                                ficha.montoFactura = tp.monto;
                                break;
                            case 2:
                                ficha.cntNDebito = tp.cnt;
                                ficha.montoNDebito = tp.monto;
                                break;
                            case 3:
                                ficha.cntNCredito = tp.cnt;
                                ficha.montoNCredito = tp.monto;
                                break;
                        }
                    }

                    var entGVta2 = cnn.Venta.
                        Where(w => w.idOperador == idOperador && w.estatusActivo == 1 && (w.tipoDocumento==1 || w.tipoDocumento==2)).
                        GroupBy(g => g.esCredito).
                        Select(s => new { key = s.Key, cnt = s.Count(), monto = s.Sum(sm => sm.montoTotal) }).
                        ToList();
                    foreach (var tp in entGVta2)
                    {
                        switch (tp.key)
                        {
                            case "N":
                                ficha.cntDocContado = tp.cnt;
                                ficha.montoDocContado = tp.monto;
                                break;
                            case "S":
                                ficha.cntDocCredito = tp.cnt;
                                ficha.montoDocCredito= tp.monto;
                                break;
                        }
                    }

                    var entGMP = cnn.VentaPago
                        .Join(cnn.Venta, p => p.idVenta, v => v.id, (p, v) => new { p, v }).
                        Where(w => w.v.idOperador == idOperador && w.v.estatusActivo == 1).
                        GroupBy(g => g.p.tipoMedioCobro).
                        Select(s => new { key = s.Key, mp = s.Select(ss=>ss.p).ToList() }).
                        ToList();
                    foreach (var tmp in entGMP) 
                    {
                        switch (tmp.key) 
                        {
                            case 1:
                                ficha.cntEfectivo = tmp.mp.Count();
                                ficha.montoEfectivo = tmp.mp.Sum(s=>s.montoRecibido);
                                break;
                            case 2:
                                ficha.cntDivisa = tmp.mp.Sum(s => s.montoRecibido);
                                ficha.montoDivisa = tmp.mp.Sum(s => s.montoRecibido*s.tasa);
                                break;
                            case 3:
                                ficha.cntElectronico = tmp.mp.Count();
                                ficha.montoElectronico = tmp.mp.Sum(s => s.montoRecibido);
                                break;
                            case 4:
                                ficha.cntOtros = tmp.mp.Count();
                                ficha.montoOtros= tmp.mp.Sum(s => s.montoRecibido);
                                break;
                        }
                    }
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

        public DtoLib.Resultado Operador_Jornada_Cerrar(DtoLibPosOffLine.Operador.Cerrar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    using (var ts = cnn.Database.BeginTransaction())
                    {

                        var ent = cnn.Operador.Find(ficha.IdOperador);
                        if (ent == null)
                        {
                            result.Mensaje = "[ ID ] OPERADOR NO ENCONTRADA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (ent.estatus == "C")
                        {
                            result.Mensaje = "ESTATUS OPERADOR CERRADA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var entJ = cnn.Jornada.Find(ficha.IdJornada);
                        if (entJ == null)
                        {
                            result.Mensaje = "[ ID ] JORNADA NO ENCONTRADA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (entJ.estatus == "C")
                        {
                            result.Mensaje = "ESTATUS JORNADA CERRADA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //CIERRE ENCABEZADO
                        ent.estatus = ficha.Estatus;
                        ent.fechaCierre = ficha.Fecha;
                        ent.horaCierre = ficha.Hora;
                        ent.estatus = "C";
                        cnn.SaveChanges();

                        //CIERRE MOVIMIENTOS
                        var entCierre = new LibEntitySqLitePosOffLine.OperadorCierre();
                        entCierre.idOperador = ent.id;
                        entCierre.diferencia = ficha.Movimientos.diferencia;
                        entCierre.efectivo = ficha.Movimientos.efectivo;
                        entCierre.divisa = ficha.Movimientos.divisa;
                        entCierre.tarjeta = ficha.Movimientos.tarjeta;
                        entCierre.otros = ficha.Movimientos.otros;
                        entCierre.firma = ficha.Movimientos.firma;
                        entCierre.devolucion = ficha.Movimientos.devolucion;
                        entCierre.subTotal = ficha.Movimientos.subTotal;
                        entCierre.total = ficha.Movimientos.total;
                        entCierre.mEfectivo = ficha.Movimientos.mEfectivo;
                        entCierre.mDivisa = ficha.Movimientos.mDivisa;
                        entCierre.mTarjeta = ficha.Movimientos.mTarjeta;
                        entCierre.mOtros = ficha.Movimientos.mOtro;
                        entCierre.mFirma = ficha.Movimientos.mFirma;
                        entCierre.mSubTotal = ficha.Movimientos.mSubTotal;
                        entCierre.mTotal = ficha.Movimientos.mTotal;
                        //
                        entCierre.cntDivisa = ficha.Movimientos.cntDivisa;
                        entCierre.cntDivisaUsu = ficha.Movimientos.cntDivisaUsu;
                        entCierre.cntDoc = ficha.Movimientos.cntDoc;
                        entCierre.cntDocFac = ficha.Movimientos.cntDocFac;
                        entCierre.cntDocNcr = ficha.Movimientos.cntDocNcr;
                        entCierre.montoFac = ficha.Movimientos.montoFac;
                        entCierre.montoNcr = ficha.Movimientos.montoNcr;

                        cnn.OperadorCierre.Add(entCierre);
                        cnn.SaveChanges();

                        //CIERRE JORNADA
                        entJ.estatus = ficha.Estatus;
                        entJ.fechaCierre = ficha.Fecha;
                        entJ.horaCierre = ficha.Hora;
                        entJ.estatus = "C";
                        cnn.SaveChanges();

                        ts.Commit();
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