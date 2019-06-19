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
   class CategoriaViewModel : INotifyPropertyChanged, ICommand
    {
        #region "Campos"
        private DataContext db = new DataContext();
        private ObservableCollection<Categoria> _Categorias;
        private ACCION accion = ACCION.NINGUNO;
        private CategoriaViewModel _Instancia;
        private bool _IsReadOnlyDescripcion = false;
        private string _Descripcion;
        private bool _IsEnabledAdd = true;
        private bool _IsEnabledUpdate = true;
        private bool _IsEnabledDelete = true;
        private bool _IsEnabledSave = false;
        private bool _IsEnabledCancel = false;
        private Categoria _SelectCategoria;

        #endregion
        public Categoria SelectCateforia
        {
            get { return this._SelectCategoria; }
            set
            {
                if(value != null)
                {
                    this._SelectCategoria = value;
                    this.Descripcion = value.Descripcion;
                    ChangeNotify("SelectCategoria");
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
        public CategoriaViewModel Instancia
        {
            get { return this._Instancia; }
            set { this._Instancia = value; }
        }
        public CategoriaViewModel()
        {
            this.Instancia = this;
            this.Titulo = "Categoria: ";
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
        public ObservableCollection<Categoria> Categorias
        {
            get
            {
                if (this._Categorias == null)
                {
                    this._Categorias = new ObservableCollection<Categoria>();
                    foreach (Categoria elemento in db.Categorias.ToList())
                    {
                        this._Categorias.Add(elemento);
                    }

                }
                return this._Categorias;
            }
            set { this._Categorias = value; }
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
                        Categoria nuevo = new Categoria();
                        nuevo.Descripcion = this.Descripcion;
                        db.Categorias.Add(nuevo);
                        db.SaveChanges();
                        this.Categorias.Add(nuevo);
                        MessageBox.Show("Registro Almacenado");
                        break;
                    case ACCION.ACTUALIZAR:
                        try
                        {
                            int position = this.Categorias.IndexOf(this.SelectCateforia);
                            var updateCategoria = this.db.Categorias.Find(this.SelectCateforia.CodigoCategoria);
                            updateCategoria.Descripcion = this.Descripcion;
                            this.db.Entry(updateCategoria).State = EntityState.Modified;
                            this.db.SaveChanges();
                            this.Categorias.RemoveAt(position);
                            this.Categorias.Insert(position, updateCategoria);
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
                if(this.SelectCateforia != null)
                {

                    var respuesta = MessageBox.Show("Esta seguro de eliminar el registro?", "Elminimar", MessageBoxButton.YesNo);
                    if (respuesta == MessageBoxResult.Yes)
                    {
                        try
                        {
                            db.Categorias.Remove(this.SelectCateforia);
                            db.SaveChanges();
                            this.Categorias.Remove(this.SelectCateforia);
                        }
                        catch(Exception e)
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
