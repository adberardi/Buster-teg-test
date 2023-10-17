
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
    public GameObject dialogSchool;
    public GameObject dialogLevel;
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
    private string NewGroupSchool { get; set; }
    private string LevelSchool { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("ConfirmSchool", "False");
        PlayerPrefs.SetString("ConfirmLevelSchool", "False");
        GroupSchool = PlayerPrefs.GetString("School");
        LevelSchool = PlayerPrefs.GetString("LevelSchool");
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
        dropdown.options.Add(new Dropdown.OptionData() { text = "Seleccione Colegio" });
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
        if ((PlayerPrefs.GetString("ConfirmSchool").Equals("True") && (PlayerPrefs.GetString("ConfirmLevelSchool").Equals("True"))))
            user.SaveUser(firstName.text, lastName.text, passw.text, email.text, "12/03/2022", profile, GroupSchool, LevelSchool);
        else if ((PlayerPrefs.GetString("ConfirmSchool").Equals("True")) && (PlayerPrefs.GetString("ConfirmLevelSchool").Equals("False")))
            user.SaveUser(firstName.text, lastName.text, passw.text, email.text, "12/03/2022", profile, GroupSchool, PlayerPrefs.GetString("LevelSchool"));
        else if ((PlayerPrefs.GetString("ConfirmSchool").Equals("False")) && (PlayerPrefs.GetString("ConfirmLevelSchool").Equals("True")))
            user.SaveUser(firstName.text, lastName.text, passw.text, email.text, "12/03/2022", profile, PlayerPrefs.GetString("School"), LevelSchool);
        else
            Debug.Log("no entro en nigun If");
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

    //Actives the window to select the new School.
    public void ShowDialogSchool()
    {
        dialogSchool.SetActive(true);
    }

    //Actives the window to select the new Level.
    public void ShowDialogLevel()
    {
        dialogLevel.SetActive(true);
    }

    //Obtiene la escuela seleccionada cuando se crea el grupo.
    public void DropdownitemSelectd(Dropdown dropdown)
    {
        int index = dropdown.value;
        GroupSchool = dropdown.options[index].text;
        Debug.Log("Colegio seleccionado: " + GroupSchool);
    }

    //Obtiene el grado seleccionado por el usuario.
    public void DropdownLeveltemSelectd(Dropdown dropdown)
    {
        int index = dropdown.value;
        LevelSchool = dropdown.options[index].text.Trim();
        Debug.Log("LevelSchool: " + LevelSchool);
    }
}
