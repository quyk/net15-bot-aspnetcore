using MongoDB.Driver;
using SimpleBotCore.Model;
using System.Threading.Tasks;

namespace SimpleBotCore.Infrastructure
{
    public class MongoConnection
    {
        const string Data = "xBot";
        const string ChatCollection = "Chat";
        const string ChatCountCollection = "ChatCount";

        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;

        public MongoConnection()
        {
            _mongoClient = new MongoClient();
            _mongoDatabase = _mongoClient.GetDatabase(Data);
        }

        public void Insert(Chat chat)
        {
            var collection = _mongoDatabase.GetCollection<Chat>(ChatCollection);
            collection.InsertOne(chat);
        }

        public void Update(ChatCount chatCount)
        {
            var filter = Builders<ChatCount>.Filter.Eq("User", chatCount.User);
            var updateDefinition = Builders<ChatCount>.Update
                .Set(m => m.Total, chatCount.Total);

            var updateOptions = new UpdateOptions { IsUpsert = true };

            var collection = _mongoDatabase.GetCollection<ChatCount>(ChatCountCollection);
            collection.UpdateOne(filter, updateDefinition, updateOptions);
        }

        public int CountMessage(Chat chat)
        {
            var filter = Builders<ChatCount>.Filter.Eq("User", chat.User);
            var collection = _mongoDatabase.GetCollection<ChatCount>(ChatCountCollection);
            var count = collection.Find(filter).SingleOrDefault<ChatCount>();
            return count?.Total ?? 0;
        }
    }
}
