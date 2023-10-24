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
        public ObjectId IdUser { get; set; }
        public int Note { get; set; }
        // TODO: Por agregar clase Content
        public ObjectId Program { get; set; }
        public ObjectId Group { get; set; }
        private MongoClient _client;

        public Score()
        {
            _client = MongoDBManager.GetClient();
        }

        public Score(ObjectId idUser, int note, ObjectId programId)
        {
            IdUser = idUser;
            Note = note;
            Program = programId;
        }

        public Score(ObjectId idUser, int note, ObjectId programId, ObjectId groupId)
        {
            IdUser = idUser;
            Note = note;
            Program = programId;
            Group = groupId;
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
            Group = credential.Group;
            Debug.Log("Read all data from the users collection.");
         }

        public void GetScore(string idUser, string idGroup)
        {
            Debug.Log("ENTRANDO EN READUSER");
            IMongoCollection<Score> docRef = GetCollection();
            //IMongoCollection<User> userCollection = GetCollection();
            Score credential = docRef.Find(score => score.IdUser == ObjectId.Parse(idUser) && score.Group == ObjectId.Parse(idGroup)).ToList()[0];
            Debug.Log(string.Format("IdUser: {0} , Note: {1}, Program ID: {2}", credential.IdUser, credential.Note, credential.Program));

            Debug.Log("Read all data from the scores collection.");
        }


        //Returns a List with the total scores by the one user from the activities assigned
        public List<Score> GetListScoreByGroup(string idUser, string idGroup)
        {
            Debug.Log("ENTRANDO EN GetListScoreByGroup");
            IMongoCollection<Score> docRef = GetCollection();
            List<Score> result = docRef.Find(score => score.IdUser == ObjectId.Parse(idUser) && score.Group == ObjectId.Parse(idGroup)).ToList();
            return result;
        }

        //Returns a List with the total scores by the one user from the lats seven's days
        public List<Score> GetListScoreByUser(string idUser, string idGroup)
        {
            Debug.Log("ENTRANDO EN GetListScoreByGroup");
            IMongoCollection<Score> docRef = GetCollection();
            List<Score> result = docRef.Find(score => score.IdUser == ObjectId.Parse(idUser) && score.Group == ObjectId.Parse(idGroup)).ToList();
            return result;
        }

        // Updates the Final Grade obtained in a Group.
        public async void UpdateScoreNote(string idUser, string idGroup, int newNote)
        {
            try
            {
                IMongoCollection<Score> docRef = GetCollection();
                var filterData = Builders<Score>.Filter.Eq(query => query.IdUser, ObjectId.Parse(idUser));
                filterData = Builders<Score>.Filter.Eq(query => query.Group, ObjectId.Parse(idGroup));
                var dataToUpdate = Builders<Score>.Update.Set(query => query.Note, newNote);
                IMongoCollection<Score> groupRef = GetCollection();
                var result = await groupRef.UpdateOneAsync(filterData, dataToUpdate);
                if (result.IsAcknowledged && result.ModifiedCount > 0)
                {
                    Debug.Log("Operacion completada");
                }
                else
                {
                    Debug.Log("Operacion Fallida");
                }
            }
            catch (MongoException)
            {
                Debug.Log("Un error ha ocurido");
            }
        }
    }
}

