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
    public enum EstadosVuelo
    {
        Default,
        Nuevo,
        Modificar,
        Eliminar
    }
    public partial class VuelosPage : Page
    {
        public int EstadoActual;
        public int ultimoId;
        public VuelosPage()
        {
            InitializeComponent();
            Refresh();
        }

        private void Refresh()
        {
            List<VueloViewModel> listVuelos = new List<VueloViewModel>();
            using (Modelo.sivarviajesEntities db = new Modelo.sivarviajesEntities())
            {
                listVuelos = (from v in db.vuelo
                                  select new VueloViewModel
                                  {
                                      id = v.idVuelo,
                                      aerolinea = v.aerolinea,
                                      origen = v.origen,
                                      destino = v.destino,
                                      fecha = v.fecha,
                                      salida = v.hora_salida,
                                      llegada = v.hora_llegada,
                                      asientos = v.asientos,
                                      costo = v.costoVuelo
                                  }
                                  ).ToList();
            }

            DGVuelos.ItemsSource = listVuelos;
            if (listVuelos.Count == 0)
                ultimoId = 0;
            else
                ultimoId = listVuelos.Last().id;

            txtAerolinea.IsEnabled = false;
            txtOrigen.IsEnabled = false;
            txtDestino.IsEnabled = false;
            dpFecha.IsEnabled = false;
            txtSalida.IsEnabled = false;
            txtLlegada.IsEnabled = false;
            txtAsientos.IsEnabled = false;
            txtCosto.IsEnabled = false;

            btnGuardar.IsEnabled = false;

            chkEliminar.IsChecked = false;
            chkEliminar.IsEnabled = false;
            Agregar_nuevo.IsEnabled = true;

            txtAerolinea.Text = "";
            txtOrigen.Text = "";
            txtDestino.Text = "";
            txtSalida.Text = "";
            txtLlegada.Text = "";
            txtAsientos.Text = "";
            txtCosto.Text = "";
            EstadoActual = Convert.ToInt32(EstadosVuelo.Default);

        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            using (Modelo.sivarviajesEntities db = new Modelo.sivarviajesEntities())
            {
                if (EstadoActual == ((int)EstadosVuelo.Nuevo))
                {
                    //string FechaFormat;
                    var oVuelo = new Modelo.vuelo();
                    oVuelo.aerolinea = txtAerolinea.Text;
                    oVuelo.origen = txtOrigen.Text;
                    oVuelo.destino = txtDestino.Text;
                    oVuelo.fecha = dpFecha.SelectedDate;
                    oVuelo.hora_salida = TimeSpan.Parse(txtSalida.Text);
                    oVuelo.hora_llegada = TimeSpan.Parse(txtLlegada.Text);
                    oVuelo.asientos = int.Parse(txtAsientos.Text);
                    oVuelo.costoVuelo = Double.Parse(txtCosto.Text);
                    db.vuelo.Add(oVuelo);
                    db.Entry(oVuelo).State = System.Data.Entity.EntityState.Added;                    
                    db.SaveChanges();
                    lblMensajeAlerta.Content = "";
                }
                else if (EstadoActual == ((int)EstadosVuelo.Modificar))
                {
                    var oVuelo = db.vuelo.Find(Convert.ToInt32(txtIdVuelo.Text));
                    oVuelo.aerolinea = txtAerolinea.Text;
                    oVuelo.origen = txtOrigen.Text;
                    oVuelo.destino = txtDestino.Text;
                    oVuelo.fecha = dpFecha.SelectedDate;
                    oVuelo.hora_salida = TimeSpan.Parse(txtSalida.Text);
                    oVuelo.hora_llegada = TimeSpan.Parse(txtLlegada.Text);
                    oVuelo.asientos = int.Parse(txtAsientos.Text);
                    oVuelo.costoVuelo = Double.Parse(txtCosto.Text);
                    db.Entry(oVuelo).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    lblMensajeAlerta.Content = "";

                }
                else if (EstadoActual == ((int)EstadosVuelo.Eliminar) || chkEliminar.IsChecked == true)
                {
                    try
                    {
                        var oVuelo = db.vuelo.Find(Convert.ToInt32(txtIdVuelo.Text));
                        db.Entry(oVuelo).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                        lblMensajeAlerta.Content = "";
                    }
                    catch (Exception ex)
                    {
                        lblMensajeAlerta.Content = "No se puede eliminar, El resgistro esta referencado a otro(s) record(s)";
                    }
                    
                }

            }
            Refresh();
        }

        private void Agregar_nuevo_Click(object sender, RoutedEventArgs e)
        {
            EstadoActual = Convert.ToInt32(EstadosVuelo.Nuevo);
            txtAerolinea.IsEnabled = true;
            txtOrigen.IsEnabled = true;
            txtDestino.IsEnabled = true;
            dpFecha.IsEnabled = true;
            txtSalida.IsEnabled = true;
            txtLlegada.IsEnabled = true;
            txtAsientos.IsEnabled = true;
            txtCosto.IsEnabled = true;
            btnGuardar.IsEnabled = true;
            Agregar_nuevo.IsEnabled = false;
            chkEliminar.IsEnabled = false;
            chkEliminar.IsChecked = false;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            EstadoActual = Convert.ToInt32(EstadosVuelo.Eliminar);
            txtAerolinea.IsEnabled = false;
            txtOrigen.IsEnabled = false;
            txtDestino.IsEnabled = false;
            dpFecha.IsEnabled = false;
            txtSalida.IsEnabled = false;
            txtLlegada.IsEnabled = false;
            txtAsientos.IsEnabled = false;
            txtCosto.IsEnabled = false;
            btnGuardar.IsEnabled = true;
            Agregar_nuevo.IsEnabled = false;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void DGPasajero_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            EstadoActual = Convert.ToInt32(EstadosVuelo.Modificar);
            txtAerolinea.IsEnabled = true;
            txtOrigen.IsEnabled = true;
            txtDestino.IsEnabled = true;
            dpFecha.IsEnabled = true;
            txtSalida.IsEnabled = true;
            txtLlegada.IsEnabled = true;
            txtAsientos.IsEnabled = true;
            txtCosto.IsEnabled = true;
            btnGuardar.IsEnabled = true;
            Agregar_nuevo.IsEnabled = false;
            chkEliminar.IsEnabled = true;
            chkEliminar.IsChecked = false;
        }

    }

    public class VueloViewModel
    {
        public int id { get; set; }
        public string aerolinea { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public DateTime? fecha { get; set; }
        public TimeSpan? salida { get; set; }
        public TimeSpan? llegada { get; set; }
        public int? asientos { get; set;}
        public double? costo { get; set;}

    }
}
