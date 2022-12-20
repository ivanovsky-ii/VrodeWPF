using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace WpfAppTrue
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static HttpClient httpClient = new HttpClient();
        public static Employee employee;

        public MainWindow()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(Properties.Settings.Default.Login) && !string.IsNullOrEmpty(Properties.Settings.Default.Password))
            {
                Enter();
            }
        }

        public void Enter()
        {
            loginTb.Text = Properties.Settings.Default.Login;

            passwordPb.Text = Properties.Settings.Default.Password;
        }

        private async void SignIn(object sender, RoutedEventArgs s)
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var contente = new UserData { password = passwordPb.Text, username = loginTb.Text };

            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(contente), Encoding.UTF8, "application/json");

            HttpResponseMessage message = await httpClient.PostAsync("http://localhost:55595/api/Login", httpContent);

            if (message.IsSuccessStatusCode)
            {
                var curContent = await message.Content.ReadAsStringAsync();
                employee = JsonConvert.DeserializeObject<Employee>(curContent);
                if ((bool)rememberChb.IsChecked)
                {
                    Properties.Settings.Default.Login = loginTb.Text;
                    Properties.Settings.Default.Password = passwordPb.Text;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.Login = string.Empty;
                    Properties.Settings.Default.Password = string.Empty;
                    Properties.Settings.Default.Save();
                }

                Main main = new Main();
                main.Show();
                Close();
            }

            else
            {
                MessageBox.Show("ПА БАШКЕ СЕБЕ ПАСТУЧИ");
            }
        }

        public class UserData
        {
            public string username { get; set; }
            public string password { get; set; }
        }
    }
}
