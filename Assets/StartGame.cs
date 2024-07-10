using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] TMP_Text scoreListText;

    private void Start()
    {
        if (MainMenu.Instance != null)
        {
            MainMenu.Instance.AssignScoreListText(scoreListText);
        }
    }

    public void StartGameButton()
    { SceneManager.LoadScene("main"); }

    public void GetNameString(string s)
    {
        MainMenu.Instance._currentPlayerName = s;
    }
}
