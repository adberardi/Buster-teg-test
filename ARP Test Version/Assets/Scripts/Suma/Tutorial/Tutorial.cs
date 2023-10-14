﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;


namespace ARProject.Tutorial
{
    class Tutorial
    {
        public ObjectId _id { get; set; }
        public string DescriptionGame { get; set; }
        public ObjectId IdGame { get; set; }
        public string TitleGame { get; set; }
        private MongoClient _client;

        public Tutorial()
        {
            _client = MongoDBManager.GetClient();
        }

        //Establece la conexion con la base de datos
        public IMongoCollection<Tutorial> GetCollection()
        {
            var db = _client.GetDatabase("Mercurio");
            return db.GetCollection<Tutorial>("Tutorial");
        }

        //Busca el tutorial y retorna la informacion a mostrar.
        public Tutorial GetGameTutorial(string titleGame)
        {
            IMongoCollection<Tutorial> docRef = GetCollection();
            Tutorial data = docRef.Find(task => task.TitleGame == titleGame).ToList()[0];
            return data;
        }

    }
}