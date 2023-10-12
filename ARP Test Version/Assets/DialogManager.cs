using UnityEngine;
using UnityEngine.UI;
using ARProject.User;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    private User user = new User();
    public GameObject dialogBox;
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
}
