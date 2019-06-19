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

    class TipoEmpaqueViewModel : INotifyPropertyChanged, ICommand
    {
        private DataContext db = new DataContext();
        private ACCION accion = ACCION.NINGUNO;
        private ObservableCollection<TipoEmpaque> _TipoEmpaques;
        private TipoEmpaqueViewModel _Instancia;
        private bool _IsReadOnlyDescripcion = false;
        private string _Descripcion;
        private bool _IsEnabledAdd = true;
        private bool _IsEnabledUpdate = true;
        private bool _IsEnabledDelete = true;
        private bool _IsEnabledSave = false;
        private bool _IsEnabledCancel = false;
        private TipoEmpaque _SelectTipoEmpaque;

        public TipoEmpaque SelectTipoEmpaque
        {
            get { return this._SelectTipoEmpaque; }
            set
            {
                if (value != null)
                {
                    this._SelectTipoEmpaque = value;
                    this.Descripcion = value.Descripcion;
                    ChangeNotify("SelectTipoEmpaque");

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

        public bool IsReadOnlyDescripcion
        {
            get { return this._IsReadOnlyDescripcion; }
            set { this._IsReadOnlyDescripcion = value; ChangeNotify(" IsReadOnlyDescripcion"); }
        }
        public string Descripcion
        {
            get { return this._Descripcion; }
            set { this._Descripcion = value; ChangeNotify("Descripcion"); }
        }
        public TipoEmpaqueViewModel Instancia
        {
            get { return this._Instancia; }
            set { this._Instancia = value; }
        }
        public TipoEmpaqueViewModel()
        {
            this.Instancia = this;
            this.Titulo = "TipoEmpaque: ";
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
        public ObservableCollection<TipoEmpaque> TipoEmpaques
        {
            get
            {
                if (this._TipoEmpaques == null)
                {
                    this._TipoEmpaques = new ObservableCollection<TipoEmpaque>();
                    foreach (TipoEmpaque elemento in db.TipoEmpaques.ToList()) //error
                    {
                        this._TipoEmpaques.Add(elemento);
                    }

                }
                return this._TipoEmpaques;
            }
            set { this._TipoEmpaques = value; }
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.Equals("Add"))
            {
                this.IsReadOnlyDescripcion = false;
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
                        TipoEmpaque nuevo = new TipoEmpaque();
                        nuevo.Descripcion = this.Descripcion;
                        db.TipoEmpaques.Add(nuevo);
                        db.SaveChanges();
                        this.TipoEmpaques.Add(nuevo);
                        MessageBox.Show("Registro Almacenado");
                        break;
                    case ACCION.ACTUALIZAR:
                        try
                        {
                            int posicion = this.TipoEmpaques.IndexOf(this.SelectTipoEmpaque);
                            var updateTipoEmpaque = this.db.TipoEmpaques.Find(this.SelectTipoEmpaque.CodigoEmpaque);
                            updateTipoEmpaque.Descripcion = this.Descripcion;
                            this.db.Entry(updateTipoEmpaque).State = EntityState.Modified;
                            this.db.SaveChanges();
                            this.TipoEmpaques.RemoveAt(posicion);
                            this.TipoEmpaques.Insert(posicion, updateTipoEmpaque);
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
                this.IsReadOnlyDescripcion = false;
                this.IsEnabledAdd = false;
                this.IsEnabledDelete = false;
                this.IsEnabledUpdate = false;
                this.IsEnabledSave = true;
                this.IsEnabledCancel = true;


            }
            else if (parameter.Equals("Delete"))
            {
                if (this.SelectTipoEmpaque != null)
                {
                    var respuesta = MessageBox.Show("Esta seguro de eliminar el registro?", "Elminimar", MessageBoxButton.YesNo);
                    if (respuesta == MessageBoxResult.Yes)
                    {
                        try
                        {
                            db.TipoEmpaques.Remove(this.SelectTipoEmpaque);
                            db.SaveChanges();
                            this.TipoEmpaques.Remove(this.SelectTipoEmpaque);

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
                this.IsReadOnlyDescripcion = true;

            }
        }

    }
}