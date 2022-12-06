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
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public List<ChatRoom> chatRooms = new List<ChatRoom>();
        public List<ChatRoomEmployee> chatRoomEmployees = new List<ChatRoomEmployee>();
        public static ChatRoom chatRoom;
        public static ChatRoomEmployee chatEmployee;

        public Main()
        {
            InitializeComponent();
            GridLooad();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        public async void GridLooad()
        {
            //HttpResponseMessage employee = await MainWindow.httpClient.GetAsync("http://localhost:55595/api/Emplyees");
            //var emplContent = await employee.Content.ReadAsStringAsync();

            HttpResponseMessage chatRooms = await MainWindow.httpClient.GetAsync("http://localhost:55595/api/Chatrooms");
            var roomContent = await chatRooms.Content.ReadAsStringAsync();
            HttpResponseMessage chatEmployee = await MainWindow.httpClient.GetAsync("http://localhost:55595/api/ChatRoomEmploees");
            var chatEmpContent = await chatEmployee.Content.ReadAsStringAsync();   


            var result = JsonConvert.DeserializeObject<List<ChatRoomEmployee>>(chatEmpContent).Where(i => i.idEmplyee == MainWindow.employee.id).ToList();

            if (result == null)
            {
                MessageBox.Show("!У ВАС НЕТ НИОДНОГО ЧАТА!");
            }
            else
            {
                var rooms = JsonConvert.DeserializeObject<List<ChatRoom>>(roomContent).ToList();
                chatList.ItemsSource = from r in rooms
                                       join rez in result on r.id equals rez.idChatRoom
                                       select r;
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            chatRoom = chatList.SelectedItem as ChatRoom;
            ChatMessage m = new ChatMessage();
            m.Show();
            Close();
        }
    }
}
