using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase.Firestore;
using Firebase.Extensions;

namespace ARProject.Group
{
    class Group
    {
        private int IdGroup { get; set; }
        private string NombreGroup { get; set; }
        private Timestamp FechaCreacion { get; set; }
        private string Admin { get; set; }
        // TODO: Por agregar clases Estudiante y Tarea
        private List<object> Participante { get; set; }
        private List<object> Asignacion { get; set; }

        private FirebaseFirestore db;
        private FirestoreError ErrorCode;

        public Group(FirebaseFirestore db)
        {
            this.db = db;
        }


        public void SaveGroup()
        {
            DocumentReference docRef = db.Collection("Groups").Document(new User.User().GetSessionDataUser());
            Dictionary<string, object> group = new Dictionary<string, object>
            {
                { "admin", "8nkf5pBPwFcRhUjShQmnwmlPlyE3" },
                { "name", "Grupo 1er grado teresiano" },
                { "assignedActivities", new List<object>() { "Task/PrimeraTarea" } },
                { "dateCreated", DateTime.Now },
                { "participantsGroup", new List<object>() {"b72406d79978ea67095af8c75c24eac0","8nkf5pBPwFcRhUjShQmnwmlPlyE3", "8f128e1e2473092b221b2a89030b817e" } },

            };

            docRef.SetAsync(group).ContinueWithOnMainThread(task =>
            {
                Debug.Log("Se registro de manera exitosa el grupo");
            });
        }

        public void ReadGroup()
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
                        FechaCreacion = (Timestamp) docGroup["dateCreated"];
                        Asignacion = (List<object>)docGroup["assignedActivities"];
                        Participante = (List<object>)docGroup["participantsGroup"];
                        Debug.Log(string.Format("=> Longitud Asignacion: {0}", Participante.Count));
                        Debug.Log(string.Format("> Leyendo Grupo: Admin {0} | Name {1} | Participantes {2} | FechaCreacion {3} | Asignacion {4}", Admin, NombreGroup, Participante.Count, FechaCreacion  , Asignacion.Count));
                    }

                });
            }

        public void UpdateNameGroup ()
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
        }

        public void UpdateParticipantsGroup(List<object> member)
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
        }

        public void SendEmailNotification(ArrayList member)
        {

        }


    }

    }



