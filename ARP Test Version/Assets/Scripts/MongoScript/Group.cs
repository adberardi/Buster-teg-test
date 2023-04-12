using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using UnityEngine;

namespace ARProject.Group
{
    class Group
    {
        public ObjectId _id { get; set; }
        public List<string> AssignedActivities { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }
        public List<string> ParticipantsGroup { get; set; }
        private MongoClient _client;

   

        public Group(List<string> assignedActivities, string dateCreated, string dateUpdated, List<string> participantsGroup)
        {
            AssignedActivities = assignedActivities;
            DateCreated = dateCreated;
            DateUpdated = dateUpdated;
            ParticipantsGroup = participantsGroup; 
        }

        public Group()
        {
            _client = MongoDBManager.GetClient();
        }
        public IMongoCollection<Group> GetCollection()
        {
            var db = _client.GetDatabase("Mercurio");
            return db.GetCollection<Group>("Groups");
        }

        public void Insert()
        {
            IMongoCollection<Group> collection = GetCollection();
            collection.InsertOne(this);
        }

        public  List<Group> FindAll()
        {
            IMongoCollection<Group> collection = GetCollection();
            return collection.Find(new BsonDocument()).ToList();
        }

        public  Group FindById(ObjectId id)
        {
            IMongoCollection<Group> collection = GetCollection();
            return collection.Find(group => group._id == id).FirstOrDefault();
        }

        public void Update()
        {
            IMongoCollection<Group> collection = GetCollection();
            collection.ReplaceOne(group => group._id == this._id, this);
        }

        public void Delete()
        {
            IMongoCollection<Group> collection = GetCollection();
            collection.DeleteOne(group => group._id == this._id);
        }
    }
}
