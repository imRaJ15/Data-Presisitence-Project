using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_Text scoreListText;

    [SerializeField] List<Player> players = new List<Player>();

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

    private void Start()
    {
        players.Sort((p1, p2) => p2.Score.CompareTo(p1.Score));

        DisplayScores();
    }
    public void DisplayScores()
    {
        // Clear existing text
        scoreListText.text = "";

        // Display sorted players in the text box
        foreach (Player player in players)
        {
            scoreListText.text += player.Name + ": " + player.Score + "\n";
        }
    }
    public void AddPlayer(string name, int initialScore)
    {
        players.Add(new Player(name, initialScore));
    }

    public void UpdateScore(string name, int newScore)
    {
        Player player = players.Find(p => p.Name == name);
        if (player != null)
        {
            player.Score = newScore;
        }
        else
        {
            Debug.LogWarning("Player not found: " + name);
        }
    }

    public void PrintAllPlayers()
    {
        foreach (Player player in players)
        {
            Debug.Log("Player: " + player.Name + ", Score: " + player.Score);
        }
    }
}
