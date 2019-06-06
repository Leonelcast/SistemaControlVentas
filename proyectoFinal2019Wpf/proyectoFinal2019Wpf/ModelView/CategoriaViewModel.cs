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
    class CategoriaViewModel : INotifyPropertyChanged, ICommand
    {

        private DataContext db = new DataContext();
        private ObservableCollection<Categoria> _Categorias;
        private CategoriaViewModel _Instancia;
        private Boolean _IsReadOnlyDescripcion = true;
        private string _Descripcion;

        public Boolean IsReadOnlyDescripcion
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
                

            }
            if (parameter.Equals("Save"))
            {
                Categoria nuevo = new Categoria();
                nuevo.Descripcion = this.Descripcion;
                db.Categorias.Add(nuevo);
                db.SaveChanges();
                this.Categorias.Add(nuevo);
                MessageBox.Show("Registro Almacenado");

            }
        }
    }
}
