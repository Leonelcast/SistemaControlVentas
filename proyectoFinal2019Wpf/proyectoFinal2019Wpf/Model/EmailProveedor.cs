using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoFinal2019Wpf.Model
{
    public class EmailProveedor
    {
        public int CodigoEmail { get; set; }
        public string Email { get; set; }
        public int CodigoProveedor { get; set; }
        public virtual Proveedor Proveedor { get; set; }
    }
}
