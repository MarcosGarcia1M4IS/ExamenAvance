using System;
using System.Collections.Generic;
using System.Data;
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
using MySql.Data.MySqlClient;
using WpfApp1.View;

namespace WpfApp1.View
{
    public partial class StockView : UserControl
    {
        public StockView()
        {
            InitializeComponent();

            // Llamar a un método para cargar los datos desde la base de datos
            LoadDataFromDatabase();
        }


        private void LoadDataFromDatabase()
        {
            // Cadena de conexión a la base de datos MySQL
            string connectionString = "Server=192.168.43.171;Port=3306;Database=inventario;User=user;Password=Notoxico1591Hello>Hi";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Consulta SQL para recuperar los datos
                    string query = "SELECT Item, Codigo, Nombre, Precio_Unitario, Cantidad, Precio_Total FROM mercancias;";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // Crear un DataTable para almacenar los datos
                            var dataTable = new System.Data.DataTable();
                            dataTable.Load(reader);

                            // Asignar el DataTable como origen de datos para el DataGrid
                            dataGrid.ItemsSource = dataTable.DefaultView;
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    // Manejar excepciones si la conexión o la consulta SQL fallan
                    MessageBox.Show("Error de MySQL: " + ex.Message);
                }
            }
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtBuscar.Text.Trim(); // Obtener el texto del TextBox y eliminar espacios en blanco al principio y al final

            // Realizar una búsqueda en la base de datos utilizando el texto de búsqueda
            // Por ejemplo, puedes modificar tu consulta SQL para filtrar los resultados en función del texto de búsqueda

            // Cadena de conexión a la base de datos MySQL
            string connectionString = "Server=192.168.43.171;Port=3306;Database=inventario;User=user;Password=Notoxico1591Hello>Hi";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Consulta SQL para recuperar los datos filtrados por el texto de búsqueda
                    string query = "SELECT Item, Codigo, Nombre, Precio_Unitario, Cantidad, Precio_Total FROM mercancias WHERE Nombre LIKE @SearchText;";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Usar parámetros para evitar la inyección de SQL
                        command.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // Crear un DataTable para almacenar los datos filtrados
                            var dataTable = new System.Data.DataTable();
                            dataTable.Load(reader);

                            // Asignar el DataTable como origen de datos para el DataGrid
                            dataGrid.ItemsSource = dataTable.DefaultView;

                           txtNoExiste.Visibility = dataTable.Rows.Count == 0 ? Visibility.Visible : Visibility.Hidden;
                            dataGrid.Visibility = dataTable.Rows.Count == 0 ? Visibility.Hidden : Visibility.Visible;


                        }
                    }
                }
                catch (MySqlException ex)
                {
                    // Manejar excepciones si la conexión o la consulta SQL fallan
                    MessageBox.Show("Error de MySQL: " + ex.Message);
                }
            }

            // Mostrar o ocultar el mensaje de "El producto no existe" en función de si se encontraron resultados
            if (dataGrid.Items.IsEmpty)
            {
                txtNoExiste.Visibility = Visibility.Visible;
            }
            else
            {
                txtNoExiste.Visibility = Visibility.Collapsed;
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Aquí puedes agregar el código para manejar la selección de filas
            // Por ejemplo, puedes obtener la fila seleccionada usando dataGrid.SelectedItem
            // y realizar acciones en función de la fila seleccionada.
        }

        private void Agregar_Inventario_Click(object sender, RoutedEventArgs e)
        {
           
            //Cierra el dataGrid
            dataGrid.Visibility = Visibility.Collapsed;
            //Abre la vista de agregar inventario



        }

        private void Eliminar_Producto_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}