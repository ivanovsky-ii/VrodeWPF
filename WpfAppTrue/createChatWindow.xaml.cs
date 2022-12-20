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
using System.Windows.Shapes;

namespace WpfAppTrue
{
    /// <summary>
    /// Логика взаимодействия для createChatWindow.xaml
    /// </summary>
    public partial class createChatWindow : Window
    {
       HttpClient httpClient = new HttpClient();
       
        public createChatWindow()
        {
            InitializeComponent();
        }

        private async void createNewChatBtn_Click(object sender, RoutedEventArgs e)
        {
            string chatName = newChatNameTbl.Text;
            SimpleChatroom simach = new SimpleChatroom(8, chatName);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(simach), Encoding.UTF8, ("application/json"));
            HttpResponseMessage mes = await httpClient.PostAsync("http://localhost:55595/api/CreateNewChatroom", (httpContent));

            MessageBox.Show(mes.StatusCode.ToString());
            if (mes.StatusCode == System.Net.HttpStatusCode.OK)
            {
                HttpContent hleb = new StringContent(JsonConvert.SerializeObject(mes), Encoding.UTF8, ("application/json"));
                HttpResponseMessage messem = await httpClient.PostAsync($"http://localhost:55595/api/AREC/{MainWindow.employee.id}", (mes.Content));
                Main m = new Main();
                m.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Абшибка ☻");
            }
        }

        public async void CreateNewChatRoomEmployee()
        {

        }

        public class SimpleChatroom
        {
            public SimpleChatroom(int Id, string topic)
            {
                id = Id;
                Topic = topic;

            }
            public int id { get; set; }
            public string Topic { get; set; }
        }

        private void backToMainBtn_Click(object sender, RoutedEventArgs e)
        {
            Main m = new Main();
            m.Show();
            this.Close();
        }
    }
}
