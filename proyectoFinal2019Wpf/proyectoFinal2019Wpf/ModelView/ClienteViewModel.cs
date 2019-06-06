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
    class ClienteViewModel : INotifyPropertyChanged, ICommand
    {

        private DataContext db = new DataContext();
        private Boolean _IsReadOnlyDpi = true;
        private Boolean _IsReadOnlyNombre = true;
        private Boolean _IsReadOnlyDireccion = true;
        private string _Dpi;
        private string _Nombre;
        private string _Direccion;
        private ObservableCollection<Cliente> _Clientes;
        private ClienteViewModel _Instancia;

        public ClienteViewModel Instancia
        {
            get { return this._Instancia; }
            set { this._Instancia = value; }
        }

        public Boolean IsReadOnlyDpi
        {
            get { return this._IsReadOnlyDpi; }
            set { this.IsReadOnlyDpi = value; ChangeNotify(" IsReadOnlyDpi"); }
        }
        public Boolean IsreadOnlyNombre
        {
            get { return this._IsReadOnlyNombre; }
            set { this._IsReadOnlyNombre = value; ChangeNotify(" IsReadOnlyNombre"); }

        }
        public Boolean IsReadOnlyDireccion
        {
            get { return this._IsReadOnlyDireccion; }
            set { this._IsReadOnlyDireccion = value; ChangeNotify(" IsReadOnlyDireccion"); }
        }
        public string Dpi
        {
            get { return this._Dpi; }
            set { this._Dpi = value; ChangeNotify(" Dpi"); }

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
                this.IsReadOnlyDireccion = false;
                this.IsreadOnlyNombre = false;
                this.IsReadOnlyDpi = false;

            }
            if (parameter.Equals("Save"))
            {
                Cliente nuevo = new Cliente();
                nuevo.Direccion = this.Direccion;
                nuevo.Dpi = this.Dpi;
                nuevo.Nombre = this.Nombre;
                db.Clientes.Add(nuevo);
                db.SaveChanges();
                this.Clientes.Add(nuevo);
                MessageBox.Show("Registro Almacenado");

            }

        }
    }
}
