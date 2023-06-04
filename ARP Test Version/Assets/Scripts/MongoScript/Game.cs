using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using UnityEngine.SceneManagement;
using ARProject.GamesPlayed;

namespace ARProject.Game
{
    class Game
    {
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public ObjectId _id { get; set; }
        public string ContentGame { get; set; }
        public string DescGame { get; set; }
        public string[] DifficultyGame { get; set; }
        public string[] ScoresGame { get; set; }
        public bool TimerGame { get; set; }

        private MongoClient _client;

        public Game()
        {
            _client = MongoDBManager.GetClient();
        }

        //Establece la conexion con la base de datos.
        public IMongoCollection<Game> GetCollection()
        {
            var db = _client.GetDatabase("Mercurio");
            return db.GetCollection<Game>("Game");
        }

        public List<Game> ReadGame(ObjectId IdGame)
        {
            Debug.Log("Leyendo en Game");
            IMongoCollection<Game> docRef = GetCollection();
            List<Game> credential = docRef.Find(game => game._id == IdGame).ToList();
            Debug.Log(string.Format("Game - DescGame: {0}", credential[0].DescGame));
            return credential;
        }

        public List<Game> ReadGame(List<GamesPlayed.GamesPlayed> gameList)
        {
            Debug.Log("Leyendo en Game");
            IMongoCollection<Game> docRef = GetCollection();
            List<Game> result = new List<Game>();
            foreach (var index in gameList)
            {
                Game credential = docRef.Find(game => game._id == index.Game).ToList()[0];
                result.Add(credential);
            }
            
            Debug.Log(string.Format("Game - DescGame: {0}", result[0].DescGame));
            
            return result;
        }

    }
}
