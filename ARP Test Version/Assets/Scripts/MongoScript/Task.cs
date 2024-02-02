using System.Collections;
using System;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using UnityEngine;

namespace ARProject.Task
{
    class Task
    {
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public ObjectId _id { get; set; }

        public int Cont { get; set; }
        public string GameType { get; set; }
        public int Reward { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int PointTask { get; set; }
        public float PercentageTask { get; set; }
        public string EndDate { get; set; }
        public string StartDate { get; set; }
        public string Checked { get; set; }
        private MongoClient _client;

        public Task(int cont, string gameType, int reward, string name, string description, int pointTask, float percentageTask, string startDate, string endDate, string cheCked)
        {
            Cont = cont;
            GameType = gameType;
            Reward = reward;
            Name = name;
            Description = description;
            PointTask = pointTask;
            PercentageTask = percentageTask;
            StartDate = startDate;
            EndDate = endDate;
            Checked = cheCked;
            _client = MongoDBManager.GetClient();
        }

        public Task()
        {
            _client = MongoDBManager.GetClient();
        }

        public IMongoCollection<Task> GetCollection()
        {
          //  _client = new MongoClient("mongodb+srv://zilus13:canuto13@cluster0.ds89fgp.mongodb.net/?retryWrites=true&w=majority");
            var db = _client.GetDatabase("Mercurio");
            return db.GetCollection<Task>("Task");
        }

        public void SaveTask(Task newTask)
        {
            Task registerTask = new Task();
            Debug.Log(" Entrando en SaveTask: "+ newTask.StartDate);
            newTask._id = ObjectId.GenerateNewId();
            registerTask.GetCollection().InsertOne(newTask);
        }

        public async void DeleteTask(string IdTask)
        {
            try
            {
                IMongoCollection<Task> docRef = GetCollection();
                var deleteFilter = Builders<Task>.Filter.Eq("_id", ObjectId.Parse(IdTask));
                DeleteResult result = await docRef.DeleteOneAsync(deleteFilter);

                if (result.IsAcknowledged & result.DeletedCount > 0)
                    Debug.Log("Grupo borrado exitosamente");
                else
                    Debug.Log("Error al borrar el grupo");

            }
            catch (MongoException)
            {
                Debug.Log("Error al borrar el grupo");
            }
        }

        public void ReadTask(string idTask)
        {
            Debug.Log("ENTRANDO EN READTASk");
            IMongoCollection<Task> docRef = GetCollection();
            //IMongoCollection<User> userCollection = GetCollection();
            Task result = docRef.Find(task => task._id == ObjectId.Parse(idTask)).ToList()[0];
            Description = result.Description;
            StartDate = result.EndDate;
            PercentageTask = result.PercentageTask;
            PointTask = result.PointTask;
            Debug.Log("Read all data from the task collection.");
        }


        public Task GetTask(ObjectId idTask)
        {
            IMongoCollection<Task> docRef = GetCollection();
            //DateTime.ParseExact(this.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Task result = docRef.Find(task => task._id == idTask).ToList()[0];
            return result;
        }

        public async void UpdateTask(string IdTask, string newTask)
        {
            try
            {
                IMongoCollection<Task> docRef = GetCollection();
                var filterData = Builders<Task>.Filter.Eq(query => query._id, ObjectId.Parse(IdTask));
                var dataToUpdate = Builders<Task>.Update.Set(query => query.Description, newTask);
                IMongoCollection<Task> groupRef = GetCollection();
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


        public Task GetTaskById(string id)
        {
            Debug.Log("GetTaskById");
            
            IMongoCollection<Task> docRef = GetCollection();
     
            Debug.Log(docRef);
           
            Task result = docRef.Find(task => task._id == ObjectId.Parse(id)).ToList()[0];
            Debug.Log("Result: " + result.Description);
            return result;
        }


    }
}


