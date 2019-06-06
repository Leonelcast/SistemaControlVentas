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
    class TipoEmpaqueViewModel: INotifyPropertyChanged, ICommand
    {
        private DataContext db = new DataContext();
        private ObservableCollection<TipoEmpaque> _TipoEmpaques;
        private TipoEmpaqueViewModel _Instancia;
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
                    this.TipoEmpaques = new ObservableCollection<TipoEmpaque>();
                    foreach (TipoEmpaque elemento in db.TipoEmpaques.ToList())
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


            }
            if (parameter.Equals("Save"))
            {
                TipoEmpaque nuevo = new TipoEmpaque();
                nuevo.Descripcion = this.Descripcion;
                db.TipoEmpaques.Add(nuevo);
                db.SaveChanges();
                this.TipoEmpaques.Add(nuevo);
                MessageBox.Show("Registro Almacenado");

            }
        }
    }
}
