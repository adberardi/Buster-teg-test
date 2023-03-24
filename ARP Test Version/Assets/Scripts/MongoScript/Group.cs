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
        // TODO: Por agregar clases Estudiante y Tarea
        //private List<object> ParticipantsGroup { get; set; }
        public string[] ParticipantsGroup { get; set; }
        public string[] AssignedActivities { get; set; }

        public string FinalTimer { get; set; }


        private MongoClient _client;

        public Group()
        {
            _client = MongoDBManager.GetClient();
        }

        public Group(string nameGroup, DateTime dateCreated, string admin)
        {
            NameGroup = nameGroup;
            DateCreated = dateCreated;
            Admin = admin;
            ParticipantsGroup = new string[] { };
            AssignedActivities = new string[] { };
            FinalTimer = "00:00:00";
        }

        public IMongoCollection<Group> GetCollection()
        {
            var db = _client.GetDatabase("Mercurio");
            return db.GetCollection<Group>("Groups");
        }


        public void CreateGroup(Group newGroup)
        {
            Group registerGroup = new Group();
            Debug.LogFormat("Firebase user created successfully: {0}", newGroup.Admin);
            registerGroup.GetCollection().InsertOne(newGroup);
        }

        public void ReadGroup(string IdGroup)
        {
            IMongoCollection<Group> docRef = GetCollection();
            Group credential = docRef.Find(group => group._id.ToString() == IdGroup).ToList()[0];         
            NameGroup = credential.NameGroup;
            Admin = credential.Admin;
            DateCreated = credential.DateCreated;
            AssignedActivities = credential.AssignedActivities;
            ParticipantsGroup = credential.ParticipantsGroup;
            //Debug.Log(string.Format("=> Longitud AssignedActivities: {0}", ParticipantsGroup.Count));
            //Debug.Log(string.Format("> Leyendo Grupo: Admin {0} | Name {1} | Participantes {2} | DateCreated {3} | AssignedActivities {4}", Admin, NameGroup, ParticipantsGroup.Count, DateCreated, AssignedActivities.Count));
            }

        public async void UpdateNameGroup (string IdGroup, string newNameGroup)
        {
            try
            {
                IMongoCollection<Group> docRef = GetCollection();
                var filterData = Builders<Group>.Filter.Eq(query => query._id, ObjectId.Parse(IdGroup));
                var dataToUpdate = Builders<Group>.Update.Set(query => query.NameGroup, newNameGroup);
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

        /*public void UpdateParticipantsGroup(List<object> member)
        {
            DocumentReference docRef = db.Collection("Groups").Document(new User.User().GetSessionDataUser());
            Dictionary<string, object> groupUpdate = new Dictionary<string, object>
            {
                { "participantsGroup", member}
            };

            docRef.UpdateAsync(groupUpdate).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                    Debug.Log("Atualizacion de nombre para grupo completada");
                else if (task.IsCanceled)
                {
                    Debug.Log("La atualizacion fue cancelada");
                }
                else
                {
                    Debug.Log("La atualizacion fue interrumpida");
                }
            });
        }*/

        public void SendEmailNotification(ArrayList member)
        {

        }

        /*public void CreateGroupGamePlayed(string nameGroup)
        {
            DocumentReference docRef = db.Collection("Groups").Document(new User.User().GetSessionDataUser());
            Dictionary<string, object> group = new Dictionary<string, object>
            {
                { "nameGroup", "Nuevo Grupo" },
            };

            docRef.SetAsync(group).ContinueWithOnMainThread(task =>
            {
                Debug.Log("Se registro de manera exitosa el grupo");
            });
        }*/

        /*public void SaveSubCollectionGamePlayed(string nameGame, string idPlayer)
        {
            //DocumentReference docRef = db.Collection("Scores").Document(new User.User().GetSessionDataUser()).Collection("PersonalGame").Document();
            DocumentReference docRef = db.Collection("GamesPlayedGroup").Document().Collection(nameGame).Document(idPlayer);
            Dictionary<string, object> subcoll = new Dictionary<string, object>
            {
                { "dayPlayed", DateTime.Now.ToString() },
                { "finalScore", 14 },
                { "finalTimer", "01:34:00" }
            };
            docRef.SetAsync(subcoll).ContinueWithOnMainThread(task =>
            {
                Debug.Log("Added data in the scores collection.");
            });
        }*/

        /*GetClassRoom: Get All participants from the Class Room */
        //public List<string> GetClassRoom(string idClassRoom, string IdUser)
        //{
        //    List<string> aux = new List<string>();
        //    DocumentReference collRef = db.Collection("Groups").Document(idClassRoom);
        //    collRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        //    {
        //        DocumentSnapshot doc = task.Result;
        //        Dictionary<string, object> p = doc.ToDictionary();
        //        aux = (List<string>) p["participantsGroup"];
        //    });
        //    Debug.Log(string.Format("Longitud List: {0}", aux.Count));
        //    return aux;
        //}

        //public void ReadSubColeccionGroupGame(string nameGame)
        //{
        //    //DocumentReference docRef = db.Collection("Scores").Document(new User.User().GetSessionDataUser());
        //    DocumentReference docRef = db.Collection("GamesPlayedGroup").Document(nameGame);
        //    Debug.Log("///VOY POR READ SCORE");
        //    docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        //    {

        //        DocumentSnapshot doc = task.Result;
        //        Dictionary<string, object> documentDic = doc.ToDictionary();
        //        Debug.Log(string.Format("user: {0}", documentDic["username"]));
        //        Debug.Log("Read all data from the scores collection.");
        //    });

        //    CollectionReference subcollRef = docRef.Collection(nameGame);
        //    subcollRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        //    {
        //        QuerySnapshot subcoll = task.Result;
        //        foreach (DocumentSnapshot sub in subcoll.Documents)
        //        {
        //            Dictionary<string, object> data = sub.ToDictionary();
        //            Debug.Log(string.Format("     _> Subcolleccion: DAYPLAYED {0} ", data["dayPlayed"]));
        //        }
        //    });


        //}

        /*public void UpdateSubColeccionGroupGame()
        {
            //DocumentReference docRef = db.Collection("Scores").Document(new User.User().GetSessionDataUser()).Collection("PersonalGame").Document();
            DocumentReference docRef = db.Collection("GamesPlayedGroup").Document("DKxpe27YWpey5VtLnD18").Collection("SumaCasa1").Document();
            Dictionary<string, object> subcoll = new Dictionary<string, object>
            {
                { "dayPlayed", DateTime.Now.ToString() },
                { "finalScore", 14 },
                { "finalTimer", "01:34:00" }
            };
            docRef.SetAsync(subcoll).ContinueWithOnMainThread(task =>
            {
                Debug.Log("Added data in the scores collection.");
            });
        }*/

        /*public Dictionary<string, object> GetRecord(string IdUser)
        {
            Dictionary<string, object> aux = new Dictionary<string, object>();
            CollectionReference collRef = db.Collection("Scores").Document(IdUser).Collection("GamesPlayed");
            collRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                QuerySnapshot query = task.Result;
                foreach (DocumentSnapshot doc in query.Documents)
                {
                    Dictionary<string, object> p = doc.ToDictionary();
                    //TODO: Completar carga de los datos.
                    /*object data = new object()
                    {
                        { "datePlayed": p["datePlayed"].ToString()};

                    };
                }
            });
            Debug.Log(string.Format("Longitud List: {0}", aux.Count));
            return aux;
        }*/
    }

    }



