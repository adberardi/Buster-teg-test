using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MongoDB.Driver;
using MongoDB.Bson;
using ARProject.Task;
using TaskAssigned = ARProject.Task;
using TaskUser = ARProject.Task;
using RegExpress = System.Text.RegularExpressions;
using ARProject.User;
using ARProject.Group;
using UnityEngine.SceneManagement;

public class prueba : MonoBehaviour
{
    private Group group;
    private string[] ListAssignedActivities;
    private Task task;
    private TaskUser.Task activity;
    private ObjectId IdTask { get; set; }

    public GameObject PanelMain;
    public GameObject PanelActivity;
    List<string> assignedActivitiesList = new List<string>();
    private string GameAssigned { get; set; }
    private int RewardAssigned { get; set; }
    private string NameAssigned { get; set; }
    private string StartDateAssigned { get; set; }
    private string EndDateAssigned { get; set; }
    public string GameActivity { get; set; }
    public string GameActivityActual { get; set; }

    public InputField RewardActivity;
    public InputField NameActivity;
    public InputField StartDateActivity;
    public InputField EndDateActivity;
    public Dropdown GameDropdown;

    Group ListMembersBelongs;

    // Variables para los objetos a generar
    public GameObject activityPrefab;   // Prefab del objeto de la actividad
    public Sprite[] activitySprites;    // Array de sprites para las actividades
    public string[] activityNames;      // Array de nombres para las actividades

    // Variables para el scroll bar
    public ScrollRect scrollRect;       // Referencia al Scroll Rect en la escena
    public RectTransform contentRect;  // Referencia al Rect Transform del contenido del Scroll Rect
    public float spacing = 10f;         // Espaciado entre las actividades

    // Variables para la posición y el tamaño de los objetos generados
    private Vector2 activitySize;
    private Vector2 activityPosition;
  
