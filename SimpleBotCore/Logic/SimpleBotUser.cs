using SimpleBotCore.Infrastructure;
using SimpleBotCore.Model;

namespace SimpleBotCore.Logic
{
    public class SimpleBotUser
    {
        public string Reply(SimpleMessage message)
        {
            var log = new MongoConnection();

            var response = $"{message.User} disse '{message.Text}'";
            var chat = new Chat
            {
                User = message.User,
                Message = response
            };

            var count = log.CountMessage(chat);
            var total = count + 1;

            log.Insert(chat);
            log.Update(new ChatCount
            {
                User = message.User,
                Total = total
            });

            return $"{response} ({total} mensagens enviadas)";
        }

    }
}