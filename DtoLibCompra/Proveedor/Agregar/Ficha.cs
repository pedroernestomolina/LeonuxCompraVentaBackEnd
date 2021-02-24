using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Proveedor.Agregar
{
    
    public class Ficha
    {

        public string idGrupo { get; set; }
        public string idEstado { get; set; }
        public string codigo { get; set; }
        public string razonSocial { get; set; }
        public string ciRif { get; set; }
        public string dirFiscal { get; set; }
        public string contacto { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string webSite { get; set; }
        public string pais { get; set; }
        public string denFiscal { get; set; }
        public string codPostal { get; set; }
        public decimal retIva { get; set; }
        public string estatus { get; set; }
        //
        public decimal saldo { get; set; }
        public decimal anticipos { get; set; }
        public decimal creditos { get; set; }
        public decimal debitos { get; set; }
        public decimal disponible { get; set; }
        public string ctaBanco { get; set; }
        public string benficiarioCtaBanco { get; set; }
        public decimal retISLR { get; set; }
        public string idCtaIngreso { get; set; }
        public string idCtaCobrar { get; set; }
        public string idCtaAnticipos { get; set; }
        public string advertencia { get; set; }
        public string memo { get; set; }
        public string nombre { get; set; }
        public string nj { get; set; }
        public string rif { get; set; }


        public Ficha()
        {
            idGrupo = "";
            idEstado = "";
            codigo = "";
            razonSocial = "";
            ciRif = "";
            dirFiscal = "";
            contacto = "";
            telefono = "";
            email = "";
            webSite = "";
            pais = "";
            codPostal = "";
            retIva = 0.0m;
            //
            anticipos = 0.0m;
            saldo = 0.0m;
            creditos = 0.0m;
            debitos = 0.0m;
            disponible = 0.0m;
            ctaBanco = "";
            benficiarioCtaBanco = "";
            retISLR = 0.0m;
            idCtaCobrar = "";
            idCtaIngreso = "";
            idCtaAnticipos = "";
            advertencia = "";
            memo = "";
            nombre = "";
            nj = "";
            rif = "";
        }

    }

}