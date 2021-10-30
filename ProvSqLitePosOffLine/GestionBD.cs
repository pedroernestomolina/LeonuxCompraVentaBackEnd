using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqLitePosOffLine
{

    public partial class Provider : IPosOffLine.IProvider
    {

        public DtoLib.Resultado Gestion_AgregarCampos()
        {
            var rt = new DtoLib.Resultado();
            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    using (var ts = cnn.Database.BeginTransaction())
                    {

                        var sql1 = "alter table sistema add habilitar_ventaMayor TEXT";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql1);
                        cnn.SaveChanges();

                        var sql2 = @"alter table producto add precioMay_1 NUMERIC";
                        var r2 = cnn.Database.ExecuteSqlCommand(sql2);
                        cnn.SaveChanges();
                        
                        sql2 = @"alter table producto add precioMay_2 NUMERIC";
                        r2 = cnn.Database.ExecuteSqlCommand(sql2);
                        cnn.SaveChanges();

                        sql2 = @"alter table producto add contMay_1 INTEGER";
                        r2 = cnn.Database.ExecuteSqlCommand(sql2);
                        cnn.SaveChanges();

                        sql2 = @"alter table producto add contMay_2 INTEGER";
                        r2 = cnn.Database.ExecuteSqlCommand(sql2);
                        cnn.SaveChanges();

                        sql2 = @"alter table producto add empMay_1 TEXT";
                        r2 = cnn.Database.ExecuteSqlCommand(sql2);
                        cnn.SaveChanges();

                        sql2 = @"alter table producto add empMay_2 TEXT";
                        r2 = cnn.Database.ExecuteSqlCommand(sql2);
                        cnn.SaveChanges();

                        sql2 = @"alter table producto add decMay_1 TEXT";
                        r2 = cnn.Database.ExecuteSqlCommand(sql2);
                        cnn.SaveChanges();

                        sql2 = @"alter table producto add decMay_2 TEXT";
                        r2 = cnn.Database.ExecuteSqlCommand(sql2);
                        cnn.SaveChanges();

                        ts.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

    }

}