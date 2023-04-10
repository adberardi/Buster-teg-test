using System;
using System.Collections.Generic;
using UnityEngine;
using ARProject.Group;
using ARProject.User;
using ARPoject.School;
using MongoDB.Driver;
using MongoDB.Bson;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActionCreateGroups : MonoBehaviour
{
    private Group group;
    private User user;
    private School school;
    private string GroupSchool { get; set; }
    private string LevelSchool { get; set; }
    public InputField nameGroup;
    public Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        group = new Group();
        user = new User();
        school = new School();

        dropdown.options.Clear();

        IMongoCollection<School> docRef = school.GetCollection();
        List<School> result = docRef.Find(Builders<School>.Filter.Empty).ToList();

        //Llena la lista desplegable.
        foreach (var i in result)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = i.SchoolName });
        }

        DropdownitemSelectd(dropdown);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void GoToWindowCreateGroup()
    {
        SceneManager.LoadScene(1);
    }

    public void CreateGroup()
    {
        if ((nameGroup.text != "") && (!group.ExistsSameGroup(nameGroup.text, GroupSchool, LevelSchool)))
        {
            Group newGroup = new Group(nameGroup.text, DateTime.Now, PlayerPrefs.GetString("IDUser"), GroupSchool, LevelSchool);
            ObjectId idGroup = group.CreateGroup(newGroup);
            //Se agrega al usuario que creo al grupo como el administrador y el primero en ser agregado.
            user.AddGroupToUser(idGroup);
        }

    }

    //Obtiene la escuela seleccionada cuando se crea el grupo
    public void DropdownitemSelectd(Dropdown dropdown)
    {
        int index = dropdown.value;
        GroupSchool = dropdown.options[index].text;
    }

    public void ListOptions(ToggleGroup op)
    {
        foreach(Toggle toggleSelected in op.ActiveToggles())
        {
            if (toggleSelected != null)
            {
                LevelSchool = toggleSelected.name;
                Debug.Log("LEVELSCHOOL: " + LevelSchool);
            }
        }
        //LevelSchool = op.name;
    }
}
