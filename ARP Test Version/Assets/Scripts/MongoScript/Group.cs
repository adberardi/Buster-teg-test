using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ARProject.Group
{
    class Group
    {
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public ObjectId _id { get; set; }
        public string NameGroup { get; set; }
        public DateTime DateCreated { get; set; }
        public string Admin { get; set; }
        public string School { get; set; }
        public string LevelSchool { get; set; }
        public string[] ParticipantsGroup { get; set; }
        public string[] AssignedActivities { get; set; }


        private MongoClient _client;

        public Group()
        {
            _client = MongoDBManager.GetClient();
        }

        public Group(string nameGroup, DateTime dateCreated, string admin, string school, string levelSchool)
        {
            NameGroup = nameGroup;
            DateCreated = dateCreated;
            Admin = admin;
            School = school;
            LevelSchool = levelSchool;
            ParticipantsGroup = new string[] { };
            AssignedActivities = new string[] { };
        }

        public IMongoCollection<Group> GetCollection()
        {
            var db = _client.GetDatabase("Mercurio");
            return db.GetCollection<Group>("Groups");
        }


        public ObjectId CreateGroup(Group newGroup)
        {
            Group registerGroup = new Group();
            registerGroup.GetCollection().InsertOne(newGroup);
            return new ObjectId();
        }

        // Valida si ya existe un grupo con; el mismo nombre (name), colegio (groupSchool), y grado escolar (levelSchool) del curso
        public bool ExistsSameGroup(string name, string groupSchool, string levelSchool)
        {
            IMongoCollection<Group> docRef = GetCollection();
            List<Group> result = docRef.Find(group => (group.NameGroup == name) && (group.School == groupSchool) && (group.LevelSchool == levelSchool)).ToList();
            if (result.Count > 0)
            {
                return true;
            }
            return false;
        }

        public void ReadGroup(string IdGroup)
        {
            IMongoCollection<Group> docRef = GetCollection();
            Group credential = docRef.Find(group => group._id == ObjectId.Parse(IdGroup)).ToList()[0];         
            NameGroup = credential.NameGroup;
            Admin = credential.Admin;
            School = credential.School;
            DateCreated = credential.DateCreated;
            AssignedActivities = credential.AssignedActivities;
            ParticipantsGroup = credential.ParticipantsGroup;
            Debug.Log(" Total Actividades asignadas: " + AssignedActivities.Length);
            //Debug.Log(string.Format("=> Longitud AssignedActivities: {0}", ParticipantsGroup.Count));
            //Debug.Log(string.Format("> Leyendo Grupo: Admin {0} | Name {1} | Participantes {2} | DateCreated {3} | AssignedActivities {4}", Admin, NameGroup, ParticipantsGroup.Count, DateCreated, AssignedActivities.Count));
            }

        public async void UpdateGroup (string IdGroup, string newNameGroup, string newSchool)
        {
            try
            {
                IMongoCollection<Group> docRef = GetCollection();
                var filterData = Builders<Group>.Filter.Eq(query => query._id, ObjectId.Parse(IdGroup));
                var dataToUpdate = Builders<Group>.Update.Set(query => query.NameGroup, newNameGroup)
                    .Set(query => query.School, newSchool);
                IMongoCollection<Group> groupRef = GetCollection();
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
            catch(MongoException)
            {
                Debug.Log("Un error ha ocurido");
            }
        }

        public async void DeleteGroup(string IdGroup)
        {
            try
            {
                IMongoCollection<Group> docRef = GetCollection();
                var deleteFilter = Builders<Group>.Filter.Eq("_id", ObjectId.Parse(IdGroup));
                DeleteResult result = await docRef.DeleteOneAsync(deleteFilter);

                if (result.IsAcknowledged & result.DeletedCount > 0)
                    Debug.Log("Grupo borrado exitosamente");
                else
                    Debug.Log("Error al borrar el grupo");

            }
            catch(MongoException)
            {
                Debug.Log("Error al borrar el grupo");
            }
        }

    }

    }



