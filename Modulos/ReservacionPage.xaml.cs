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
    public enum EstadosReservacion
    {
        Default,
        Nuevo,
        Modificar,
        Eliminar
    }
    public partial class ReservacionPage : Page
    {
        public int EstadoActual;
        public int utlimoId;
        public ReservacionPage()
        {
            InitializeComponent();
            Refresh();
        }

        private void Refresh()
        {
            List<detalle_reservaViewModel> listDetalleReserva = new List<detalle_reservaViewModel>();
            List<PasajeroViewModel> listPasajero = new List<PasajeroViewModel>();
            List<VueloViewModel> listVuelos = new List<VueloViewModel>();
            List<detalle_reservaViewModel> listDetalle = new List<detalle_reservaViewModel>();
            using (Modelo.sivarviajesEntities db = new Modelo.sivarviajesEntities())
            {
                listDetalleReserva = (from dr in db.detalle_reservacion from r in db.reservacion from p in db.pasajero from v in db.vuelo
                                      where dr.idDetalle != 0
                                      where dr.idReservacion == r.idReservacion
                                      where dr.idPasajero == p.idPasajero
                                      where r.idVuelo == v.idVuelo
                                      select new detalle_reservaViewModel
                                      {
                                          //detalle_reservacion
                                          idDetalle = dr.idDetalle,

                                          //pasajero
                                          idPasajero = p.idPasajero,
                                          nombrePasajero = p.nombre,

                                          //reservacion
                                          idReservacion = r.idReservacion,
                                          fecha = r.fecha,
                                          cantReservada = r.cantReservada,
                                          

                                          //vuelo
                                          idVuelo = v.idVuelo,
                                          destino = v.origen + ", " + v.destino
                                      }
                                  ).ToList();

                listPasajero = (from d in db.pasajero
                                select new PasajeroViewModel
                                {
                                    id = d.idPasajero,
                                    nombre = d.nombre
                                }
                                  ).ToList();

                listVuelos = (from d in db.vuelo
                              select new VueloViewModel
                              {
                                  id = d.idVuelo,
                                  origen = d.origen + ", " + d.destino
                              }
                                  ).ToList();

                //listDetalleReserva = (from d in db.vuelo
                //                      select new detalle_reservaViewModel
                //                      {
                //                          idVuelo = d.idVuelo,
                //                          destino = (d.origen +" "+ d.destino)
                //                      }
                //                  ).ToList();

            }

            cboPasajeros.ItemsSource = listPasajero;
            cboPasajeros.DisplayMemberPath = "nombre";
            cboPasajeros.SelectedValuePath = "id";

            cboVuelos.ItemsSource = listVuelos;
            cboVuelos.DisplayMemberPath = "origen";
            cboVuelos.SelectedValuePath = "id";


            DGDetalles.ItemsSource = listDetalleReserva;
            if (listDetalleReserva.Count == 0)
                utlimoId = 0;
            else
                utlimoId = listDetalleReserva.Last().idDetalle;

            cboPasajeros.IsEnabled = false;
            cboVuelos.IsEnabled = false;
            dpFecha.IsEnabled = false;
            txtAsientos.IsEnabled = false;
            btnGuardar.IsEnabled = false;

            chkEliminar.IsChecked = false;
            chkEliminar.IsEnabled = false;
            Agregar_nuevo.IsEnabled = true;

            cboPasajeros.SelectedIndex = 0; ;
            cboVuelos.SelectedIndex = 0;
            dpFecha.SelectedDate = DateTime.Today;
            txtAsientos.Text = "";
            btnGuardar.IsEnabled = false;
        }
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            using (Modelo.sivarviajesEntities db = new Modelo.sivarviajesEntities())
            {
                if (EstadoActual == ((int)EstadosReservacion.Nuevo))
                {
                    //string FechaFormat;
                    var oReservacion = new Modelo.reservacion();
                    oReservacion.fecha = dpFecha.SelectedDate;
                    oReservacion.idVuelo = Convert.ToInt32(cboVuelos.SelectedValue);
                    oReservacion.cantReservada = Convert.ToInt32(txtAsientos.Text);
                    db.reservacion.Add(oReservacion);
                    db.Entry(oReservacion).State = System.Data.Entity.EntityState.Added;

                    var oDetalles = new Modelo.detalle_reservacion();
                    oDetalles.idReservacion = oReservacion.idReservacion;
                    oDetalles.idPasajero = Convert.ToInt32(cboPasajeros.SelectedValue);
                    db.Entry(oDetalles).State = System.Data.Entity.EntityState.Added;

                    db.SaveChanges();
                }
                else if (EstadoActual == ((int)EstadosReservacion.Modificar))
                {
                    var oReservacion = db.reservacion.Find(Convert.ToInt32(txtIdReservacion.Text));
                    oReservacion.fecha = dpFecha.SelectedDate;
                    oReservacion.idVuelo = Convert.ToInt32(cboVuelos.SelectedValue);
                    oReservacion.cantReservada = Convert.ToInt32(txtAsientos.Text);
                    db.Entry(oReservacion).State = System.Data.Entity.EntityState.Modified;

                    var oDetalles = db.detalle_reservacion.Find(Convert.ToInt32(txtIdDetallesReserva.Text));
                    oDetalles.idPasajero = Convert.ToInt32(cboPasajeros.SelectedValue);
                    db.Entry(oDetalles).State = System.Data.Entity.EntityState.Modified;

                    db.SaveChanges();
                }
                else if (EstadoActual == ((int)EstadosReservacion.Eliminar) || chkEliminar.IsChecked == true)
                {
                    var oDetalles = db.detalle_reservacion.Find(Convert.ToInt32(txtIdDetallesReserva.Text));
                    db.Entry(oDetalles).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }

            }
            Refresh();
        }

        private void Agregar_nuevo_Click(object sender, RoutedEventArgs e)
        {
            EstadoActual = Convert.ToInt32(EstadosReservacion.Nuevo);
            cboPasajeros.IsEnabled = true;
            cboVuelos.IsEnabled = true;
            dpFecha.IsEnabled = true;
            txtAsientos.IsEnabled = true;
            btnGuardar.IsEnabled = true;

            chkEliminar.IsEnabled = false;
            chkEliminar.IsChecked = false;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            EstadoActual = Convert.ToInt32(EstadosReservacion.Eliminar);
            cboPasajeros.IsEnabled = false;
            cboVuelos.IsEnabled = false;
            dpFecha.IsEnabled = false;
            txtAsientos.IsEnabled = false;
            btnGuardar.IsEnabled = true;
            Agregar_nuevo.IsEnabled = false;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void DGPasajero_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            EstadoActual = Convert.ToInt32(EstadosReservacion.Modificar);
            cboPasajeros.IsEnabled = true;
            cboVuelos.IsEnabled = true;
            dpFecha.IsEnabled = true;
            txtAsientos.IsEnabled = true;
            btnGuardar.IsEnabled = true;
            Agregar_nuevo.IsEnabled = false;
            chkEliminar.IsEnabled = true;
            chkEliminar.IsChecked = false;
        }
    }

    public class detalle_reservaViewModel
    {
        public int idDetalle { get; set; }
        public int idReservacion { get; set; }
        public int idPasajero { get; set; }
        public string nombrePasajero { get; set; }
        public int idVuelo { get; set; }
        public string destino { get; set; }
        public int? cantReservada { get; set; }
        public DateTime? fecha { get; set; }
    }

}
