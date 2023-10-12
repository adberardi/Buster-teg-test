
using UnityEngine;
using UnityEngine.UI;
using ARProject.User;
using System.Collections;
using UnityEngine.SceneManagement;

public class ActionUpdateUser : MonoBehaviour
{
    public DialogManager dialogManager;
    public InputField email;
    public InputField passw;
    public InputField userName;
    public InputField firstName;
    public InputField lastName;
    //public Transform birthDay;
    public string profile;
    private User user;

    // Start is called before the first frame update
    void Start()
    {
        
        user = new User();
        userName.text = user.GetDataUser()["Username"];
        firstName.text = user.GetDataUser()["Firstname"];
        lastName.text = user.GetDataUser()["Lastname"];
        email.text = user.GetDataUser()["Email"];
        //Debug.Log("Entrando a Start " + user.GetDataUser()["Username"]);
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    //When the user update yourself profile information.
    public void UpdateUser()
    {
        //Debug.Log("Fecha insertada: "+ birthDay.name);
        user.SaveUser(firstName.text,lastName.text,passw.text,email.text, "12/03/2022", profile);
    }

    //When the user delete yourself account.
    public void DeleteUser()
    {
        dialogManager.ShowDialog(user);
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
