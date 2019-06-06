using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoFinal2019Wpf.Model
{
    public class TipoEmpaque
    {
        public int CodigoEmpaque { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<Producto> Producto { get; set; }
    }
}
