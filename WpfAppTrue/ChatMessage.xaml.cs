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
using static System.Net.WebRequestMethods;

namespace WpfAppTrue
{
    /// <summary>
    /// Логика взаимодействия для ChatMessage.xaml
    /// </summary>
    public partial class ChatMessage : Window
    {
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
    }
}
