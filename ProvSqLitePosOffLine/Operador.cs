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

                    ent.estatus = ficha.Estatus;
                    ent.fechaCierre = ficha.Fecha;
                    ent.horaCierre = ficha.Hora;
                    ent.estatus = "C";
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

    }

}