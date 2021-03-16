using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Producto.Entidad
{

    public class Ficha
    {

        public string Auto { get; set; }
        public string AutoDepartamento { get; set; }
        public string AutoGrupo { get; set; }
        public string AutoSubGrupo { get; set; }
        public string AutoTasaIva { get; set; }
        public string AutoMarca { get; set; }

        public string CodigoPrd { get; set; }
        public string CodigoPLU { get; set; }
        public string NombrePrd { get; set; }
        public string Referencia { get; set; }
        public string Categoria { get; set; }

        public string CodDepartamento { get; set; }
        public string NombreDepartamento { get; set; }

        public string CodGrupo { get; set; }
        public string NombreGrupo { get; set; }

        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Pasillo { get; set; }

        public bool IsActivo { get; set; }
        public bool IsDivisa { get; set; }
        public bool IsPesado { get; set; }
        public bool IsOferta { get; set; }

        public decimal TasaImpuesto { get; set; }
        public DateTime? OfertaDesde { get; set; }
        public DateTime? OfertaHasta { get; set; }
        public decimal OfertaPrecio { get; set; }
        public int DiasEmpaqueGarantia { get; set; }
        public DateTime FechaServidor { get; set; }

        public decimal Costo { get; set; }
        public decimal CostoUnidad { get; set; }
        public decimal CostoPromedio { get; set; }
        public decimal CostoPromedioUnidad { get; set; }

        public Precio Precio { get; set; }

    }

}