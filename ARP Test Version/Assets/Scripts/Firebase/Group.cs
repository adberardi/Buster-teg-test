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
        private DateTime FechaCreacion { get; set; }
        private string Admin { get; set; }
        // TODO: Por agregar clases Estudiante y Tarea
        private ArrayList Participante { get; set; }
        private ArrayList Asignacion { get; set; }

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
                { "participantsGroup", new ArrayList() {"b72406d79978ea67095af8c75c24eac0","8nkf5pBPwFcRhUjShQmnwmlPlyE3", "8f128e1e2473092b221b2a89030b817e" } },

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
                        Debug.Log(string.Format("> Leyendo Grupo: Admin {0} | Name {1} | Participantes {2}", docGroup["admin"], docGroup["name"], docGroup["participantsGroup"].GetType()));

                        foreach (var index in (List<object>)docGroup["participantsGroup"])
                        {
                            Debug.Log(string.Format("Ciclo: {0}", index));
                            //TODO: corregir Participante.Add(index)
                            Participante.Add(index);
                        }
                        NombreGroup = docGroup["name"].ToString();
                        Admin = docGroup["admin"].ToString();
                        //FechaCreacion = (DateTime)docGroup["dateCreated"];
                        //Asignacion.Add((ArrayList)docGroup["assignedActivities"]);
                        //Debug.Log(string.Format("=> Longitud Asignacion: ", Asignacion.Count));
                    }

                });
            }
        }

    }



