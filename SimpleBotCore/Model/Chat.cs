using System;

namespace SimpleBotCore.Model
{
    public class Chat
    {
        public string User { get; set; }
        public string Message { get; set; }
        public DateTime Create { get; set; } = DateTime.Now;
    }
}
