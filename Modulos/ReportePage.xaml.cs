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
    /// Lógica de interacción para ReportePage.xaml
    /// </summary>
    public partial class ReportePage : Page
    {
        public ReportePage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Reportes.ReportPasajeros obj = new Reportes.ReportPasajeros();
            obj.Load("@ReportPasajeros");
            viewer.ViewerCore.ReportSource = obj;            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Reportes.ReportVuelos obj = new Reportes.ReportVuelos();
            obj.Load("@ReportVuelos");
            viewer.ViewerCore.ReportSource = obj;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Reportes.ReportReservacion obj = new Reportes.ReportReservacion();
            obj.Load("@ReportReservacion");
            viewer.ViewerCore.ReportSource = obj;
        }
    }
}
