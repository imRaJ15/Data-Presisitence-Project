using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    //public string currentPlayerName;

    void Start()
    {
        //currentPlayerName = null;
    }
    public void StartGameButton()
    { SceneManager.LoadScene("main"); }

    public void GetNameString(string s)
    {
        MainMenu.instance._currentPlayerName = s;
    }
}
