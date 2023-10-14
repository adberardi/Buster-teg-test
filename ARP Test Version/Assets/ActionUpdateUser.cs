
using UnityEngine;
using UnityEngine.UI;
using ARProject.User;
using ARProject.School;
using MongoDB.Driver;
using System.Collections.Generic;
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
    private School school;
    public Dropdown dropdown;
    private string GroupSchool { get; set; }
    private string LevelSchool { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
        user = new User();
        userName.text = user.GetDataUser()["Username"];
        firstName.text = user.GetDataUser()["Firstname"];
        lastName.text = user.GetDataUser()["Lastname"];
        email.text = user.GetDataUser()["Email"];
        //Debug.Log("Entrando a Start " + user.GetDataUser()["Username"]);
        school = new School();
        dropdown.options.Clear();
        IMongoCollection<School> docRef = school.GetCollection();
        List<School> result = docRef.Find(Builders<School>.Filter.Empty).ToList();
        //Llena la lista desplegable.
        foreach (var i in result)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = i.SchoolName });
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    //When the user update yourself profile information.
    public void UpdateUser()
    {
        //Debug.Log("Fecha insertada: "+ birthDay.name);
        user.SaveUser(firstName.text,lastName.text,passw.text,email.text, "12/03/2022", profile, GroupSchool, LevelSchool);
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

    //Obtiene la escuela seleccionada cuando se crea el grupo.
    public void DropdownitemSelectd(Dropdown dropdown)
    {
        int index = dropdown.value;
        GroupSchool = dropdown.options[index].text;
    }

    //Obtiene el grado seleccionado por el usuario.
    public void DropdownLeveltemSelectd(Dropdown dropdown)
    {
        int index = dropdown.value;
        LevelSchool = dropdown.options[index].text.Trim();
        Debug.Log("LevelSchool: " + LevelSchool);
    }
}
