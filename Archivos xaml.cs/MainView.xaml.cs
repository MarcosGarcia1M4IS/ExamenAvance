using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.View
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        public void AbrirInventario_Click(object sender, RoutedEventArgs e)
        {
            // Crea una instancia del UserControl que deseas mostrar en el ContentControl
            StockView Stv = new StockView();
            // Asigna la instancia del UserControl al ContentControl
            contentControl.Content = Stv;

        }

        private void EntradasButton_Click(object sender, RoutedEventArgs e)
        {
            // Cierra el StockView
            contentControl.Content = null;

            // Muestra el DashView (crea una instancia y asígnala al ContentControl)
            EntradasView Env = new EntradasView();
            contentControl.Content = Env;
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            // Cierra el StockView
            contentControl.Content = null;


            // Muestra el DashView (crea una instancia y asígnala al ContentControl)
            DashboardView dashView = new DashboardView();
            contentControl.Content = dashView;
        }
    }
}
