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
                        switch(ent.requiereClave.Trim().ToUpper())
                        {
                            case "":
                                devolucion.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.SinAcceso;
                                break;
                            case "S":
                                devolucion.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.PedirClave;
                                break;
                            case "N":
                                devolucion.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.Libre;
                                break;
                        }
                    }
                    nr.Devolucion = devolucion;

                    ent = cnn.Permiso.FirstOrDefault(f => f.modulo == 2 && f.codigo == "02");
                    if (ent != null)
                    {
                        anularVenta.Codigo = ent.codigo;
                        anularVenta.Descripcion = ent.descripcion;
                        switch (ent.requiereClave.Trim().ToUpper())
                        {
                            case "":
                                anularVenta.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.SinAcceso;
                                break;
                            case "S":
                                anularVenta.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.PedirClave;
                                break;
                            case "N":
                                anularVenta.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.Libre;
                                break;
                        }
                    }
                    nr.AnularVenta = anularVenta;

                    ent = cnn.Permiso.FirstOrDefault(f => f.modulo == 2 && f.codigo == "03");
                    if (ent != null)
                    {
                        sumar.Codigo = ent.codigo;
                        sumar.Descripcion = ent.descripcion;
                        switch (ent.requiereClave.Trim().ToUpper())
                        {
                            case "":
                                sumar.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.SinAcceso;
                                break;
                            case "S":
                                sumar.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.PedirClave;
                                break;
                            case "N":
                                sumar.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.Libre;
                                break;
                        }
                    }
                    nr.Sumar = sumar;

                    ent = cnn.Permiso.FirstOrDefault(f => f.modulo == 2 && f.codigo == "04");
                    if (ent != null)
                    {
                        multiplicar.Codigo = ent.codigo;
                        multiplicar.Descripcion = ent.descripcion;
                        switch (ent.requiereClave.Trim().ToUpper())
                        {
                            case "":
                                multiplicar.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.SinAcceso;
                                break;
                            case "S":
                                multiplicar.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.PedirClave;
                                break;
                            case "N":
                                multiplicar.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.Libre;
                                break;
                        }
                    }
                    nr.Multiplicar = multiplicar;

                    ent = cnn.Permiso.FirstOrDefault(f => f.modulo == 2 && f.codigo == "05");
                    if (ent != null)
                    {
                        restar.Codigo = ent.codigo;
                        restar.Descripcion = ent.descripcion;
                        switch (ent.requiereClave.Trim().ToUpper())
                        {
                            case "":
                                restar.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.SinAcceso;
                                break;
                            case "S":
                                restar.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.PedirClave;
                                break;
                            case "N":
                                restar.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.Libre;
                                break;
                        }
                    }
                    nr.Restar= restar;

                    ent = cnn.Permiso.FirstOrDefault(f => f.modulo == 2 && f.codigo == "06");
                    if (ent != null)
                    {
                        dejarCtaPend.Codigo = ent.codigo;
                        dejarCtaPend.Descripcion = ent.descripcion;
                        switch (ent.requiereClave.Trim().ToUpper())
                        {
                            case "":
                                dejarCtaPend.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.SinAcceso;
                                break;
                            case "S":
                                dejarCtaPend.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.PedirClave;
                                break;
                            case "N":
                                dejarCtaPend.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.Libre;
                                break;
                        }
                    }
                    nr.DejarCtaPendiente = dejarCtaPend;

                    ent = cnn.Permiso.FirstOrDefault(f => f.modulo == 2 && f.codigo == "07");
                    if (ent != null)
                    {
                        anularCtaPend.Codigo = ent.codigo;
                        anularCtaPend.Descripcion = ent.descripcion;
                        switch (ent.requiereClave.Trim().ToUpper())
                        {
                            case "":
                                anularCtaPend.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.SinAcceso;
                                break;
                            case "S":
                                anularCtaPend.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.PedirClave;
                                break;
                            case "N":
                                anularCtaPend.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.Libre;
                                break;
                        }
                    }
                    nr.AnularCtaPendiente= anularCtaPend;

                    ent = cnn.Permiso.FirstOrDefault(f => f.modulo == 2 && f.codigo == "08");
                    if (ent != null)
                    {
                        dsctoGlobal.Codigo = ent.codigo;
                        dsctoGlobal.Descripcion = ent.descripcion;
                        switch (ent.requiereClave.Trim().ToUpper())
                        {
                            case "":
                                dsctoGlobal.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.SinAcceso;
                                break;
                            case "S":
                                dsctoGlobal.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.PedirClave;
                                break;
                            case "N":
                                dsctoGlobal.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.Libre;
                                break;
                        }
                    }
                    nr.DarDesctoGlobal = dsctoGlobal;

                    ent = cnn.Permiso.FirstOrDefault(f => f.modulo == 2 && f.codigo == "09");
                    if (ent != null)
                    {
                        ctaCredito.Codigo = ent.codigo;
                        ctaCredito.Descripcion = ent.descripcion;
                        switch (ent.requiereClave.Trim().ToUpper())
                        {
                            case "":
                                ctaCredito.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.SinAcceso;
                                break;
                            case "S":
                                ctaCredito.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.PedirClave;
                                break;
                            case "N":
                                ctaCredito.RequiereClave = DtoLibPosOffLine.Permiso.Pos.Permiso.EnumAcceso.Libre;
                                break;
                        }
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

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Permiso.AdmDocumento.Ficha> Permiso_AdmDocumento()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Permiso.AdmDocumento.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var anular = new DtoLibPosOffLine.Permiso.permiso();
                    var reimprimir  = new DtoLibPosOffLine.Permiso.permiso();
                    var notacredito  = new DtoLibPosOffLine.Permiso.permiso();
                    var nr = new DtoLibPosOffLine.Permiso.AdmDocumento.Ficha();

                    var ent = cnn.Permiso.FirstOrDefault(f => f.modulo == 3 && f.codigo == "01");
                    if (ent != null)
                    {
                        anular.Codigo = ent.codigo;
                        anular.Descripcion = ent.descripcion;
                        switch (ent.requiereClave.Trim().ToUpper())
                        {
                            case "":
                                anular.RequiereClave = DtoLibPosOffLine.Permiso.permiso.EnumAcceso.SinAcceso;
                                break;
                            case "S":
                                anular.RequiereClave = DtoLibPosOffLine.Permiso.permiso.EnumAcceso.PedirClave;
                                break;
                            case "N":
                                anular.RequiereClave =  DtoLibPosOffLine.Permiso.permiso.EnumAcceso.Libre ;
                                break;
                        }
                    }
                    nr.Anular = anular;

                    ent = cnn.Permiso.FirstOrDefault(f => f.modulo == 3 && f.codigo == "02");
                    if (ent != null)
                    {
                        notacredito.Codigo = ent.codigo;
                        notacredito.Descripcion = ent.descripcion;
                        switch (ent.requiereClave.Trim().ToUpper())
                        {
                            case "":
                                notacredito.RequiereClave = DtoLibPosOffLine.Permiso.permiso.EnumAcceso.SinAcceso;
                                break;
                            case "S":
                                notacredito.RequiereClave = DtoLibPosOffLine.Permiso.permiso.EnumAcceso.PedirClave;
                                break;
                            case "N":
                                notacredito.RequiereClave = DtoLibPosOffLine.Permiso.permiso.EnumAcceso.Libre;
                                break;
                        }
                    }
                    nr.NotaCredito = notacredito;

                    ent = cnn.Permiso.FirstOrDefault(f => f.modulo == 3 && f.codigo == "03");
                    if (ent != null)
                    {
                        reimprimir.Codigo = ent.codigo;
                        reimprimir.Descripcion = ent.descripcion;
                        switch (ent.requiereClave.Trim().ToUpper())
                        {
                            case "":
                                reimprimir.RequiereClave = DtoLibPosOffLine.Permiso.permiso.EnumAcceso.SinAcceso;
                                break;
                            case "S":
                                reimprimir.RequiereClave = DtoLibPosOffLine.Permiso.permiso.EnumAcceso.PedirClave;
                                break;
                            case "N":
                                reimprimir.RequiereClave = DtoLibPosOffLine.Permiso.permiso.EnumAcceso.Libre;
                                break;
                        }
                    }
                    nr.ReImprimir = reimprimir;
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

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Permiso.Actual.Ficha> Permiso_ActualCargar()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Permiso.Actual.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var list = new List<DtoLibPosOffLine.Permiso.Actual.Permiso>();
                    var q = cnn.Permiso.ToList();
                    if (q != null) 
                    {
                        if (q.Count > 0) 
                        {
                            list = q.Select(s =>
                            {
                                var rg = new DtoLibPosOffLine.Permiso.Actual.Permiso()
                                {
                                    CodigoFuncion = s.codigo,
                                    Descripcion = s.descripcion,
                                    Id = (int)s.id,
                                    Modulo =(int) s.modulo,
                                    RequiereClave = s.requiereClave.Trim().ToUpper()=="S"?true:false,
                                };
                                return rg;
                            }).ToList();
                        }
                    }

                    var nr = new DtoLibPosOffLine.Permiso.Actual.Ficha();
                    nr.Permisos = list;
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

        public DtoLib.Resultado Permiso_Actualizar(DtoLibPosOffLine.Permiso.Actualizar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    using (var ts = cnn.Database.BeginTransaction())
                    {
                        foreach (var rg in ficha.Permisos) 
                        {
                            var ent = cnn.Permiso.Find(rg.Id);
                            if (ent == null) 
                            {
                                result.Mensaje = "PERMISO NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            ent.requiereClave = rg.RequiereClave ? "S" : "N";
                            cnn.SaveChanges();
                        }
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