using UnityEngine;
using UnityEngine.UI;
using ARProject.User;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    private User user = new User();
    public GameObject dialogBox;
    private string GroupSchool { get; set; }
    private string LevelSchool { get; set; }
    //public Text dialogText;

    public void ShowDialog(User user)
    {
        dialogBox.SetActive(true);
        //dialogText.text = dialog;
    }

    public void ConfirmAction()
    {
        //In this case, the user confirm the action to deleted your account.
        user.DeleteUser();
        SceneManager.LoadScene(0);
    }

    public void CloseDialog()
    {
        dialogBox.SetActive(false);
    }

    //Dialog used in Module "User Edit Profile".
    public void ConfirmSchoolSelected()
    {
        PlayerPrefs.SetString("ConfirmSchool", "True");
        CloseDialog();
    }

    //Dialog used in Module "User Edit Profile".
    public void ConfirmLevelSchoolSelected()
    {
        PlayerPrefs.SetString("ConfirmLevelSchool", "True");
        CloseDialog();
    }
}
