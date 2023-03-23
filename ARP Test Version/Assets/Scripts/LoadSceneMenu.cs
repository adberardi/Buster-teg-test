using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ARProject.User;
using ARProject.Group;
using System;
using MongoDB.Driver;
using MongoDB.Bson;

public class LoadSceneMenu : MonoBehaviour
{
    private User user;
    private Group group;

    // Start is called before the first frame update
    void Start()
    {
        user = new User();
        group = new Group();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void RestartGame()
    {
        if (MultiplicationController.current.ValidateAttempts())
        {
       
            //boat.transform.localPosition = new Vector3(-0.274f, 0.01f, 0f);
            //SceneManager.LoadScene(4);
            GameObject aux = MultiplicationController.current.GetBoat();
            //aux.GetComponent<Animator>().SetBool("TopToIdle", true);
            MultiplicationController.current.RestartGame();
        }

    }


    public void ButtonSaveUser()
    {
        //User user = new User();
        user.SaveUser("6411384514070dd6d438055b","Fulanito");
    }

    public void ButtonGetProfile()
    {
        //User user = new User();
        user.ReadUser();
    }

    public void ButtonCreateGroup()
    {
        Group newGroup = new Group("nameGroup", DateTime.Now, "llllll");
        group.CreateGroup(newGroup);
    }

    public void ButtonDeleteGroup()
    {
        group.DeleteGroup("641a85a533e6f58db731c316");
    }
}
