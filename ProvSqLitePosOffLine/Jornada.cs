using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqLitePosOffLine
{

    public partial class Provider : IPosOffLine.IProvider
    {


        public DtoLib.ResultadoId Jornada_Abrir(DtoLibPosOffLine.Jornada.Abrir.Ficha ficha)
        {
            var result = new DtoLib.ResultadoId();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {

                    var ent = new LibEntitySqLitePosOffLine.Jornada()
                    {
                        estatus = ficha.Estatus,
                        fechaApertura = ficha.Fecha,
                        horaApertura = ficha.Hora,
                        estatusTransmitida = "",
                        fechaCierre = "",
                        horaCierre = "",
                    };
                    cnn.Jornada.Add(ent);
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

        public DtoLib.Resultado Jornada_Cerrar(DtoLibPosOffLine.Jornada.Cerrar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var ent = cnn.Jornada.Find(ficha.IdJornada);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] JORNADA NO ENCONTRADA";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    if (ent.estatus == "C")
                    {
                        result.Mensaje = "ESTATUS JORNADA CERRADA";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    ent.estatus = ficha.Estatus;
                    ent.fechaCierre = ficha.Fecha;
                    ent.horaCierre=  ficha.Hora;
                    ent.estatus="C";
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

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Jornada.Cargar.Ficha> Jornada_Cargar(int idJornada)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Jornada.Cargar.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var ent = cnn.Jornada.Find(idJornada);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] JORNADA NO ENCONTRADA";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    var estatus = DtoLibPosOffLine.Jornada.Enumerado.EnumEstatus.SinDefinir;
                    var estatusTransmitida = DtoLibPosOffLine.Jornada.Enumerado.EnumEstatusTrasmicion.SinDefinir;
                    DateTime? fechaCierre = null;
                    if (ent.fechaCierre.Trim().ToUpper() != "") 
                    {
                        fechaCierre = DateTime.Parse(ent.fechaCierre);
                    }
                    estatus = ent.estatus == "A" ? DtoLibPosOffLine.Jornada.Enumerado.EnumEstatus.Abierta : DtoLibPosOffLine.Jornada.Enumerado.EnumEstatus.Cerrada;
                    estatusTransmitida = ent.estatusTransmitida == "T" ? DtoLibPosOffLine.Jornada.Enumerado.EnumEstatusTrasmicion.Transmitida : DtoLibPosOffLine.Jornada.Enumerado.EnumEstatusTrasmicion.NoTransmitida;
                    
                    var nr = new DtoLibPosOffLine.Jornada.Cargar.Ficha()
                    {
                        Id=(int)ent.id,
                        Estatus = estatus,
                        EstatusTransmicion =estatusTransmitida,
                        FechaApertura= DateTime.Parse(ent.fechaApertura),
                        HoraApertura=ent.horaApertura,
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

        public DtoLib.ResultadoEntidad<int> Jornada_Activa()
        {
            var result = new DtoLib.ResultadoEntidad<int>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var ent = cnn.Jornada.FirstOrDefault(f=>f.estatus.Trim().ToUpper()=="A");
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

        public DtoLib.ResultadoEntidad<bool> Jornada_Abrir_VerificaIsOk()
        {
            var result = new DtoLib.ResultadoEntidad<bool>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var ent = cnn.Jornada.FirstOrDefault(f => f.estatus.Trim().ToUpper() == "A");
                    if (ent == null) //NO HAY NINGUNA ABIERTA, TODO OK
                    {
                        result.Entidad = true;
                    }
                    else // HAY UNA JORNADA ABIERTA, PROBLEMA
                    {
                        result.Mensaje ="EXISTE UNA JORNADA ABIERTA";
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

    }

}