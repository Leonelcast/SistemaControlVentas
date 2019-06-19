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
    enum ACCION
    {
        NINGUNO,
        NUEVO,
        ACTUALIZAR,
        GUARDAR
    };
    class ClienteViewModel : INotifyPropertyChanged, ICommand
    {
        #region "Campos"
        private ACCION accion = ACCION.NINGUNO;
        private DataContext db = new DataContext();
        private bool _IsReadOnlyDPI = false;
        private bool _IsReadOnlyNombre = false;
        private bool _IsReadOnlyDireccion = false;
        private bool _IsReadOnlyNit = false;
        private string _Nit;
        private string _DPI;
        private string _Nombre;
        private string _Direccion;
        private ObservableCollection<Cliente> _Clientes;
        private ClienteViewModel _Instancia;
        private bool _IsEnabledAdd = true;
        private bool _IsEnabledUpdate = true;
        private bool _IsEnabledDelete = true;
        private bool _IsEnabledSave = false;
        private bool _IsEnabledCancel = false;
        private Cliente _SelectCliente;


        #endregion
        public Cliente SelectCliente
        {
            get { return this._SelectCliente; }
            set
            {
                if(value!= null)
                {
                    this._SelectCliente = value;
                    this.Nit = value.Nit;
                    this.DPI = value.DPI;
                    this.Nombre = value.Nombre;
                    this.Direccion = value.Direccion;
                    ChangeNotify("SelectCliente");

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

        public ClienteViewModel Instancia
        {
            get { return this._Instancia; }
            set { this._Instancia = value; }
        }

        public bool IsReadOnlyDPI
        {
            get { return this._IsReadOnlyDPI; }
            set { this._IsReadOnlyDPI = value; ChangeNotify(" IsReadOnlyDPI"); }
        }
        public bool IsreadOnlyNombre
        {
            get { return this._IsReadOnlyNombre; }
            set { this._IsReadOnlyNombre = value; ChangeNotify(" IsReadOnlyNombre"); }

        }
        public bool IsReadOnlyDireccion
        {
            get { return this._IsReadOnlyDireccion; }
            set { this._IsReadOnlyDireccion = value; ChangeNotify(" IsReadOnlyDireccion"); }
        }
        public string DPI
        {
            get { return this._DPI; }
            set { this._DPI = value; ChangeNotify(" DPI"); }

        }
        public string Nombre
        {
            get { return this._Nombre; }
            set { this._Nombre = value; ChangeNotify("Nombre"); }
        }
        public string Direccion
        {
            get { return this._Direccion; }
            set { this._Direccion = value; ChangeNotify("Direccion"); }
        }

        public ClienteViewModel()
        {
            this.Instancia = this;
            this.Titulo = "Clientes: ";
        }
        public string Nit
        {
            get { return this._Nit; }
            set { this._Nit = value; ChangeNotify("Nit"); }
        }
        public bool IsReadOnlyNit
        {
            get { return this._IsReadOnlyNit; }
            set { this._IsReadOnlyNit = value; ChangeNotify("IsReadOnlyNit"); }
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
        public ObservableCollection<Cliente> Clientes
        {
            get
            {
                if (this._Clientes == null)
                {
                    this._Clientes = new ObservableCollection<Cliente>();
                    foreach (Cliente elemento in db.Clientes.ToList())
                    {
                        this._Clientes.Add(elemento);
                    }

                }
                return this._Clientes;
            }
            set { this._Clientes = value; }
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.Equals("Add"))
            {
                this.IsReadOnlyNit = false;
                this.IsReadOnlyDireccion = true;
                this.IsreadOnlyNombre = true;
                this.IsReadOnlyDPI = true;
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
                    Cliente nuevo = new Cliente();
                    nuevo.Nit = this.Nit;
                    nuevo.Direccion = this.Direccion;
                    nuevo.DPI = this.DPI;
                    nuevo.Nombre = this.Nombre;
                    db.Clientes.Add(nuevo);
                    db.SaveChanges();
                    this.Clientes.Add(nuevo);
                    MessageBox.Show("Registro Almacenado");
                        break;
                    case ACCION.ACTUALIZAR:
                        try
                        {
                            int posicion = this.Clientes.IndexOf(this.SelectCliente);
                            var updateCliente = this.db.Clientes.Find(this.SelectCliente.Nit);
                            updateCliente.Nit = this.Nit;
                            updateCliente.DPI = this.DPI;
                            updateCliente.Direccion = this.Direccion;
                            updateCliente.Nombre = this.Nombre;
                            this.db.Entry(updateCliente).State = EntityState.Modified; //se le da un objeto al estado 
                            this.db.SaveChanges();
                            this.Clientes.RemoveAt(posicion); //se elimina el elemento anterior
                            this.Clientes.Insert(posicion, updateCliente);
                            MessageBox.Show("Registro actualizado!!");
                          
                        }
                        catch (Exception e)
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
                this.IsReadOnlyDPI = false;
                this.IsReadOnlyDireccion = false;
                this.IsreadOnlyNombre = false;
                this.IsEnabledAdd = false;
                this.IsEnabledDelete = false;
                this.IsEnabledUpdate = false;
                this.IsEnabledSave = true;
                this.IsEnabledCancel = true;


            }
            else if (parameter.Equals("Delete"))
            {
                if (this.SelectCliente != null)
                {
                    var respuesta = MessageBox.Show("Esta seguro de eliminar el registro?", "Elminimar", MessageBoxButton.YesNo);
                    if (respuesta == MessageBoxResult.Yes)
                    {
                        try
                        {
                            db.Clientes.Remove(this.SelectCliente);
                            db.SaveChanges();
                            this.Clientes.Remove(this.SelectCliente);

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
                this.IsReadOnlyDPI = true;
                this.IsReadOnlyDireccion = true;
                this.IsreadOnlyNombre = true;
            }


        }
    }
}
