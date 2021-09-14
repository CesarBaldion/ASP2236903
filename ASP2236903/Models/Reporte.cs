using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP2236903.Models
{
    public class Reporte
    {
        public String nombreCliente { get; set; }
        public String DocumentoCliente   { get; set; }
        public String EmailCliente { get; set; }
        public DateTime fechaCompra { get; set; }
        public int totalCompra { get; set; }
    }
}