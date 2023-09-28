using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    ///  string connectionString = "Server=127.0.0.1;Port=3306;Database=users;User=root;Password=Notoxico1591Hello>Hi";
    public partial class LoginView : Window
    {
      

        public LoginView()
        {
            InitializeComponent();

            string connectionString = "Server=192.168.43.171;Port=3306;Database=users;User=user;Password=Notoxico1591Hello>Hi";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Verificar si la conexión se ha establecido con éxito
                    if (connection.State == ConnectionState.Open)
                    {
                        MessageBox.Show("Conexión exitosa a la base de datos.");
                    }
                    else
                    {
                        MessageBox.Show("No se pudo conectar a la base de datos.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
                }
            }
        }

        private void Window_MouseDown(Object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(Object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(Object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnMaximize_Click(Object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }
        public Boolean loginExitoso = false;
        

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Configura la cadena de conexión con la dirección IP y el puerto TCP adecuados

            string connectionString = "Server=192.168.43.171;Port=3306;Database=users;User=user;Password=Notoxico1591Hello>Hi";


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Consulta SQL para verificar las credenciales del usuario (correo y contraseña)
                   string query = "SELECT * FROM usuarios WHERE correo = @correo AND contrasena = @contrasena";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@correo", txtUser.Text);
                    cmd.Parameters.AddWithValue("@contrasena", txtpassword.Password);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Las credenciales son válidas, puedes abrir la siguiente ventana o realizar otras acciones.
                        MessageBox.Show("Inicio de sesión exitoso.");
                        loginExitoso = true;

                        if (loginExitoso == true)
                        {
                            this.Hide(); // Oculta la ventana de inicio de sesión
                            var mainView = new MainView();
                            mainView.Closed += (s, args) => this.Close(); // Cierra la aplicación cuando se cierre la ventana principal
                            mainView.Show(); // Muestra la ventana principal
                        }



                    }
                    else
                    {
                        MessageBox.Show("Credenciales incorrectas. Inténtalo de nuevo.");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
                }
            }
        }
    }
}