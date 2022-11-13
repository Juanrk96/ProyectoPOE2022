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

namespace ProyectoPOE
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility
            if (tg_btn.IsChecked == true)
            {

                tt_home.Visibility = Visibility.Collapsed;
                tt_pasajero.Visibility = Visibility.Collapsed;
                tt_vuelo.Visibility = Visibility.Collapsed;
                tt_reserva.Visibility = Visibility.Collapsed;
                tt_reportes.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_pasajero.Visibility = Visibility.Visible;
                tt_vuelo.Visibility = Visibility.Visible;
                tt_reserva.Visibility = Visibility.Visible;
                tt_reportes.Visibility = Visibility.Visible;
            }
        }

        private void tg_btn_Unchecked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 1;
        }

        private void tg_btn_Checked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 0.3;
        }

        private void bgInicio_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(null);
            img_bg.Effect = new System.Windows.Media.Effects.BlurEffect()
            {
                Radius = 0
            };
            tg_btn.IsChecked = false;

        }

        private void bg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (frPrincipal.NavigationService.Content == null)
            {

                frPrincipal.NavigationService.Navigate(null);
                img_bg.Effect = new System.Windows.Media.Effects.BlurEffect()
                {
                    Radius = 0
                };
                tg_btn.IsChecked = false;
            }

        }

        private void bgPasajeros_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tg_btn.IsChecked = false;
            //Cargando Page 1 dentro del frame
            //frPrincipal.Content = new Page1();
            Modulos.PasajerosPage pasajerosPage = new Modulos.PasajerosPage();
            frPrincipal.NavigationService.Navigate(pasajerosPage);
            img_bg.Effect = new System.Windows.Media.Effects.BlurEffect()
            {
                Radius = 15
            };

        }
        private void bgVuelo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tg_btn.IsChecked = false;
            Modulos.VuelosPage vuelosPage = new Modulos.VuelosPage();
            frPrincipal.NavigationService.Navigate(vuelosPage);
            img_bg.Effect = new System.Windows.Media.Effects.BlurEffect()
            {
                Radius = 15
            };

        }
        private void bgReservacion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tg_btn.IsChecked = false;
            Modulos.ReservacionPage reservacionPage = new Modulos.ReservacionPage();
            frPrincipal.NavigationService.Navigate(reservacionPage);
            img_bg.Effect = new System.Windows.Media.Effects.BlurEffect()
            {
                Radius = 15
            };

        }
        private void bgReportes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tg_btn.IsChecked = false;
            //AGREGAR REPORTE(S)
            Modulos.PasajerosPage pasajerosPage = new Modulos.PasajerosPage();
            frPrincipal.NavigationService.Navigate(pasajerosPage);
            img_bg.Effect = new System.Windows.Media.Effects.BlurEffect()
            {
                Radius = 15
            };

        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