    // Start is called before the first frame update
    void Start()
    {
        string iduser=PlayerPrefs.GetString("IDUser");
        //string iduser = "6411384514070dd6d438055b";
        Task newTask = new Task();
        Task dataTask = new Task();
        User dataUser = new User();
        string itemName = "Item ";
        //Group group = new Group();
        group = new Group();
        activity = new TaskUser.Task();
        Group dataGroup = new Group();
        List<string> groups = dataUser.GetAllGroupsFromUser();
        //List<string> assignedActivitiesList = new List<string>();
        for (int i = 0; i < groups.Count; i++) {
            Debug.Log("grupo::" + groups[i]);
            dataGroup =group.GetGroup(groups[i]);
            //Debug.Log("actividades::"+dataGroup.AssignedActivities.Length);
            if ((dataGroup != null) && (dataGroup.AssignedActivities.Length>0)) {
                Debug.Log("en el if");
                for (int iw = 0; iw < dataGroup.AssignedActivities.Length; iw++) {
                    Debug.Log(dataGroup.AssignedActivities[iw]);
                    assignedActivitiesList.Add(dataGroup.AssignedActivities[iw]);
                }
                Debug.Log("Fuera");
            };
            
        }




        Debug.Log("prueba - activitySprites: "+ assignedActivitiesList.Count); 
        activitySprites = new Sprite[assignedActivitiesList.Count];
        activityNames = new string[assignedActivitiesList.Count];
        //Debug.Log(activitySprites.Length);
       // Obtener el tamaño del objeto de la actividad
       activitySize = activityPrefab.GetComponent<RectTransform>().sizeDelta;

        // Calcular la posición inicial del primer objeto de la actividad
        activityPosition = new Vector2(spacing + activitySize.x / 2f, -spacing - activitySize.y / 2f);

        // Generar los objetos de la actividad
        for (int i = 0; i < activitySprites.Length; i++)
        {
            
           
            // Instanciar el prefab de la actividad
            GameObject activityObject = Instantiate(activityPrefab, contentRect);

            // Establecer la posición del objeto de la actividad
            activityObject.GetComponent<RectTransform>().anchoredPosition = activityPosition;

            // Asignar la imagen y el nombre de la actividad
            activityObject.GetComponent<Image>().sprite = activitySprites[i];
            activitySprites[i] = Resources.Load<Sprite>("Sprites/btn_square_blue_pressed");

            dataTask = newTask.GetTaskById(assignedActivitiesList[i]);

            // Encontrar el componente de texto de la descripción de la actividad
            Text descriptionText = activityObject.transform.Find("Description").GetComponent<Text>();
            Text rewardNumber = activityObject.transform.Find("RewardNumber").GetComponent<Text>();
            // Cargar el sprite de la imagen
            Sprite checkboxSprite = Resources.Load<Sprite>(dataTask.Checked);
            // ...
            // Asignar la imagen y el nombre de la actividad
            activityObject.GetComponent<Image>().sprite = activitySprites[i];
            // ...
            // Asignar el sprite de la imagen del checkbox
            //activityObject.transform.Find("Checkbox").GetComponent<Image>().sprite = checkboxSprite;
            //activityObject.transform.Find("Checkbox").GetComponent<Toggle>().isOn = false;

            itemName = i.ToString();
            activityObject.name = itemName;
            activityNames[i] = dataTask.Name;

            
            activityObject.GetComponentInChildren<Text>().text = activityNames[i];
            string pointTaskAsString = dataTask.PointTask.ToString();
            descriptionText.text = dataTask.Description;
            rewardNumber.text = pointTaskAsString;
          
            // Actualizar la posición para el siguiente objeto de la actividad
            //  activityPosition += new Vector2(activitySize.x + spacing, 0f);
        }
        activityPrefab.SetActive(false);
        // Actualizar el tamaño del contenido del Scroll Rect para mostrar todas las actividades
        contentRect.sizeDelta = new Vector2(activityPosition.x + activitySize.x / 2f + spacing, contentRect.sizeDelta.y);
        DropdownitemSelectd(GameDropdown);
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    // Valida los usuarios que que tengan el Toggle con estatus 'Checked'
    public void ProcessRequestActivity(List<GameObject> activityObject, List<string> listDataMember, List<string> listDataMembersId)
    {
        //List<string> ListNewMembers = new List<string>();
        for (int index = 0; index < activityObject.Count; index++)
        {
            if (activityObject[index].transform.Find("ToggleGreen").GetComponent<Toggle>().isOn)
            {
                //Registra las personas que fueron seleccionadas que van a ser agregadas en el grupo.
                //ListNewMembers.Add(listDataMember[index]);
                Debug.Log("Validando cantidad de miembros a ingresar " + listDataMembersId[index]);
                //ListNewMembers.Add(listDataMembersId[index]);
                //activityObject[index].transform.Find("Title").GetComponent<Text>().text;
                Task data = new Task().GetTaskById(assignedActivitiesList[index]);
                RewardActivity.text = data.Reward.ToString();
                NameActivity.text = data.Name.ToString();
                StartDateActivity.text = data.StartDate;
                EndDateActivity.text = data.EndDate;
            }
            else
            {
                Debug.Log("ProcessRequest: NO hay una persona con casilla marcada");

            }
        }
        //Si por lo menos hay una persona que fue seleccionada, se procedera a registrarlo en la Base de Datos. En caso contrario, se hara caso omiso.
        /*if (ListNewMembers.Count > 0)
        {
            //AddMembersToGroup(ListNewMembers);
            return true;
        }
        else
        {
            return false;
        }*/

    }

    public void ChangePanelToDetail(GameObject activityObj)
    {
        Debug.Log("prueba - ChangePanelToDetail:" + activityObj.name);
        int indexData = int.Parse(activityObj.name);
        try
        {
            Debug.Log("IndexData: " + indexData.ToString());
            //PlayerPrefs.SetInt("IndexData", indexData);
            Task data = new Task().GetTaskById(assignedActivitiesList[indexData]);
            Debug.Log("MenuActividades - ChangePanelToDetail: " + data.GameType);
            IdTask = data._id;
            GameActivityActual = data.GameType;
            RewardActivity.text = data.Reward.ToString();
            NameActivity.text = data.Name.ToString();
            StartDateActivity.text = data.StartDate;
            EndDateActivity.text = data.EndDate;
            PanelActivity.SetActive(true);
            PanelMain.SetActive(false);
            //ChangeScene(23);
        }
        catch (System.ArgumentOutOfRangeException err)
        {
            Debug.Log("Se ha detectado un error del tipo ArgumentOutOfRangeException ListGroups - ChangePanelToDetail: " + err + ", el valor index: " + indexData);
        }

    }

    public void BtnUpdateTask()
    {
        task = new Task();
        Debug.Log("prueba - BtnUpdateTask: GameActivity " + GameActivity + " | GameActivityActual "+ GameActivityActual);
        if (!(GameActivity is null))
            task.SaveTask(IdTask, int.Parse(RewardActivity.text), NameActivity.text, StartDateActivity.text, EndDateActivity.text,GameActivity);
        else
            task.SaveTask(IdTask, int.Parse(RewardActivity.text), NameActivity.text, StartDateActivity.text, EndDateActivity.text, GameActivityActual);
        
    }

    public void ChangePanelToMain()
    {
        PanelActivity.SetActive(false);
        PanelMain.SetActive(true);
    }

    public void UpdatePosSCroll(Vector2 value)
    {
        Debug.Log("UpdatePosSCroll: " + value.y);
    }

    //Obtiene la escuela seleccionada cuando se crea el grupo
    public void DropdownitemSelectd(Dropdown GameDropdown)
    {
        int index = GameDropdown.value;
        GameActivity = GameDropdown.options[index].text;
    }

    public void CreateActivity()
    {
        //Implementar busqueda de los GameObject - InputField
        Debug.Log("ListGroups -> CreateActivity: " + RewardActivity);
        RewardAssigned = int.Parse(RewardActivity.text);
        PlayerPrefs.SetInt("RewardActivity", RewardAssigned);
        NameAssigned = NameActivity.text;
        StartDateAssigned = StartDateActivity.text;
        EndDateAssigned = StartDateActivity.text;
        Debug.Log("ListGroup - CreateActivity: " + RegExpress.Regex.IsMatch(StartDateAssigned, @"^\d{2}/\d{2}/\d{4}$"));
        if ((RegExpress.Regex.IsMatch(StartDateAssigned, @"^\d{2}/\d{2}/\d{4}$")) && (RegExpress.Regex.IsMatch(EndDateAssigned, @"^\d{2}/\d{2}/\d{4}$")))
        {
            TaskAssigned.Task newtask = new TaskAssigned.Task(0, GameAssigned, RewardAssigned, NameAssigned, "", 0, 0f, StartDateAssigned, EndDateAssigned, "Sprites/checkbox_unchecked");
            activity.SaveTask(newtask);
            ListMembersBelongs = group.GetGroup(PlayerPrefs.GetString("IDGroup"));
            ListAssignedActivities = ListMembersBelongs.AssignedActivities;
            Debug.Log(" CreateActivity - ListAssignedActivities: " + ListAssignedActivities.Length);
            List<string> aux = new List<string>(ListAssignedActivities);
            aux.Add(newtask._id.ToString());
            group.UpdateGroupListAssignedActivities(PlayerPrefs.GetString("IDGroup"), aux);
            //Actualizar lista de Actividades Asignadas
            ChangePanelCreateActivitiesToActivities();
        }

    }



    //Change the panel from PanelActivities to PanelGroupDetail.
    public void ChangePanelCreateActivitiesToActivities()
    {
        /*BtnCreateActivities.SetActive(true);
        PanelActivities.SetActive(false);
        PanelGroupDetail.SetActive(true);
        PanelStatistics.SetActive(false);
        PanelMembers.SetActive(false);*/
    }


}
