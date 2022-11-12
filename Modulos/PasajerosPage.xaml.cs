using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoPOE.Modulos
{
    /// <summary>
    /// Lógica de interacción para Page1.xaml
    /// </summary>
    /// 
    public enum EstadosPasajeros
    {
        Default,
        Nuevo,
        Modificar,
        Eliminar
    }
    public partial class PasajerosPage : Page
    {
        public int EstadoActual;
        public int utlimoId;
        public PasajerosPage()
        {
            InitializeComponent();
            Refresh();
        }

        private void Refresh()
        {
            List<PasajeroViewModel> listPasajero = new List<PasajeroViewModel>();
            using (Modelo.sivarviajesEntities db = new Modelo.sivarviajesEntities())
            {
                listPasajero = (from d in db.pasajero
                                  select new PasajeroViewModel
                                  {
                                      id = d.idPasajero,
                                      nombre = d.nombre,
                                      direccion = d.direccion,
                                      telefono = d.telefono,
                                      email = d.email
                                  }
                                  ).ToList();
            }

            DGPasajero.ItemsSource = listPasajero;
            if (listPasajero.Count == 0)
                utlimoId = 0;
            else
                utlimoId = listPasajero.Last().id;

            txtDireccion.IsEnabled = false;
            txtNombre.IsEnabled = false;
            txtTelefono.IsEnabled = false;

            txtEmail.IsEnabled = false;
            btnGuardar.IsEnabled = false;

            chkEliminar.IsChecked = false;
            Agregar_nuevo.IsEnabled = true;

            txtDireccion.Text = "";
            txtNombre.Text = "";
            txtTelefono.Text = "";
            EstadoActual = Convert.ToInt32(EstadosPasajeros.Default);

        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            using (Modelo.sivarviajesEntities db = new Modelo.sivarviajesEntities())
            {
                if (EstadoActual == ((int)EstadosPasajeros.Nuevo))
                {
                    //string FechaFormat;
                    var oPasajero = new Modelo.pasajero();
                    oPasajero.nombre = txtNombre.Text;
                    oPasajero.telefono = txtTelefono.Text;
                    oPasajero.email = txtEmail.Text;
                    oPasajero.direccion = txtDireccion.Text.ToString();
                    db.pasajero.Add(oPasajero);
                    db.Entry(oPasajero).State = System.Data.Entity.EntityState.Added;
                    
                    db.SaveChanges();
                }
                else if (EstadoActual == ((int)EstadosPasajeros.Modificar))
                {
                    var oPasajero = db.pasajero.Find(Convert.ToInt32(txtIdPropietrio.Text));
                    oPasajero.nombre = txtNombre.Text;
                    oPasajero.telefono = txtTelefono.Text;
                    oPasajero.direccion = txtDireccion.Text;
                    oPasajero.email = txtEmail.Text;
                    db.Entry(oPasajero).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else if (EstadoActual == ((int)EstadosPasajeros.Eliminar) || chkEliminar.IsChecked == true)
                {
                    var oPasajero = db.pasajero.Find(Convert.ToInt32(txtIdPropietrio.Text));
                    db.Entry(oPasajero).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }

            }
            Refresh();
        }

        private void Agregar_nuevo_Click(object sender, RoutedEventArgs e)
        {
            EstadoActual = Convert.ToInt32(EstadosPasajeros.Nuevo);
            txtDireccion.IsEnabled = true;
            txtNombre.IsEnabled = true;
            txtTelefono.IsEnabled = true;
            txtEmail.IsEnabled = true;
            btnGuardar.IsEnabled = true;
            Agregar_nuevo.IsEnabled = false;
            chkEliminar.IsEnabled = false;
            chkEliminar.IsChecked = false;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            EstadoActual = Convert.ToInt32(EstadosPasajeros.Eliminar);
            txtDireccion.IsEnabled = false;
            txtNombre.IsEnabled = false;
            txtTelefono.IsEnabled = false;
            txtEmail.IsEnabled = false;
            btnGuardar.IsEnabled = true;
            Agregar_nuevo.IsEnabled = false;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void DGPasajero_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            EstadoActual = Convert.ToInt32(EstadosPasajeros.Modificar);
            txtDireccion.IsEnabled = true;
            txtNombre.IsEnabled = true;
            txtTelefono.IsEnabled = true;
            txtEmail.IsEnabled = true;
            btnGuardar.IsEnabled = true;
            Agregar_nuevo.IsEnabled = false;
            chkEliminar.IsEnabled = true;
            chkEliminar.IsChecked = false;
        }
    }

    public class PasajeroViewModel
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
    }
}
