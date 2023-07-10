using System;
using System.Collections.Generic;
using UnityEngine;
using ARProject.Group;
using ARProject.User;
using ARProject.School;
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
    public GameObject PanelMember;
    public GameObject PanelForm;
    public GameObject BtnBackForm;
    public List<ObjectId> studentsList = new List<ObjectId>();
    List<User> dataResult;
    List<GameObject> listMembers = new List<GameObject>();


    // Variables para los objetos a generar
    public GameObject activityPrefab;   // Prefab del objeto de la actividad
    public Sprite[] activitySprites;    // Array de sprites para las actividades
    public string[] activityNames;      // Array de nombres para las actividades

    private GameObject AuxObj { get; set; }

    // Variables para el scroll bar
    public ScrollRect scrollRect;       // Referencia al Scroll Rect en la escena
    public RectTransform contentRect;  // Referencia al Rect Transform del contenido del Scroll Rect
    public float spacing = 0f;         // Espaciado entre las actividades

    // Variables para la posición y el tamaño de los objetos generados
    private Vector2 activitySize;
    private Vector2 activityPosition;

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

        /*for (int i = 0; i < activitySprites.Length; i++)
        {


            // Instanciar el prefab de la actividad
            GameObject activityObject = Instantiate(activityPrefab, contentRect);

            // Establecer la posición del objeto de la actividad
            activityObject.GetComponent<RectTransform>().anchoredPosition = activityPosition;

            // Asignar la imagen y el nombre de la actividad
            activityObject.GetComponent<Image>().sprite = activitySprites[i];
            activitySprites[i] = Resources.Load<Sprite>("Sprites/btn_square_blue");

            // Encontrar el componente de texto de la descripción de la actividad
            Text descriptionText = activityObject.transform.Find("Description").GetComponent<Text>();

            // Asignar la imagen y el nombre de la actividad
            activityObject.GetComponent<Image>().sprite = activitySprites[i];


            itemName = i.ToString();
            activityObject.name = itemName;
            activityNames[i] = groupBelongs[i].NameGroup;
            activityObject.GetComponentInChildren<Text>().text = activityNames[i];
            descriptionText.text = groupBelongs[i].School;

        }
        activityPrefab.SetActive(false);*/

        DropdownitemSelectd(dropdown);

        ChangePanelToDetail();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePosSCroll(Vector2 value)
    {
        Debug.Log("UpdatePosSCroll: " + value.y);
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
            Group newGroup = new Group(ObjectId.GenerateNewId(), nameGroup.text, DateTime.Now, PlayerPrefs.GetString("IDUser"), GroupSchool, LevelSchool);
            ObjectId idGroup = group.CreateGroup(newGroup);
            Debug.Log("Id Grupo Nuevo: " + idGroup.ToString()+" PRUEBA: "+ ObjectId.GenerateNewId().ToString());
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

    public void ChangePanelToDetail()
    {
        //studentsList = school.ListStudents("64302d6b274b7b781c82ebcf");
        //studentsList = school.ListStudents();
        SearchStudents(studentsList);
        //int indexData = int.Parse(activityObj.name);
        //PanelMember.SetActive(true);
        //PanelForm.SetActive(false);
        //BtnBackForm.SetActive(false);
        //PlayerPrefs.SetString("IDGroup", dataResult[indexData]._id.ToString());
        //PlayerPrefs.SetInt("IndexData", indexData);
        //LoadDataPanel(studentsList[indexData]);
    }

    public void SearchStudents(List<ObjectId> param)
    {
        List<User> result = new List<User>();
        List<ObjectId> aux = school.ListStudents("64302d6b274b7b781c82ebcf");
        //List<ObjectId> aux = new List<ObjectId>();
        /*for (int i = 0; i < param.Count; i++ )
        {
            aux.Add(ObjectId.Parse(param[i].ToString()));
        }*/
        result = user.GetStudents(aux);
        
        LoadScroll(result);
    }

    public void LoadScroll(List<User> groupBelongs)
    {
        Debug.Log("Entrando a LoadScroll ");
        string itemName = "Item ";
        activitySprites = new Sprite[groupBelongs.Count];
        //activitySprites = new Sprite[dataGroup.Count];
        activityNames = new string[groupBelongs.Count];
        Debug.Log(activitySprites.Length);
        // Obtener el tamaño del objeto de la actividad
        activitySize = activityPrefab.GetComponent<RectTransform>().sizeDelta;

        // Calcular la posición inicial del primer objeto de la actividad
        activityPosition = new Vector2(spacing + activitySize.x / 2f, -spacing - activitySize.y / 2f);
        for (int i = 0; i < activitySprites.Length; i++)
        {


            // Instanciar el prefab de la actividad
            GameObject activityObject = Instantiate(activityPrefab, contentRect);

            // Establecer la posición del objeto de la actividad
            activityObject.GetComponent<RectTransform>().anchoredPosition = activityPosition;

            // Asignar la imagen y el nombre de la actividad
            //activityObject.GetComponent<Image>().sprite = activitySprites[i];
            activitySprites[i] = Resources.Load<Sprite>("Sprites/bg_gradient_medium_yellow");

            // Encontrar el componente de texto de la descripción de la actividad
            //Text descriptionText = activityObject.transform.Find("Description").GetComponent<Text>();
            Toggle optionCheckbox = activityObject.transform.Find("ToggleGreen").GetComponent<Toggle>();

            // Asignar la imagen y el nombre de la actividad
            //activityObject.GetComponent<Image>().sprite = activitySprites[i];


            itemName = i.ToString();
            activityObject.name = itemName;
            activityNames[i] = groupBelongs[i].FirstName +" "+ groupBelongs[i].LastName;
            activityObject.GetComponentInChildren<Text>().text = activityNames[i];
            //descriptionText.text = groupBelongs[i].School;
            optionCheckbox.isOn = false;
            Debug.Log("Dentro del Loop");
            listMembers.Add(activityObject);
        }
        activityPrefab.SetActive(false);
        // Actualizar el tamaño del contenido del Scroll Rect para mostrar todas las actividades
        contentRect.sizeDelta = new Vector2(activityPosition.x + activitySize.x / 2f + spacing, contentRect.sizeDelta.y);
        Debug.Log("Saliendo a LoadScroll ");
    }

    //Metodo que se invoca al presionar el boton de Submit
    public void OnSubmit()
    {
        school.ProcessRequest(listMembers);
    }

}
