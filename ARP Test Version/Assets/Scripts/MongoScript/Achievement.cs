using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using UnityEngine;

namespace ARProject.Achievement
{
    class Achievement
    {
        public ObjectId _id { get; set; }
        public List<string> AssignedActivities { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }
        public List<string> ParticipantsAchievement { get; set; }
        private MongoClient _client;



        public Achievement(List<string> assignedActivities, string dateCreated, string dateUpdated, List<string> participantsAchievement)
        {
            AssignedActivities = assignedActivities;
            DateCreated = dateCreated;
            DateUpdated = dateUpdated;
            ParticipantsAchievement = participantsAchievement;
        }

        public Achievement()
        {
            _client = MongoDBManager.GetClient();
        }
        public IMongoCollection<Achievement> GetCollection()
        {
            var db = _client.GetDatabase("Mercurio");
            return db.GetCollection<Achievement>("Achievements");
        }

        public void Insert()
        {
            IMongoCollection<Achievement> collection = GetCollection();
            collection.InsertOne(this);
        }

        public List<Achievement> FindAll()
        {
            IMongoCollection<Achievement> collection = GetCollection();
            return collection.Find(new BsonDocument()).ToList();
        }

        public Achievement FindById(ObjectId id)
        {
            IMongoCollection<Achievement> collection = GetCollection();
            return collection.Find(Achievement => Achievement._id == id).FirstOrDefault();
        }

        public void Update()
        {
            IMongoCollection<Achievement> collection = GetCollection();
            collection.ReplaceOne(Achievement => Achievement._id == this._id, this);
        }

        public void Delete()
        {
            IMongoCollection<Achievement> collection = GetCollection();
            collection.DeleteOne(Achievement => Achievement._id == this._id);
        }
    }
}
