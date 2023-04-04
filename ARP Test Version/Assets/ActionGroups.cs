using System;
using System.Collections.Generic;
using UnityEngine;
using ARProject.Group;
using ARProject.User;
using MongoDB.Driver;
using MongoDB.Bson;
using UnityEngine.SceneManagement;

public class ActionGroups : MonoBehaviour
{
    private Group group;
    private User user;
    // Start is called before the first frame update
    void Start()
    {
        group = new Group();
        user = new User();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToWindowCreateGroup()
    {
        SceneManager.LoadScene(1);
    }

    public void CreateGroup()
    {
        Group newGroup = new Group("nameGroup", DateTime.Now, PlayerPrefs.GetString("IDUser"));
        ObjectId idGroup = group.CreateGroup(newGroup);
        //Se agrega al usuario que creo al grupo como el administrador y el primero en ser agregado.
        user.AddGroupToUser(idGroup);
    }
}
