﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using UnityEngine.SceneManagement;

namespace ARPoject.School
{
    class School
    {
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public ObjectId _id { get; set; }
        public string SchoolName { get; set; }
        private List<School> ListSchools {get; set;}
        private MongoClient _client;

        public School()
        {
            _client = MongoDBManager.GetClient();
        }

        //Establece la conexion con la base de datos
        public IMongoCollection<School> GetCollection()
        {
            var db = _client.GetDatabase("Mercurio");
            return db.GetCollection<School>("School");
        }


        public void ReadSchool(string IdSchool)
        {
            Debug.Log("ENTRANDO EN ReadSchool");
            IMongoCollection<School> docRef = GetCollection();
            School credential = docRef.Find(task => task._id == ObjectId.Parse(IdSchool)).ToList()[0];
            Debug.Log(string.Format("SchoolName: {0}", credential.SchoolName));
            SchoolName = credential.SchoolName;
            Debug.Log("Read all data from the School collection.");
        }

        public List<School> GetListSchools()
        {
            return ListSchools;
        }

        public async void GetAllSchools()
        {

        }
    }
}