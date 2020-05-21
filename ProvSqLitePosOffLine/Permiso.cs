using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqLitePosOffLine
{
    
    public partial class Provider : IPosOffLine.IProvider
    {

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Permiso.Pos.Ficha> Permiso_ManejoPos()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Permiso.Pos.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var devolucion = new DtoLibPosOffLine.Permiso.Pos.Permiso(); 
                    var anularVenta = new DtoLibPosOffLine.Permiso.Pos.Permiso();
                    var sumar = new DtoLibPosOffLine.Permiso.Pos.Permiso();
                    var restar = new DtoLibPosOffLine.Permiso.Pos.Permiso();
                    var multiplicar = new DtoLibPosOffLine.Permiso.Pos.Permiso();
                    var dejarCtaPend = new DtoLibPosOffLine.Permiso.Pos.Permiso();
                    var anularCtaPend = new DtoLibPosOffLine.Permiso.Pos.Permiso();
                    var dsctoGlobal = new DtoLibPosOffLine.Permiso.Pos.Permiso();
                    var ctaCredito = new DtoLibPosOffLine.Permiso.Pos.Permiso();
                    var nr= new DtoLibPosOffLine.Permiso.Pos.Ficha();

                    var ent = cnn.Permiso.FirstOrDefault(f=>f.modulo==2 && f.codigo=="01");
                    if (ent != null)
                    {
                        devolucion.Codigo = ent.codigo;
                        devolucion.Descripcion=ent.descripcion;
                        devolucion.RequiereClave=ent.requiereClave.Trim().ToUpper()=="S"?true:false;
                    }
                    nr.Devolucion = devolucion;

                    ent = cnn.Permiso.FirstOrDefault(f => f.modulo == 2 && f.codigo == "02");
                    if (ent != null)
                    {
                        anularVenta.Codigo = ent.codigo;
                        anularVenta.Descripcion = ent.descripcion;
                        anularVenta.RequiereClave = ent.requiereClave.Trim().ToUpper() == "S" ? true : false;
                    }
                    nr.AnularVenta = anularVenta;

                    ent = cnn.Permiso.FirstOrDefault(f => f.modulo == 2 && f.codigo == "03");
                    if (ent != null)
                    {
                        sumar.Codigo = ent.codigo;
                        sumar.Descripcion = ent.descripcion;
                        sumar.RequiereClave = ent.requiereClave.Trim().ToUpper() == "S" ? true : false;
                    }
                    nr.Sumar = sumar;

                    ent = cnn.Permiso.FirstOrDefault(f => f.modulo == 2 && f.codigo == "04");
                    if (ent != null)
                    {
                        multiplicar.Codigo = ent.codigo;
                        multiplicar.Descripcion = ent.descripcion;
                        multiplicar.RequiereClave = ent.requiereClave.Trim().ToUpper() == "S" ? true : false;
                    }
                    nr.Multiplicar = multiplicar;

                    ent = cnn.Permiso.FirstOrDefault(f => f.modulo == 2 && f.codigo == "05");
                    if (ent != null)
                    {
                        restar.Codigo = ent.codigo;
                        restar.Descripcion = ent.descripcion;
                        restar.RequiereClave = ent.requiereClave.Trim().ToUpper() == "S" ? true : false;
                    }
                    nr.Restar= restar;

                    ent = cnn.Permiso.FirstOrDefault(f => f.modulo == 2 && f.codigo == "06");
                    if (ent != null)
                    {
                        dejarCtaPend.Codigo = ent.codigo;
                        dejarCtaPend.Descripcion = ent.descripcion;
                        dejarCtaPend.RequiereClave = ent.requiereClave.Trim().ToUpper() == "S" ? true : false;
                    }
                    nr.DejarCtaPendiente = dejarCtaPend;

                    ent = cnn.Permiso.FirstOrDefault(f => f.modulo == 2 && f.codigo == "07");
                    if (ent != null)
                    {
                        anularCtaPend.Codigo = ent.codigo;
                        anularCtaPend.Descripcion = ent.descripcion;
                        anularCtaPend.RequiereClave = ent.requiereClave.Trim().ToUpper() == "S" ? true : false;
                    }
                    nr.AnularCtaPendiente= anularCtaPend;

                    ent = cnn.Permiso.FirstOrDefault(f => f.modulo == 2 && f.codigo == "08");
                    if (ent != null)
                    {
                        dsctoGlobal.Codigo = ent.codigo;
                        dsctoGlobal.Descripcion = ent.descripcion;
                        dsctoGlobal.RequiereClave = ent.requiereClave.Trim().ToUpper() == "S" ? true : false;
                    }
                    nr.DarDesctoGlobal = dsctoGlobal;

                    ent = cnn.Permiso.FirstOrDefault(f => f.modulo == 2 && f.codigo == "09");
                    if (ent != null)
                    {
                        ctaCredito.Codigo = ent.codigo;
                        ctaCredito.Descripcion = ent.descripcion;
                        ctaCredito.RequiereClave = ent.requiereClave.Trim().ToUpper() == "S" ? true : false;
                    }
                    nr.CtaCredito= ctaCredito;
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