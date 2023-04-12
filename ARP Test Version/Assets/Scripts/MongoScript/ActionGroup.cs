using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ARProject.User;
using ARProject.Group;

public class ActionGroup : MonoBehaviour
{
    private Group group;
    private User user;

    // Start is called before the first frame update
    void Start()
    {
        group = new Group();
        user = new User();

        List<string> dataGroup = user.GetAllGroupsFromUser();
        List<Group> aux = new List<Group>();

        foreach (var index in dataGroup)
        {
            aux.Add(group.GetGroup(index));
        }

        Debug.Log("Total de grupos a los que pertenezco: " + aux.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

}

//643359be46c68d2f7fe1c9be
//6411373b14070dd6d4380557
//