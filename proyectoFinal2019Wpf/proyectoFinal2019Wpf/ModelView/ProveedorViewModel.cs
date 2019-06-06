using proyectoFinal2019Wpf.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace proyectoFinal2019Wpf.ModelView
{
    class ProveedorViewModel : INotifyPropertyChanged, ICommand
    {
        private DataContext db = new DataContext();
        private ObservableCollection<Proveedor> _Proveedores;
        private ProveedorViewModel _Instancia;
        private Boolean _IsReadOnlyNit = true;
        private Boolean _IsReadOnlyRazonSocial = true;
        private Boolean _IsReadOnlyDireccion = true;
        private Boolean _IsReadOnlyPaginaWeb = true;
        private Boolean _IsReadOnlyContactoPrincipal = true;
        private string _Nit;
        private string _RazonSocial;
        private string _Direccion;
        private string _PaginaWeb;
        private string _ContactoPrincipal;

        public Boolean IsReadOnlyNit
        {
            get { return this._IsReadOnlyNit; }
            set { this._IsReadOnlyNit = value; ChangeNotify(" IsReadOnlyNit"); }
        }
        public Boolean IsReadOnlyRazonSocial
        {
            get { return this._IsReadOnlyRazonSocial; }
            set { this._IsReadOnlyRazonSocial = value; ChangeNotify(" IsReadOnlyRazonSocial"); }
        }
        public Boolean IsReadOnlyDireccion
        {
            get { return this._IsReadOnlyDireccion; }
            set { this._IsReadOnlyDireccion = value; ChangeNotify(" IsReadOnlyDireccion"); }
        }
        public Boolean IsReadOnlyPaginaWeb
        {
            get { return this._IsReadOnlyPaginaWeb; }
            set { this._IsReadOnlyPaginaWeb = value; ChangeNotify(" IsReadOnlyPaginaWeb"); }
        }
        public Boolean IsReadOnlyContactoPrincipal
        {
            get { return this._IsReadOnlyContactoPrincipal; }
            set { this._IsReadOnlyContactoPrincipal = value; ChangeNotify(" IsReadOnlyContactoPrincipal"); }
        }
        public string Nit
        {
            get { return this._Nit; }
            set { this._Nit = value; ChangeNotify("Nit"); }
        }
        public string RazonSocial
        {
            get { return this._RazonSocial; }
            set { this._RazonSocial = value; ChangeNotify(" RazonSocial"); }
        }
        public string Direccion
        {
            get { return this._Direccion; }
            set { this._Direccion = value; ChangeNotify(" Direccion"); }
        }
        public string PaginaWeb
        {
            get { return this._PaginaWeb; }
            set { this._PaginaWeb = value; ChangeNotify(" PaginaWeb"); }
        }
        public string ContactoPrincipal
        {
            get { return this._ContactoPrincipal; }
            set { this._ContactoPrincipal = value; ChangeNotify(" ContactoPrincipal"); }
        }
        public ProveedorViewModel Instancia
        {
            get { return this._Instancia; }
            set { this._Instancia = value; }
        }
        public ProveedorViewModel()
        {
            this.Instancia = this;
            this.Titulo = "Proveedores: ";
        }
        public string Titulo { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;

        public void ChangeNotify(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public ObservableCollection<Proveedor> Proveedores
        {
            get
            {
                if (this._Proveedores == null)
                {
                    this._Proveedores = new ObservableCollection<Proveedor>();
                    foreach (Proveedor elemento in db.Proveedores.ToList())
                    {
                        this._Proveedores.Add(elemento);
                    }

                }
                return this._Proveedores;
            }
            set { this._Proveedores = value; }
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            if (parameter.Equals("Add"))
            {
                this.IsReadOnlyNit = false;
                this.IsReadOnlyContactoPrincipal = false;
                this.IsReadOnlyPaginaWeb = false;
                this.IsReadOnlyRazonSocial = false;
                this.IsReadOnlyDireccion = false;
            }
            if (parameter.Equals("Save"))
            {
                Proveedor nuevo = new Proveedor();

                nuevo.Nit = this.Nit;
                nuevo.Direccion = this.Direccion;
                nuevo.PaginaWeb = this.PaginaWeb;
                nuevo.PaginaWeb = this.RazonSocial;
                nuevo.ContactoPrincipal = this.ContactoPrincipal;
                db.Proveedores.Add(nuevo);
                db.SaveChanges();
                this.Proveedores.Add(nuevo);
                MessageBox.Show("Registro Almacenado");
            }
        }
    }
}
