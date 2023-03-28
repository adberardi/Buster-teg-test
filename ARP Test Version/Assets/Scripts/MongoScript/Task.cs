﻿using System.Collections;
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

        public string GroupTask { get; set; }
        public string ContentTask { get; set; }
        public int PointsTask { get; set; }
        public float PercentageTask { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        private MongoClient _client;

        public Task()
        {
            _client = MongoDBManager.GetClient();
        }

        public Task(string contentTask, string startDate, string endDate, string groupTask, float percentageTask, int pointTask)
        {
            ContentTask = contentTask;
            StartDate = DateTime.Parse(startDate);
            EndDate= DateTime.Parse(endDate);
            GroupTask = groupTask;
            PercentageTask = percentageTask;
            PointsTask = pointTask;
        }

        public IMongoCollection<Task> GetCollection()
        {
            var db = _client.GetDatabase("Mercurio");
            return db.GetCollection<Task>("Task");
        }

        public void SaveTask(Task newTask)
        {
            Task registerTask = new Task();
            Debug.Log(" Entrando en SaveTask");
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
            Debug.Log(string.Format("ContentTask: {0} , StartDate: {1}, GroupTask: {2}", result.ContentTask, result.StartDate, result.GroupTask));
            ContentTask = result.ContentTask;
            StartDate = result.EndDate;
            GroupTask = result.GroupTask;
            PercentageTask = result.PercentageTask;
            PointsTask = result.PointsTask;
            Debug.Log("Read all data from the task collection.");
        }

        public async void UpdateTask(string IdTask, string newTask)
        {
            try
            {
                IMongoCollection<Task> docRef = GetCollection();
                var filterData = Builders<Task>.Filter.Eq(query => query._id, ObjectId.Parse(IdTask));
                var dataToUpdate = Builders<Task>.Update.Set(query => query.ContentTask, newTask);
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
    }
}


