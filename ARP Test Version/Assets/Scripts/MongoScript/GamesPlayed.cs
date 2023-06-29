using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
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
        public ObjectId Group { get; set; }
        private MongoClient _client;



        public GamesPlayed( string dayPlayed, int finalScore,string finalTimer, ObjectId user, ObjectId game, ObjectId group)
        {
            DayPlayed = dayPlayed;
            FinalScore = finalScore;
            FinalTimer = finalTimer;
            User = user;
            Game = game;
            Group = group;
        }

        public GamesPlayed()
        {
            _client = MongoDBManager.GetClient();
        }
        public IMongoCollection<GamesPlayed> GetCollection()
        {
            var db = _client.GetDatabase("Mercurio");
            return db.GetCollection<GamesPlayed>("GamesPlayed");
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
        public (List<string>, List<int>) GetGamesPlayedByUserId(ObjectId userId)
        {
            IMongoCollection<GamesPlayed> collection = GetCollection();
            List<GamesPlayed> gamesPlayed = collection.Find(gp => gp.User.Equals(userId)).ToList();
            Dictionary<string, int> dayPlayedCount = new Dictionary<string, int>();
            Debug.Log("ddddddddddd");

            foreach (GamesPlayed game in gamesPlayed)
            {
                string dayPlayed = game.DayPlayed;
                if (dayPlayedCount.ContainsKey(dayPlayed))
                {
                    dayPlayedCount[dayPlayed]++;
                }
                else
                {
                    dayPlayedCount[dayPlayed] = 1;
                }
            }

            List<string> dayPlayedList = dayPlayedCount.Keys.ToList();
            List<int> dayPlayedCountList = dayPlayedCount.Values.ToList();

            return (dayPlayedList, dayPlayedCountList);
        }


    }
}
