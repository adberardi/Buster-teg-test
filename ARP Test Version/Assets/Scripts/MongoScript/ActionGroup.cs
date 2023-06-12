using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ARProject.User;
using ARProject.Group;

public class ActionGroup : MonoBehaviour
{
    private Group group;
    private User user;
    public Button btnIngresar;

    // Start is called before the first frame update
    void Start()
    {
        group = new Group();
        user = new User();

        List<string> dataGroup = user.GetAllGroupsFromUser();
        List<Group> aux = new List<Group>();
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
