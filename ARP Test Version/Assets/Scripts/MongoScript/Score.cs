using System;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;


namespace ARProject.Score
{
    public class Score
    {
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public ObjectId _id { get; set; }
        public string IdUser { get; set; }
        public int Note { get; set; }
        // TODO: Por agregar clase Content
        public ObjectId Program { get; set; }
        private MongoClient _client;
        private string v1;
        private int v2;
        private string v3;

        public Score()
        {
            _client = MongoDBManager.GetClient();
        }

        public Score(string idUser, int note, ObjectId programId)
        {
            IdUser = idUser;
            Note = note;
            Program = programId;
        }

        public IMongoCollection<Score> GetCollection()
        {
            var db = _client.GetDatabase("Mercurio");
            return db.GetCollection<Score>("Scores");
        }


        public void SaveScore(Score newScore)
        {
            Score registerGroup = new Score();
            Debug.LogFormat("MongoDB user created successfully: {0}", newScore._id);
            registerGroup.GetCollection().InsertOne(newScore);
        }



        public void ReadScore(string idParam)
         {
            Debug.Log("ENTRANDO EN READUSER");
            IMongoCollection<Score> docRef = GetCollection();
            //IMongoCollection<User> userCollection = GetCollection();
            Score credential = docRef.Find(task => task._id == ObjectId.Parse(idParam)).ToList()[0];
            Debug.Log(string.Format("IdUser: {0} , Note: {1}, Program ID: {2}", credential.IdUser, credential.Note, credential.Program));
            Note = credential.Note;
            Program = credential.Program;
            Debug.Log("Read all data from the users collection.");
         }

    }
}

