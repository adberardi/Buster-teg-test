using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using UnityEngine;

namespace ARProject.Content
{
    class Content
    {
        public ObjectId _id { get; set; }
      
        public string DescContent { get; set; }
        public string GradeContent { get; set; }
        public string Level { get; set; }
        public string TitleContent { get; set; }
        public string NameContent { get; set; }
        private MongoClient _client;



        public Content( string descContent, string gradeContent,string level, string titleContent,string nameContent)
        {
            DescContent = descContent;
            GradeContent = gradeContent;
            Level = level;
            TitleContent = titleContent;
            NameContent = nameContent;
        }

        public Content()
        {
            _client = MongoDBManager.GetClient();
        }
        public IMongoCollection<Content> GetCollection()
        {
            var db = _client.GetDatabase("Mercurio");
            return db.GetCollection<Content>("Contents");
        }

        public void Insert()
        {
            IMongoCollection<Content> collection = GetCollection();
            collection.InsertOne(this);
        }

        public List<Content> FindAll()
        {
            IMongoCollection<Content> collection = GetCollection();
            return collection.Find(new BsonDocument()).ToList();
        }

        public Content FindById(ObjectId id)
        {
            IMongoCollection<Content> collection = GetCollection();
            return collection.Find(Content => Content._id == id).FirstOrDefault();
        }

        public void Update()
        {
            IMongoCollection<Content> collection = GetCollection();
            collection.ReplaceOne(Content => Content._id == this._id, this);
        }

        public void Delete()
        {
            IMongoCollection<Content> collection = GetCollection();
            collection.DeleteOne(Content => Content._id == this._id);
        }
    }
}
