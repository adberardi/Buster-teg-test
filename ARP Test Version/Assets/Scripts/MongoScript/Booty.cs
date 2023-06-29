using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using UnityEngine;

namespace ARProject.Booty
{
    class Booty
    {
        public ObjectId _id { get; set; }
       
        public string Image { get; set; }
        public int Price { get; set; }
        private MongoClient _client;



        public Booty( string image,int price)
        {
            Image = image;
            Price = price;
        }

        public Booty()
        {
            _client = MongoDBManager.GetClient();
        }
        public IMongoCollection<Booty> GetCollection()
        {
            var db = _client.GetDatabase("Mercurio");
            return db.GetCollection<Booty>("booty");
        }

        public void Insert()
        {
            IMongoCollection<Booty> collection = GetCollection();
            collection.InsertOne(this);
        }

        public List<Booty> FindAll()
        {
            IMongoCollection<Booty> collection = GetCollection();
            return collection.Find(new BsonDocument()).ToList();
        }

        public Booty FindById(ObjectId id)
        {
            IMongoCollection<Booty> collection = GetCollection();
            return collection.Find(Reward => Reward._id == id).FirstOrDefault();
        }

        public void Update()
        {
            IMongoCollection<Booty> collection = GetCollection();
            collection.ReplaceOne(Booty => Booty._id == this._id, this);
        }

        public void Delete()
        {
            IMongoCollection<Booty> collection = GetCollection();
            collection.DeleteOne(Reward => Reward._id == this._id);
        }

        public List<string> GetAllImages()
        {
            IMongoCollection<Booty> collection = GetCollection();
            List<Booty> booties = collection.Find(new BsonDocument()).ToList();
            List<string> images = new List<string>();

            foreach (Booty booty in booties)
            {
                images.Add(booty.Image);
            }

            return images;
        }

        public List<int> GetAllPrices()
        {
            IMongoCollection<Booty> collection = GetCollection();
            List<Booty> booties = collection.Find(new BsonDocument()).ToList();
            List<int> prices = new List<int>();

            foreach (Booty booty in booties)
            {
                prices.Add(booty.Price);
            }

            return prices;
        }
    }
}
