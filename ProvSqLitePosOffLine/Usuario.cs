using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqLitePosOffLine
{

    public partial class Provider : IPosOffLine.IProvider
    {

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Usuario.Ficha> Usuario_Cargar(DtoLibPosOffLine.Usuario.Cargar ficha)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Usuario.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    using (var ts = cnn.Database.BeginTransaction())
                    {
                        var ent = cnn.UsuarioGrupo.FirstOrDefault(f => f.usuarioCodigo.Trim().ToUpper() == ficha.Codigo &&
                            f.usuarioClave.Trim().ToUpper() == ficha.PassWord);
                        if (ent == null)
                        {
                            result.Entidad = null;
                            result.Mensaje = "USUARIO NO ENCONTRADO, VERIFIQUE POR FAVOR";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        if (ent.usuarioEstatus.Trim().ToUpper() != "A")
                        {
                            result.Entidad = null;
                            result.Mensaje = "USUARIO EN ESTADO INACTIVO, VERIFIQUE POR FAVOR";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var permiso = cnn.UsuarioPermiso.FirstOrDefault(f => f.codigoGrupo == ent.autoGrupo && f.codigoFuncion == "0816000000");
                        if (permiso == null)
                        {
                            result.Entidad = null;
                            result.Mensaje = "PERMISO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        if (permiso.esActivo == 0)
                        {
                            result.Entidad = null;
                            result.Mensaje = "PERMISO NO ASIGNADO PARA ESTE USUARIO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var entPermisos = cnn.UsuarioPermiso.Where(f => f.codigoGrupo == ent.autoGrupo).ToList();
                        foreach (var r in entPermisos)
                        {
                            switch (r.codigoFuncion)
                            {
                                case "0816010000": //DEVOLUCION
                                    var p1 = cnn.Permiso.Find(1);
                                    p1.requiereClave = "";
                                    if (r.esActivo == 1)
                                    {
                                        p1.requiereClave = "N";
                                        if (r.requiereClave == 1)
                                        {
                                            p1.requiereClave = "S";
                                        }
                                    }
                                    cnn.SaveChanges();
                                    break;

                                case "0816020000": // ANULAR VENTA 
                                    var p2 = cnn.Permiso.Find(2);
                                    p2.requiereClave = "";
                                    if (r.esActivo == 1)
                                    {
                                        p2.requiereClave = "N";
                                        if (r.requiereClave == 1)
                                        {
                                            p2.requiereClave = "S";
                                        }
                                    }
                                    cnn.SaveChanges();
                                    break;

                                case "0816030000": // SUMAR 
                                    var p3 = cnn.Permiso.Find(3);
                                    p3.requiereClave = "";
                                    if (r.esActivo == 1)
                                    {
                                        p3.requiereClave = "N";
                                        if (r.requiereClave == 1)
                                        {
                                            p3.requiereClave = "S";
                                        }
                                    }
                                    cnn.SaveChanges();
                                    break;

                                case "0816040000": // MULTIPLICAR
                                    var p4 = cnn.Permiso.Find(4);
                                    p4.requiereClave = "";
                                    if (r.esActivo == 1)
                                    {
                                        p4.requiereClave = "N";
                                        if (r.requiereClave == 1)
                                        {
                                            p4.requiereClave = "S";
                                        }
                                    }
                                    cnn.SaveChanges();
                                    break;

                                case "0816050000": // RESTAR
                                    var p5 = cnn.Permiso.Find(5);
                                    p5.requiereClave = "";
                                    if (r.esActivo == 1)
                                    {
                                        p5.requiereClave = "N";
                                        if (r.requiereClave == 1)
                                        {
                                            p5.requiereClave = "S";
                                        }
                                    }
                                    cnn.SaveChanges();
                                    break;

                                case "0816060000": // CTA PENDIENTE
                                    var p6 = cnn.Permiso.Find(6);
                                    p6.requiereClave = "";
                                    if (r.esActivo == 1)
                                    {
                                        p6.requiereClave = "N";
                                        if (r.requiereClave == 1)
                                        {
                                            p6.requiereClave = "S";
                                        }
                                    }
                                    cnn.SaveChanges();
                                    break;

                                case "0816070000": // ANULAR PENDIENTE
                                    var p7 = cnn.Permiso.Find(7);
                                    p7.requiereClave = "";
                                    if (r.esActivo == 1)
                                    {
                                        p7.requiereClave = "N";
                                        if (r.requiereClave == 1)
                                        {
                                            p7.requiereClave = "S";
                                        }
                                    }
                                    cnn.SaveChanges();
                                    break;

                                case "0816080000": // DSCTO GLOBAL
                                    var p8 = cnn.Permiso.Find(8);
                                    p8.requiereClave = "";
                                    if (r.esActivo == 1)
                                    {
                                        p8.requiereClave = "N";
                                        if (r.requiereClave == 1)
                                        {
                                            p8.requiereClave = "S";
                                        }
                                    }
                                    cnn.SaveChanges();
                                    break;

                                case "0816090000": // CTA CREDITO
                                    var p9 = cnn.Permiso.Find(9);
                                    p9.requiereClave = "";
                                    if (r.esActivo == 1)
                                    {
                                        p9.requiereClave = "N";
                                        if (r.requiereClave == 1)
                                        {
                                            p9.requiereClave = "S";
                                        }
                                    }
                                    cnn.SaveChanges();
                                    break;

                                case "0816100000": // ADM ANULAR DOCUMENTO 
                                    var p10 = cnn.Permiso.Find(10);
                                    p10.requiereClave = "";
                                    if (r.esActivo == 1)
                                    {
                                        p10.requiereClave = "N";
                                        if (r.requiereClave == 1)
                                        {
                                            p10.requiereClave = "S";
                                        }
                                    }
                                    cnn.SaveChanges();
                                    break;

                                case "0816110000": // ADM NOTA DE CREDITO 
                                    var p11 = cnn.Permiso.Find(11);
                                    p11.requiereClave = "";
                                    if (r.esActivo == 1)
                                    {
                                        p11.requiereClave = "N";
                                        if (r.requiereClave == 1)
                                        {
                                            p11.requiereClave = "S";
                                        }
                                    }
                                    cnn.SaveChanges();
                                    break;

                                case "0816120000": // ADM REIMPRIMIR DOCUMENTO 
                                    var p12 = cnn.Permiso.Find(12);
                                    p12.requiereClave = "";
                                    if (r.esActivo == 1)
                                    {
                                        p12.requiereClave = "N";
                                        if (r.requiereClave == 1)
                                        {
                                            p12.requiereClave = "S";
                                        }
                                    }
                                    cnn.SaveChanges();
                                    break;
                            }
                        }

                        var nr = new DtoLibPosOffLine.Usuario.Ficha()
                        {
                            GrupoAuto = ent.autoGrupo,
                            GrupoDescripcion = ent.grupoDescripcion,
                            UsuarioAuto = ent.autoUsuario,
                            UsuarioClave = ent.usuarioClave,
                            UsuarioCodigo = ent.usuarioCodigo,
                            UsuarioDescripcion = ent.usuarioDescripcion,
                        };
                        result.Entidad = nr;

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