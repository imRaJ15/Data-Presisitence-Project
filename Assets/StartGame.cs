using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] TMP_Text scoreListText, currentHighScoreText;
    [SerializeField] GameObject highestScoreTextBox, enterTextTextBox;
    [SerializeField] Toggle toggleButton;

    private bool isNameEntered;

    private void Start()
    {
        if (MainMenu.Instance != null)
        {
            MainMenu.Instance.AssignScoreListText(scoreListText);
        }

        highestScoreTextBox.SetActive(false);
        enterTextTextBox.SetActive(false);
        isNameEntered = false;
    }

    private void Update()
    {
        if (toggleButton.isOn) 
        { highestScoreTextBox.SetActive(true); }
        else { highestScoreTextBox.SetActive(false); }

        currentHighScoreText.text = "Best Score " + MainMenu.Instance.players[0].Name + " : " + MainMenu.Instance.players[0].Score;
    }

    public void StartGameButton()
    {
        if (isNameEntered)
        { SceneManager.LoadScene("main"); }
    }

    public void QuitGame()
    { Application.Quit(); }

    public void GetNameString(string s)
    {
        if (s != null)
        { 
            MainMenu.Instance._currentPlayerName = s;
            isNameEntered = true;
        }
        else { 
            enterTextTextBox.SetActive(true); 
            isNameEntered = false;
        }
    }
}
