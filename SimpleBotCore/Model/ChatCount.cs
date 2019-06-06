using MongoDB.Bson.Serialization.Attributes;

namespace SimpleBotCore.Model
{
    [BsonIgnoreExtraElements]
    public class ChatCount
    {
        public string User { get; set; }
        public int Total { get; set; }
    }
}
