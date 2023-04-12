using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using UnityEngine;

namespace ARProject.GamesPlayed
{
    class GamesPlayed
    {
        public ObjectId _id { get; set; }
     
        public string DayPlayed { get; set; }
        public int FinalScore { get; set; }
        public string  FinalTimer { get; set; }
        public ObjectId User { get; set; }
        public ObjectId Game { get; set; }

        private MongoClient _client;



        public GamesPlayed( string dayPlayed, int finalScore,string finalTimer, ObjectId user, ObjectId game)
        {
            DayPlayed = dayPlayed;
            FinalScore = finalScore;
            FinalTimer = finalTimer;
            User = user;
            Game = game;
        }

        public GamesPlayed()
        {
            _client = MongoDBManager.GetClient();
        }
        public IMongoCollection<GamesPlayed> GetCollection()
        {
            var db = _client.GetDatabase("Mercurio");
            return db.GetCollection<GamesPlayed>("GamesPlayeds");
        }

        public void Insert()
        {
            IMongoCollection<GamesPlayed> collection = GetCollection();
            collection.InsertOne(this);
        }

        public List<GamesPlayed> FindAll()
        {
            IMongoCollection<GamesPlayed> collection = GetCollection();
            return collection.Find(new BsonDocument()).ToList();
        }

        public GamesPlayed FindById(ObjectId id)
        {
            IMongoCollection<GamesPlayed> collection = GetCollection();
            return collection.Find(GamesPlayed => GamesPlayed._id == id).FirstOrDefault();
        }

        public void Update()
        {
            IMongoCollection<GamesPlayed> collection = GetCollection();
            collection.ReplaceOne(GamesPlayed => GamesPlayed._id == this._id, this);
        }

        public void Delete()
        {
            IMongoCollection<GamesPlayed> collection = GetCollection();
            collection.DeleteOne(GamesPlayed => GamesPlayed._id == this._id);
        }
    }
}
