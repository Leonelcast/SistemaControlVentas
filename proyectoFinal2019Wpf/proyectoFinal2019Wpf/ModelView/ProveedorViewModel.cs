using proyectoFinal2019Wpf.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
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
        private ACCION accion = ACCION.NINGUNO;
        private ObservableCollection<Proveedor> _Proveedores;
        private ProveedorViewModel _Instancia;
        private bool _IsReadOnlyNit = false;
        private bool _IsReadOnlyRazonSocial = false;
        private bool _IsReadOnlyDireccion = false;
        private bool _IsReadOnlyPaginaWeb = false;
        private bool _IsReadOnlyContactoPrincipal = false;
        private string _Nit;
        private string _RazonSocial;
        private string _Direccion;
        private string _PaginaWeb;
        private string _ContactoPrincipal;
        private bool _IsEnabledAdd = true;
        private bool _IsEnabledUpdate = true;
        private bool _IsEnabledDelete = true;
        private bool _IsEnabledSave = false;
        private bool _IsEnabledCancel = false;
        private Proveedor _SelectProveedor;

        public Proveedor SelectProveedor
        {
            get { return this._SelectProveedor; }
            set
            {
                if(value!= null)
                {
                    this._SelectProveedor = value;
                    this.Nit = value.Nit;
                    this.RazonSocial = value.RazonSocial;
                    this.Direccion = value.Direccion;
                    this.PaginaWeb = value.PaginaWeb;
                    this.ContactoPrincipal = value.ContactoPrincipal;
                    ChangeNotify("SelectProveedor");
                }
            }
        }
        public bool IsEnabledAdd
        {
            get { return _IsEnabledAdd; }
            set { this._IsEnabledAdd = value; ChangeNotify("IsEnabledAdd"); }
        }
        public bool IsEnabledDelete
        {
            get { return _IsEnabledDelete; }
            set { this._IsEnabledDelete = value; ChangeNotify("IsEnabledDelete"); }
        }
        public bool IsEnabledUpdate
        {
            get { return _IsEnabledUpdate; }
            set { this._IsEnabledUpdate = value; ChangeNotify("IsEnabledUpdate"); }
        }
        public bool IsEnabledSave
        {
            get { return _IsEnabledSave; }
            set { this._IsEnabledSave = value; ChangeNotify("IsEnabledSave"); }
        }
        public bool IsEnabledCancel
        {
            get { return _IsEnabledCancel; }
            set { this._IsEnabledCancel = value; ChangeNotify("IsEnabledCancel"); }
        }

        public bool IsReadOnlyNit
        {
            get { return this._IsReadOnlyNit; }
            set { this._IsReadOnlyNit = value; ChangeNotify(" IsReadOnlyNit"); }
        }
        public bool IsReadOnlyRazonSocial
        {
            get { return this._IsReadOnlyRazonSocial; }
            set { this._IsReadOnlyRazonSocial = value; ChangeNotify(" IsReadOnlyRazonSocial"); }
        }
        public bool IsReadOnlyDireccion
        {
            get { return this._IsReadOnlyDireccion; }
            set { this._IsReadOnlyDireccion = value; ChangeNotify(" IsReadOnlyDireccion"); }
        }
        public bool IsReadOnlyPaginaWeb
        {
            get { return this._IsReadOnlyPaginaWeb; }
            set { this._IsReadOnlyPaginaWeb = value; ChangeNotify(" IsReadOnlyPaginaWeb"); }
        }
        public bool IsReadOnlyContactoPrincipal
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
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.Equals("Add"))
            {
                this.IsReadOnlyNit = true;
                this.IsReadOnlyContactoPrincipal = true;
                this.IsReadOnlyPaginaWeb = true;
                this.IsReadOnlyRazonSocial = true;
                this.IsReadOnlyDireccion = true;
                this.accion = ACCION.NUEVO;
                this.IsEnabledAdd = false;
                this.IsEnabledDelete = false;
                this.IsEnabledUpdate = false;
                this.IsEnabledSave = true;
                this.IsEnabledCancel = true;
            }
            if (parameter.Equals("Save"))
            {
                this.IsEnabledAdd = true;
                this.IsEnabledDelete = true;
                this.IsEnabledUpdate = true;
                this.IsEnabledSave = false;
                this.IsEnabledCancel = false;

                switch (this.accion)
                {

                    case ACCION.NUEVO:
                    Proveedor nuevo = new Proveedor();
                    nuevo.Nit = this.Nit;
                    nuevo.RazonSocial = this.RazonSocial;
                    nuevo.Direccion = this.Direccion;
                    nuevo.PaginaWeb = this.PaginaWeb;
                    nuevo.ContactoPrincipal = this.ContactoPrincipal;
                    db.Proveedores.Add(nuevo);
                    db.SaveChanges();
                    this.Proveedores.Add(nuevo);
                    MessageBox.Show("Registro Almacenado");
                        break;
                    case ACCION.ACTUALIZAR:
                        try
                        {
                            int posicion = this.Proveedores.IndexOf(this.SelectProveedor);
                            var updateProveedores = this.db.Proveedores.Find(this.SelectProveedor.CodigoProveedor);
                            updateProveedores.Nit = this.Nit;
                            updateProveedores.RazonSocial = this.RazonSocial;
                            updateProveedores.Direccion = this.Direccion;
                            updateProveedores.PaginaWeb = this.PaginaWeb;
                            updateProveedores.ContactoPrincipal = this.ContactoPrincipal;
                            this.db.Entry(updateProveedores).State = EntityState.Modified;
                            this.db.SaveChanges();
                            this.Proveedores.RemoveAt(posicion);
                            this.Proveedores.Insert(posicion, updateProveedores);
                            MessageBox.Show("Registro actualizado!!");

                        }
                        catch(Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                        break;
                }
            }
            else if (parameter.Equals("Update"))
            {
                this.accion = ACCION.ACTUALIZAR;
                this.IsReadOnlyNit = false;
                this.IsReadOnlyDireccion = false;
                this.IsReadOnlyContactoPrincipal = false;
                this.IsReadOnlyRazonSocial = false;
                this.IsReadOnlyPaginaWeb = false;
                this.IsEnabledAdd = false;
                this.IsEnabledDelete = false;
                this.IsEnabledUpdate = false;
                this.IsEnabledSave = true;
                this.IsEnabledCancel = true;


            }
            else if (parameter.Equals("Delete"))
            {
                if (this.SelectProveedor != null)
                {
                    var respuesta = MessageBox.Show("Esta seguro de eliminar el registro?", "Elminimar", MessageBoxButton.YesNo);
                    if (respuesta == MessageBoxResult.Yes)
                    {
                        try
                        {
                            db.Proveedores.Remove(this.SelectProveedor);
                            db.SaveChanges();
                            this.Proveedores.Remove(this.SelectProveedor);

                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                        MessageBox.Show("Registro eliminado correctamente!!");
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un registro", "Eliminar", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (parameter.Equals("Cancel"))
            {
                this.IsEnabledAdd = true;
                this.IsEnabledDelete = true;
                this.IsEnabledUpdate = true;
                this.IsEnabledSave = false;
                this.IsEnabledCancel = false;
                this.IsReadOnlyNit = true;
                this.IsReadOnlyDireccion = true;
                this.IsReadOnlyContactoPrincipal = true;
                this.IsReadOnlyRazonSocial = true;
                this.IsReadOnlyPaginaWeb = true;
            }


        }
    }
}
