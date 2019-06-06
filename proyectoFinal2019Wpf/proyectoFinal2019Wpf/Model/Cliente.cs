using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoFinal2019Wpf.Model
{
    public class Cliente
    {
        public string Nit { get; set; }
        public string Dpi { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public virtual ICollection<EmailCliente> EmailCliente { get; set; }
        public virtual ICollection<TelefonoCliente> TelefonoCliente { get; set; }
        public virtual ICollection<Factura> Factura { get; set; }
    }
}
