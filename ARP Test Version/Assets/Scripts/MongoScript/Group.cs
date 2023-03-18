using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ARProject.Group
{
    class Group
    {
        
        private int IdGroup { get; set; }
        private string NombreGroup { get; set; }
        private DateTime FechaCreacion { get; set; }
        private string Admin { get; set; }
        // TODO: Por agregar clases Estudiante y Tarea
        private List<object> Participante { get; set; }
        private List<object> Asignacion { get; set; }

        private string FinalTimer { get; set; }


        /*public Group(FirebaseFirestore db)
        {
            this.db = db;
        }*/


        /*public void SaveGroup(string nameGroup)
        {
            DocumentReference docRef = db.Collection("Groups").Document(nameGroup);
            Dictionary<string, object> group = new Dictionary<string, object>
            {
                { "admin", "8nkf5pBPwFcRhUjShQmnwmlPlyE3" },
                { "name", "Grupo 1er grado teresiano" },
                { "assignedActivities", new List<object>() { "Task/PrimeraTarea" } },
                { "dateCreated",  DateTime.Now.ToString()},
                { "participantsGroup", new List<object>() {"b72406d79978ea67095af8c75c24eac0","8nkf5pBPwFcRhUjShQmnwmlPlyE3", "8f128e1e2473092b221b2a89030b817e" } },

            };

            docRef.SetAsync(group).ContinueWithOnMainThread(task =>
            {
                Debug.Log("Se registro de manera exitosa el grupo");
            });

            CreateGroupGamePlayed(nameGroup);
        }*/

        /*public void ReadGroup()
        {
                DocumentReference docRef = db.Collection("Groups").Document(new User.User().GetSessionDataUser());
                docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
                {
                    if (task.IsCanceled)
                    {
                        Debug.Log(" La operaion ha sido cancelada");
                    }
                    else
                    {
                        DocumentSnapshot doc = task.Result;
                        Dictionary<string, object> docGroup = doc.ToDictionary();
                        //Debug.Log(string.Format("> Leyendo Grupo: Admin {0} | Name {1} | Participantes {2}", docGroup["admin"], docGroup["name"], docGroup["participantsGroup"].GetType()));

                        Debug.Log(string.Format("> Leyendo Fecha Creacion: {0}", docGroup["dateCreated"].GetType()));

                        NombreGroup = docGroup["name"].ToString();
                        Admin = docGroup["admin"].ToString();
                        FechaCreacion = DateTime.Parse(docGroup["dateCreated"].ToString());
                        Asignacion = (List<object>)docGroup["assignedActivities"];
                        Participante = (List<object>)docGroup["participantsGroup"];
                        Debug.Log(string.Format("=> Longitud Asignacion: {0}", Participante.Count));
                        Debug.Log(string.Format("> Leyendo Grupo: Admin {0} | Name {1} | Participantes {2} | FechaCreacion {3} | Asignacion {4}", Admin, NombreGroup, Participante.Count, FechaCreacion, Asignacion.Count));
                    }

                });
            }*/

        /*public void UpdateNameGroup ()
        {
            DocumentReference docRef = db.Collection("Groups").Document(new User.User().GetSessionDataUser());
            Dictionary<string, object> groupUpdate = new Dictionary<string, object>
            {
                { "name", "Nuevo Nombre Grupo"}
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



