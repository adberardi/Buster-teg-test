using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using UnityEngine;

namespace ARProject.Reward
{
    class Reward
    {
        public ObjectId _id { get; set; }
       
        public string DescReward { get; set; }
        private MongoClient _client;



        public Reward( string descReward)
        {
            DescReward = descReward;        
        }

        public Reward()
        {
            _client = MongoDBManager.GetClient();
        }
        public IMongoCollection<Reward> GetCollection()
        {
            var db = _client.GetDatabase("Mercurio");
            return db.GetCollection<Reward>("Rewards");
        }

        public void Insert()
        {
            IMongoCollection<Reward> collection = GetCollection();
            collection.InsertOne(this);
        }

        public List<Reward> FindAll()
        {
            IMongoCollection<Reward> collection = GetCollection();
            return collection.Find(new BsonDocument()).ToList();
        }

        public Reward FindById(ObjectId id)
        {
            IMongoCollection<Reward> collection = GetCollection();
            return collection.Find(Reward => Reward._id == id).FirstOrDefault();
        }

        public void Update()
        {
            IMongoCollection<Reward> collection = GetCollection();
            collection.ReplaceOne(Reward => Reward._id == this._id, this);
        }

        public void Delete()
        {
            IMongoCollection<Reward> collection = GetCollection();
            collection.DeleteOne(Reward => Reward._id == this._id);
        }
    }
}
