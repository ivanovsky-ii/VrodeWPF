using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
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
using static System.Net.WebRequestMethods;

namespace WpfAppTrue
{
    /// <summary>
    /// Логика взаимодействия для ChatMessage.xaml
    /// </summary>
    public partial class ChatMessage : Window
    {
        HttpClient httpClient = new HttpClient();


        List<ChatMessageClass> chatMessages = new List<ChatMessageClass>();
        public ChatMessage()
        {
            InitializeComponent();
            Title = Main.chatRoom.Topic;



            GetMessage();
        }

        public async void GetMessage()
        {
            HttpResponseMessage message = await MainWindow.httpClient.GetAsync("http://localhost:55595/api/ChatMessages");
            var content = await message.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ChatMessageClass>>(content).ToList();

            messageList.ItemsSource = result.Where(i => i.idChatRoom == Main.chatRoom.id);
        }

        private async void SendMessage(object sender, RoutedEventArgs e)
        {
            string TMFmessage = TheMutherFuckerMessageSukaBlyatYaLubluPutinaTbx.Text;
            SimpleChatMessage chatMessageClass = new SimpleChatMessage(1, MainWindow.employee.id, Main.chatRoom.id, TMFmessage, System.DateTime.Now);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(chatMessageClass), Encoding.UTF8, "application/json");

            HttpResponseMessage message = await httpClient.PostAsync("http://localhost:55595/api/WTMFmessage", httpContent);

            MessageBox.Show(message.StatusCode.ToString());
            if (message.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Main m = new Main();
                m.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Абшибка ☻");
            }
        }
    }
    public class SimpleChatMessage
    {
        public SimpleChatMessage(int id, int? idEmplyee, int? idChatRoom, string textMessage, DateTime date)
        {
            this.id = id;
            this.idEmplyee = idEmplyee;
            this.idChatRoom = idChatRoom;
            this.textMessage = textMessage;
            this.dateTime = date;
        }

        public int id { get; set; }
        public Nullable<int> idEmplyee { get; set; }
        public Nullable<int> idChatRoom { get; set; }
        public string textMessage { get; set; }
        public Nullable<System.DateTime> dateTime { get; set; }

    }
}
