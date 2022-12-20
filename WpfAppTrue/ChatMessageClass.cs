using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppTrue
{
    public partial class ChatMessageClass
    {
        public ChatMessageClass( int? IdEmplyee, int? IdChatRoom, string TextMessage, DateTime? DateTime)
        {
         
            this.idEmplyee = IdEmplyee;
            this.idChatRoom = IdChatRoom;
            this.textMessage = TextMessage;
            this.dateTime = DateTime;
            
        }

        public int id { get; set; }
        public Nullable<int> idEmplyee { get; set; }
        public Nullable<int> idChatRoom { get; set; }
        public string textMessage { get; set; }
        public Nullable<System.DateTime> dateTime { get; set; }
        public string takeMessage { get; set; }
    }
}
