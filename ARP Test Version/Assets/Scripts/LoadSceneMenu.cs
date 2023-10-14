using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ARProject.User;
using ARProject.Group;
using ARProject.Score;
using ARProject.Task;
using System;
using MongoDB.Driver;
using MongoDB.Bson;

public class LoadSceneMenu : MonoBehaviour
{
    private User user;
    private Group group;
    private Score score;
    private Task taskClass;

    public GameObject UsernameText;
    public GameObject UserCollectedReward;

    // Start is called before the first frame update
    void Start()
    {
        user = new User();
        group = new Group();
        score = new Score();
        taskClass = new Task();
        UsernameText.GetComponent<Text>().text = PlayerPrefs.GetString("Username");
        UserCollectedReward.GetComponent<Text>().text = PlayerPrefs.GetString("UserReward");
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
        //user.SaveUser("6411384514070dd6d438055b","Fulanito");
        //User newUser = new User("usernameField", "probando@email.com", "123456789");
        //user.CreateUser(newUser);
        //user.AddGroupToUser(new ObjectId());
        //user.DeleteGroupUser("6411384514070dd6d438055i");
        /*List<string> aux = user.GetAllGroupsFromUser();
        foreach (var index in aux)
        {
            Debug.Log("Group: "+index);
        }*/
        user.EmailExist("prueba@email.com");

    }

    public void ButtonGetProfile()
    {
        //User user = new User();
        user.ReadUser();
    }

    /*public void ButtonCreateGroup()
    {
        //Group newGroup = new Group("nameGroup", DateTime.Now, "llllll", "U.E. Instituto Humanitas","1er Grado");
        //group.CreateGroup(newGroup);
        group.ReadGroup("641bcdb046c68d6ae5968c4c");
    }*/

    /*public void ButtonDeleteGroup()
    {
        group.DeleteGroup("641a85a533e6f58db731c316");
    }*/

    public void ButtonSaveScore()
    {
        Score newScore = new Score("6411384514070dd6d438055b", 10, new ObjectId());
        score.SaveScore(newScore);
    }

    public void ButtonReadscore()
    {
        score.ReadScore(user.GetSessionDataUser());
    }

    /*public void ButtonCreateTask()
    {
       // Task newTask = new Task("contentTask", DateTime.Now.ToString(), DateTime.Now.ToString(), "groupTask", 15, 20);
       // taskClass.SaveTask(newTask);
        //taskClass.ReadTask("6420fca5daf7d33604c8e65f");
        //taskClass.UpdateTask("6420fca5daf7d33604c8e65f", "New Content Updated");
        //taskClass.DeleteTask("6420fca5daf7d33604c8e65f");
    }*/
}
