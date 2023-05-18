using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using UnityEngine.SceneManagement;

namespace ARProject.GamesPlayed
{
    class GamesPlayed
    {
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public ObjectId _id { get; set; }
        public string DayPlayed { get; set; }
        public int FinalScore { get; set; }
        public string FinalTimer { get; set; }
        public ObjectId User { get; set; }
        public ObjectId Game { get; set; }
        public ObjectId Group { get; set; }

        private MongoClient _client;

        public GamesPlayed()
        {
            _client = MongoDBManager.GetClient();
        }

        //Establece la conexion con la base de datos.
        public IMongoCollection<GamesPlayed> GetCollection()
        {
            var db = _client.GetDatabase("Mercurio");
            return db.GetCollection<GamesPlayed>("GamesPlayed");
        }

        //Retorna una partida en especifico de un jugador determinado.
        public void ReadGamePlayed(ObjectId IdUser, ObjectId IdGame)
        {
            Debug.Log("Leyendo en GamesPlayed");
            IMongoCollection<GamesPlayed> docRef = GetCollection();
            GamesPlayed credential = docRef.Find(gp => gp.User == IdUser && gp.Game == IdGame).ToList()[0];
            Debug.Log(string.Format("GamesPlayed - DayPlayed: {0}", credential.DayPlayed));
        }

        //Retorna todas las partidas de un usuario.
        public List<GamesPlayed> ReadGamesPlayed(ObjectId IdUser)
        {
            Debug.Log("Leyendo en GamesPlayed");
            IMongoCollection<GamesPlayed> docRef = GetCollection();
            List<GamesPlayed> result = docRef.Find(gp => gp.User == IdUser).ToList();
            return result;
        }

        //Retorna todas las partidas de un grupo.
        public List<GamesPlayed> ReadGamesPlayedGroup(ObjectId IdGroup)
        {
            Debug.Log("Leyendo en GamesPlayed");
            IMongoCollection<GamesPlayed> docRef = GetCollection();
            List<GamesPlayed> result = docRef.Find(gp => gp.Group == IdGroup).ToList();
            return result;
        }

        //Retorna todas las partidas de un jugador en grupo.
        public List<GamesPlayed> ReadGamesPlayedGroup(ObjectId IdUser, ObjectId IdGroup)
        {
            Debug.Log("Leyendo en GamesPlayed");
            IMongoCollection<GamesPlayed> docRef = GetCollection();
            List<GamesPlayed> result = docRef.Find(gp => gp.User == IdUser && gp.Group == IdGroup).ToList();
            return result;
        }

        //Se registra por primera vez una partida del jugador.
        public void CreateRecord(GamesPlayed newGp)
        {
            Debug.LogFormat("Fcreated successfully: {0} ({1})", newGp.User, newGp._id);
            User = newGp.User;
            Group = newGp.Group;
            DayPlayed = newGp.DayPlayed;
            FinalScore = newGp.FinalScore;
            FinalTimer = newGp.FinalTimer;
            Game = newGp.Game;
            GamesPlayed registerRecord = new GamesPlayed();
            registerRecord.GetCollection().InsertOne(newGp);
        }

        //Se actualiza los datos de la partida del jugador.
        private async void UpdateRecord(FilterDefinition<GamesPlayed> filterData, string newDayPlayed, int newFinalScore, string newFinalTimer)
        {
            IMongoCollection<GamesPlayed> recordCollection = GetCollection();
            var dataToUpdate = Builders<GamesPlayed>.Update.Set(query => query.FinalScore, newFinalScore)
                .Set(query => query.DayPlayed, newDayPlayed)
                .Set(query => query.FinalTimer, newFinalTimer);
        var result = await recordCollection.UpdateOneAsync(filterData, dataToUpdate);
            IsSuccessfullyOperation(result);
        }

        public void ChangeScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        public bool IsSuccessfullyOperation(UpdateResult result)
        {
            if (result.IsAcknowledged && result.ModifiedCount > 0)
            {
                Debug.Log("La operacion resulto exitosa");
                ChangeScene(1);
                return true;
            }
            else
            {
                Debug.Log("La operacion fallo");
                return false;
            }
        }
    }
}
