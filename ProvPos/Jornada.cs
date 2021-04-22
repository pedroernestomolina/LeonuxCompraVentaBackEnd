﻿using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvPos
{

    public partial class Provider: IPos.IProvider
    {

        public DtoLib.ResultadoId Jornada_Abrir(DtoLibPos.Pos.Abrir.Ficha ficha)
        {
            var result = new DtoLib.ResultadoId();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var sql = "";

                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var horaSistema = fechaSistema.ToShortTimeString();

                        sql = "update sistema_contadores set a_cierre=a_cierre+1 ";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var aCierre = cnn.Database.SqlQuery<int>("select a_cierre from sistema_contadores").FirstOrDefault();
                        var autoCierre = ficha.codSucursal + ficha.idEquipo;
                        autoCierre+=aCierre.ToString().Trim().PadLeft((10-autoCierre.Length), '0');

                        var op=ficha.operadorAbrir;
                        var pOperador = new p_operador()
                        {
                            auto_usuario = op.idUsuario,
                            estatus = op.estatus,
                            fecha_apertura = fechaSistema.Date,
                            fecha_cierre = null,
                            hora_apertura = horaSistema,
                            hora_cierre = "",
                            id_equipo = op.idEquipo,
                        };
                        cnn.p_operador.Add(pOperador);
                        cnn.SaveChanges();

                        var arq = ficha.arqueoAbrir;
                        const string InsertarPosArqueo = @"INSERT INTO pos_arqueo ("+
                            "auto_cierre, auto_usuario, codigo, usuario, fecha, hora, " +
                            "diferencia, efectivo, cheque, debito, credito, ticket, firma, "+
                            "retiro, otros, devolucion, subtotal, cobranza, " +
                            "total, mefectivo, mcheque, mbanco1, mbanco2, mbanco3, mbanco4, mtarjeta, "+
                            "mticket, mtrans, mfirma, motros, mgastos, mretiro, mretenciones, msubtotal, "+
                            "mtotal, cierre_ftp, cnt_divisa, cnt_divisa_usuario, cntDoc, cntDocFac, cntDocNcr, "+
                            "montoFac, montoNcr)" +
                            "VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, "+
                            "{12}, {13}, {14}, {15},{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, "+
                            "{26}, {27}, {28}, {29},{30}, {31}, {32}, {33}, {34}, {35}, {36}, {37}, {38}, {39}, "+
                            "{40}, {41}, {42})";
                        var arqueo = cnn.Database.ExecuteSqlCommand(InsertarPosArqueo, 
                            autoCierre,arq.idUsuario,arq.codUsuario, arq.nombreUsuario, fechaSistema.Date, horaSistema,
                            arq.diferencia, arq.efectivo, arq.cheque, arq.debito,arq.credito, arq.ticket, arq.firma,
                            arq.retiro, arq.otros, arq.devolucion, arq.subTotal, arq.cobranza,
                            arq.total, arq.mefectivo, arq.mcheque, arq.mbanco1, arq.mbanco2, arq.mbanco3, arq.mbanco4, arq.mtarjeta,
                            arq.mticket, arq.mtrans, arq.mfirma, arq.motros, arq.mgastos, arq.mretiro, arq.mretenciones, arq.msubtotal,
                            arq.mtotal, arq.cierreFtp, arq.cntDivisia, arq.cntDivisaUsuario, arq.cntDoc, arq.cntDocFac, arq.cntDocNCr,
                            arq.montoFac, arq.montoNCr);
                        if (arqueo == 0)
                        {
                            result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO DE ARQUEO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.SaveChanges();

                        var res= ficha.resumenAbrir;
                        var pResumen = new p_resumen()
                        {
                            id_p_operador = pOperador.id,
                            C__auto_pos_arqueo = autoCierre,
                            m_efectivo = res.mEfectivo,
                            cnt_efectivo = res.cntEfectivo,
                            m_divisa = res.mDivisa,
                            cnt_divisa = res.cntDivisa,
                            m_electronico = res.mElectronico,
                            cnt_electronico = res.cntElectronico,
                            m_otros = res.mOtros,
                            cnt_otros = res.cntotros,
                            m_devolucion = res.mDevolucion,
                            cnt_devolucion = res.cntDevolucion,
                            m_contado = res.mContado,
                            m_credito = res.mCredito,
                            cnt_doc = res.cntDoc,
                            cnt_fac = res.cntFac,
                            cnt_ncr = res.cntNCr,
                            m_fac = res.mFac,
                            m_ncr = res.mNCr,
                            cnt_doc_contado = res.cntDocContado,
                            cnt_doc_credito = res.cntDocCredito,
                        };
                        cnn.p_resumen.Add(pResumen);
                        cnn.SaveChanges();
                       
                        ts.Complete();
                        result.Id = pOperador.id;
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

        public DtoLib.Resultado Jornada_Verificar_Abrir(string idEquipo)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {

                    var ent = cnn.p_operador.FirstOrDefault(f => f.id_equipo == idEquipo && f.estatus == "A");
                    if (ent != null) 
                    {
                        result.Mensaje = "EXISTE UNA JORANADA ABIERTA PARA ESTE EQUIPO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
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

        public DtoLib.Resultado Jornada_Cerrar(DtoLibPos.Pos.Cerrar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoId();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var horaSistema = fechaSistema.ToShortTimeString();

                        var pOperador = cnn.p_operador.Find(ficha.idOperador);
                        if (pOperador == null) 
                        {
                            result.Mensaje ="[ ID] OPERADOR NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        pOperador.estatus = ficha.estatus;
                        pOperador.fecha_cierre =fechaSistema.Date;
                        pOperador.hora_cierre = horaSistema;
                        cnn.SaveChanges();

                        var arq = ficha.arqueoCerrar;
                        const string UpdatePosArqueo = @"UPDATE pos_arqueo SET " +
                            "diferencia={0}, efectivo={1}, cheque={2}, debito={3}, credito={4}, ticket={5}, firma={6}, " +
                            "retiro={7}, otros={8}, devolucion={9}, subtotal={10}, cobranza={11}, " +
                            "total={12}, mefectivo={13}, mcheque={14}, mbanco1={15}, mbanco2={16}, mbanco3={17}, mbanco4={18}, mtarjeta={19}, " +
                            "mticket={20}, mtrans={21}, mfirma={22}, motros={23}, mgastos={24}, mretiro={25}, mretenciones={26}, msubtotal={27}, " +
                            "mtotal={28}, cierre_ftp={29}, cnt_divisa={30}, cnt_divisa_usuario={31}, cntDoc={32}, cntDocFac={33}, cntDocNcr={34}, " +
                            "montoFac={35}, montoNcr={36} " +
                            "where auto_cierre={37}";
                        var arqueo = cnn.Database.ExecuteSqlCommand(UpdatePosArqueo,
                            arq.diferencia, arq.efectivo, arq.cheque, arq.debito, arq.credito, arq.ticket, arq.firma,
                            arq.retiro, arq.otros, arq.devolucion, arq.subTotal, arq.cobranza,
                            arq.total, arq.mefectivo, arq.mcheque, arq.mbanco1, arq.mbanco2, arq.mbanco3, arq.mbanco4, arq.mtarjeta,
                            arq.mticket, arq.mtrans, arq.mfirma, arq.motros, arq.mgastos, arq.mretiro, arq.mretenciones, arq.msubtotal,
                            arq.mtotal, arq.cierreFtp, arq.cntDivisia, arq.cntDivisaUsuario, arq.cntDoc, arq.cntDocFac, arq.cntDocNCr,
                            arq.montoFac, arq.montoNCr,arq.autoArqueo);
                        if (arqueo == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR MOVIMIENTO DE ARQUEO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.SaveChanges();

                        ts.Complete();
                        result.Id = pOperador.id;
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

        public DtoLib.Resultado Jornada_Verificar_Cerrer(int idOperador)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.p_operador.Find(idOperador);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] OPERADOR NO EXISTE";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    if (ent.estatus == "C") 
                    {
                        result.Mensaje = "OPERADOR YA CERRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
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

        public DtoLib.ResultadoEntidad<DtoLibPos.Pos.EnUso.Ficha> Jornada_EnUso_GetByIdEquipo(string idEquipo)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Pos.EnUso.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {

                    var nr = new DtoLibPos.Pos.EnUso.Ficha();
                    var ent = cnn.p_operador.FirstOrDefault(f => f.id_equipo == idEquipo && f.estatus == "A");
                    if (ent != null)
                    {
                        var idArqueoCierre = "";
                        var idResumen=-1;

                        var entResumen= cnn.p_resumen.FirstOrDefault(f=>f.id_p_operador==ent.id);
                        if (entResumen!=null)
                        {
                            idArqueoCierre=entResumen.C__auto_pos_arqueo;
                            idResumen=entResumen.id;
                        }

                        var codUsu="";
                        var nomUsu="";
                        var entUsuario= cnn.usuarios.Find(ent.auto_usuario);
                        if (entUsuario!=null)
                        {
                            codUsu=entUsuario.codigo;
                            nomUsu=entUsuario.nombre;
                        }
                        nr.id = ent.id;
                        nr.idUsuario = ent.auto_usuario;
                        nr.fechaApertura = ent.fecha_apertura;
                        nr.horaApertura = ent.hora_apertura;
                        nr.codUsuario=codUsu;
                        nr.nomUsuario=nomUsu;
                        nr.idArqueoCierre = idArqueoCierre;
                        nr.idResumen = idResumen;
                    }
                    result.Entidad = nr;
                    return result;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Pos.EnUso.Ficha> Jornada_EnUso_GetById(int id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Pos.EnUso.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.p_operador.Find(id);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] JORNADA NO ENCONTRADA";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    
                    var idArqueoCierre = "";
                    var idResumen=-1;
                    var entResumen= cnn.p_resumen.FirstOrDefault(f=>f.id_p_operador==ent.id);
                    if (entResumen!=null)
                    {
                        idArqueoCierre=entResumen.C__auto_pos_arqueo;
                        idResumen=entResumen.id;
                    }

                    var codUsu = "";
                    var nomUsu = "";
                    var entUsuario = cnn.usuarios.Find(ent.auto_usuario);
                    if (entUsuario != null)
                    {
                        codUsu = entUsuario.codigo;
                        nomUsu = entUsuario.nombre;
                    }
                    var nr = new DtoLibPos.Pos.EnUso.Ficha()
                    {
                        id = ent.id,
                        idUsuario = ent.auto_usuario,
                        fechaApertura = ent.fecha_apertura,
                        horaApertura = ent.hora_apertura,
                        codUsuario = codUsu,
                        nomUsuario = nomUsu,
                        idArqueoCierre=idArqueoCierre,
                        idResumen=idResumen,
                    };
                    result.Entidad = nr;

                    return result;
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