using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_Text highScoreText;
    public static MainMenu instance;

    public string _currentPlayerName;

    private void Awake()
    {
        if (instance == null ) 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject);  return; }
    }

    void Start()
    {
        DisplayHighScores();
    }

    void DisplayHighScores()
    {
        // Retrieve all player names and scores
        List<KeyValuePair<string, int>> scores = new List<KeyValuePair<string, int>>();

        foreach (string key in PlayerPrefs.GetString("PlayerName"))
        {
            int score = PlayerPrefs.GetInt(key, 0);
            scores.Add(new KeyValuePair<string, int>(key, score));
        }

        // Sort scores in descending order
        scores.Sort((x, y) => y.Value.CompareTo(x.Value));

        // Display scores in TMP_Text
        highScoreText.text = "High Scores:\n";
        foreach (var score in scores)
        {
            highScoreText.text += score.Key + ": " + score.Value + "\n";
        }
    }

    public void GetNameString(string s)
    {
        _currentPlayerName = s;
    }

    public void StartGame()
    { SceneManager.LoadScene("main"); }
}
